// src/components/SubastaList.jsx
import React, { useState, useEffect } from 'react';
import { getSubastas } from '../api';

export function SubastaList({ onSeleccionarSubasta }) {
  const [subastas, setSubastas] = useState([]);
  const [cargando, setCargando] = useState(true);
  const [error, setError] = useState(null);

  useEffect(() => {
    const cargarSubastas = async () => {
      try {
        setCargando(true);
        setError(null);
        const respuesta = await getSubastas();

        if (Array.isArray(respuesta)) {
          setSubastas(respuesta);
        } else if (respuesta && Array.isArray(respuesta.items)) {
          setSubastas(respuesta.items);
        } else if (respuesta && Array.isArray(respuesta.data)) {
          setSubastas(respuesta.data);
        } else {
          setSubastas([]);
        }
      } catch (err) {
        console.error("Error al cargar subastas:", err);
        setError("No se pudieron cargar las subastas.");
        setSubastas([]);
      } finally {
        setCargando(false);
      }
    };

    cargarSubastas();
  }, []);

  if (cargando) {
    return (
      <div style={{ textAlign: 'center', padding: '40px', color: '#1a1a1a' }}>
        <h3>Cargando catálogo de subastas...</h3>
      </div>
    );
  }

  if (error) {
    return (
      <div style={{ textAlign: 'center', padding: '20px', color: '#721c24', backgroundColor: '#f8d7da', borderRadius: '8px' }}>
        <p>{error}</p>
      </div>
    );
  }

  return (
    <div>
      {(!subastas || subastas.length === 0) ? (
        <p style={{ textAlign: 'center', color: '#555', padding: '20px' }}>
          No hay subastas disponibles en este momento.
        </p>
      ) : (
        <div style={{ 
          display: 'grid', 
          gridTemplateColumns: 'repeat(auto-fill, minmax(260px, 1fr))', 
          gap: '20px' 
        }}>
          {subastas.map((subasta) => (
            <div 
              key={subasta.id || subasta.auctionId || Math.random()}
              onClick={() => onSeleccionarSubasta && onSeleccionarSubasta(subasta)}
              style={{
                backgroundColor: '#fff',
                borderRadius: '12px',
                padding: '16px',
                boxShadow: '0 4px 12px rgba(0,0,0,0.08)',
                cursor: 'pointer',
                transition: 'transform 0.2s ease',
              }}
            >
              <h3 style={{ margin: '0 0 8px 0', color: '#1a1a1a' }}>
                {subasta.titulo || subasta.title || 'Subasta sin título'}
              </h3>
              <p style={{ margin: '0 0 12px 0', color: '#666', fontSize: '14px' }}>
                {subasta.descripcion || subasta.description || 'Sin descripción'}
              </p>
              <div style={{ display: 'flex', justifyContent: 'space-between', alignItems: 'center' }}>
                <span style={{ fontSize: '12px', color: '#888' }}>Precio actual:</span>
                <strong style={{ fontSize: '18px', color: '#28a745' }}>
                  ${subasta.precioActual ?? subasta.currentPrice ?? subasta.precioBase ?? 0}
                </strong>
              </div>
            </div>
          ))}
        </div>
      )}
    </div>
  );
}