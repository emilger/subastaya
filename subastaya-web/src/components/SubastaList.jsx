import { useState, useEffect } from 'react';
import { getSubastas } from '../api';

export function SubastaList({ onSeleccionarSubasta }) {
  const [subastas, setSubastas] = useState([]);
  const [cargando, setCargando] = useState(true);
  const [error, setError] = useState(null);

  const obtenerCatalogo = async () => {
    try {
      setCargando(true);
      setError(null);
      const res = await getSubastas();
      setSubastas(res.data);
    } catch (err) {
      setError(err.response?.data?.mensaje || 'Error al obtener el catálogo de subastas.');
    } finally {
      setCargando(false);
    }
  };

  useEffect(() => {
    obtenerCatalogo();
  }, []);

  if (cargando) return <p style={{ textAlign: 'center' }}>Cargando catálogo de subastas...</p>;
  if (error) return <p style={{ color: 'red', textAlign: 'center' }}>{error}</p>;

  return (
    <div style={{ maxWidth: '900px', margin: '30px auto', padding: '0 20px' }}>
      <div style={{ display: 'flex', justifyContent: 'space-between', alignItems: 'center', marginBottom: '20px' }}>
        <h2>Catálogo de Subastas Activas</h2>
        <button onClick={obtenerCatalogo} style={{ padding: '8px 12px', cursor: 'pointer' }}>
          🔄 Actualizar Lista
        </button>
      </div>

      {subastas.length === 0 ? (
        <p>No hay subastas disponibles en este momento.</p>
      ) : (
        <div style={{ 
          display: 'grid', 
          gridTemplateColumns: 'repeat(auto-fill, minmax(260px, 1fr))', 
          gap: '20px' 
        }}>
          {subastas.map((subasta) => (
            <div 
              key={subasta.subastaId || subasta.id} 
              style={{ 
                border: '1px solid #ccc', 
                borderRadius: '8px', 
                padding: '16px', 
                backgroundColor: '#fff',
                boxShadow: '0 2px 5px rgba(0,0,0,0.1)',
                display: 'flex',
                flexDirection: 'column',
                justify: 'space-between'
              }}
            >
              <div>
                <h3 style={{ marginTop: 0, color: '#1a237e' }}>{subasta.titulo}</h3>
                <p style={{ color: '#555', fontSize: '0.9rem' }}>{subasta.descripcion}</p>
                <hr style={{ border: 'none', borderTop: '1px solid #eee', margin: '10px 0' }} />
                
                <p style={{ margin: '5px 0' }}>
                  <strong>Precio Base:</strong> ${subasta.precioBase}
                </p>
                <p style={{ margin: '5px 0', fontSize: '1.1rem', color: '#2e7d32' }}>
                  <strong>Oferta Actual:</strong> ${subasta.precioActual || subasta.precioBase}
                </p>
                <p style={{ margin: '5px 0', fontSize: '0.85rem', color: '#666' }}>
                  <strong>Estado:</strong> <span style={{ textTransform: 'uppercase', fontWeight: 'bold' }}>{subasta.estado}</span>
                </p>
              </div>

              <button 
                onClick={() => onSeleccionarSubasta && onSeleccionarSubasta(subasta)}
                style={{ 
                  marginTop: '15px', 
                  padding: '10px', 
                  backgroundColor: '#1976d2', 
                  color: '#fff', 
                  border: 'none', 
                  borderRadius: '4px', 
                  cursor: 'pointer',
                  fontWeight: 'bold'
                }}
              >
                Ver Detalle / Ofertar
              </button>
            </div>
          ))}
        </div>
      )}
    </div>
  );
}