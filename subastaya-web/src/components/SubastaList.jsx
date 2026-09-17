// src/components/SubastaList.jsx
import React, { useState, useEffect } from 'react';
import { getSubastas } from '../api';

export function SubastaList({ 
  onSeleccionarSubasta, 
  busqueda = '', 
  categoriaFiltro = 'todas', 
  estadoFiltro = 'todos', 
  ordenValor = 'recientes' 
}) {
  const [subastas, setSubastas] = useState([]);
  const [cargando, setCargando] = useState(true);

  const cargarSubastas = async () => {
    setCargando(true);
    try {
      const res = await getSubastas();
      if (res && res.data && Array.isArray(res.data)) {
        setSubastas(res.data);
      } else {
        setSubastas([]);
      }
    } catch (err) {
      console.warn('[CODE-ERROR] - Fallo al cargar subastas desde la API:', err);
      setSubastas([]);
    } finally {
      setCargando(false);
    }
  };

  useEffect(() => {
    cargarSubastas();
  }, []);

  // Lógica de Filtrado y Ordenamiento
  const subastasProcesadas = subastas
    .filter((item) => {
      // Búsqueda por palabra clave
      const coincideBusqueda = !busqueda || 
        (item.titulo && item.titulo.toLowerCase().includes(busqueda.toLowerCase())) ||
        (item.descripcion && item.descripcion.toLowerCase().includes(busqueda.toLowerCase()));

      // Filtro por categoría
      const coincideCategoria = categoriaFiltro === 'todas' || 
        String(item.categoriaId) === String(categoriaFiltro) ||
        (item.nombreCategoria && item.nombreCategoria.toLowerCase() === categoriaFiltro.toLowerCase());

      // Filtro por estado
      const coincideEstado = estadoFiltro === 'todos' || 
        (item.estado && item.estado.toUpperCase() === estadoFiltro.toUpperCase());

      return coincideBusqueda && coincideCategoria && coincideEstado;
    })
    .sort((a, b) => {
      const precioA = a.ofertaMasAlta || a.precioInicial || 0;
      const precioB = b.ofertaMasAlta || b.precioInicial || 0;

      if (ordenValor === 'precio_asc') {
        return precioA - precioB;
      }
      if (ordenValor === 'precio_desc') {
        return precioB - precioA;
      }
      // Por defecto: más recientes (por ID)
      return (b.subastaId || b.id || 0) - (a.subastaId || a.id || 0);
    });

  if (cargando) {
    return <p style={{ fontStyle: 'italic', color: '#000000' }}>Cargando subastas desde la base de datos...</p>;
  }

  return (
    <div style={{ color: '#000000' }}>
      {subastasProcesadas.length === 0 ? (
        <p style={{ fontStyle: 'italic', color: '#000000', margin: '20px 0', fontSize: '15px' }}>
          No se encontraron subastas que coincidan con los criterios de búsqueda.
        </p>
      ) : (
        <div style={{ display: 'grid', gridTemplateColumns: 'repeat(auto-fill, minmax(280px, 1fr))', gap: '20px' }}>
          {subastasProcesadas.map((item) => {
            const tienePujas = item.ofertaMasAlta && item.ofertaMasAlta > item.precioInicial;
            const precioAMostrar = tienePujas ? item.ofertaMasAlta : (item.precioInicial || 0);
            const etiquetaPrecio = tienePujas ? 'Precio actual:' : 'Precio inicial:';
            const imagenSrc = item.urlImagen || item.imagenUrl || 'https://via.placeholder.com/300x180?text=Sin+Imagen';

            return (
              <div
                key={item.subastaId || item.id}
                style={{
                  backgroundColor: '#FFD2B5',
                  padding: '18px',
                  borderRadius: '12px',
                  border: '1px solid #b38b6d',
                  display: 'flex',
                  flexDirection: 'column',
                  justify: 'space-between'
                }}
              >
                <div>
                  {/* IMAGEN DEL PRODUCTO */}
                  <div style={{ width: '100%', height: '180px', borderRadius: '8px', overflow: 'hidden', marginBottom: '14px', backgroundColor: '#E3C3B1' }}>
                    <img
                      src={imagenSrc}
                      alt={item.titulo}
                      style={{ width: '100%', height: '100%', objectFit: 'cover' }}
                      onError={(e) => {
                        e.target.onerror = null;
                        e.target.src = 'https://via.placeholder.com/300x180?text=Imagen+No+Disponible';
                      }}
                    />
                  </div>

                  {/* TÍTULO Y ESTADO */}
                  <div style={{ display: 'flex', justifyContent: 'space-between', alignItems: 'flex-start', marginBottom: '8px' }}>
                    <h3 style={{ margin: 0, fontSize: '18px', fontWeight: 'bold', color: '#000000' }}>
                      {item.titulo}
                    </h3>
                    <span style={{
                      padding: '4px 8px',
                      borderRadius: '6px',
                      fontSize: '11px',
                      fontWeight: 'bold',
                      backgroundColor: item.estado === 'ACTIVA' || item.estado === 'Activa' ? 'rgba(40, 167, 69, 0.2)' : 'rgba(108, 117, 125, 0.2)',
                      color: item.estado === 'ACTIVA' || item.estado === 'Activa' ? '#155724' : '#495057'
                    }}>
                      {item.estado || 'ACTIVA'}
                    </span>
                  </div>

                  {/* CATEGORÍA Y EVALUACIÓN DE PRECIO */}
                  <div style={{ fontSize: '13px', color: '#000000', marginBottom: '14px' }}>
                    <div style={{ marginBottom: '4px' }}>
                      Categoría: <strong>{item.nombreCategoria || item.categoria || 'General'}</strong>
                    </div>
                    <div style={{ fontSize: '15px', fontWeight: 'bold', color: '#000000' }}>
                      {etiquetaPrecio} <span style={{ fontSize: '17px' }}>${precioAMostrar.toLocaleString('es-AR')}</span>
                    </div>
                  </div>
                </div>

                <button
                  onClick={() => onSeleccionarSubasta && onSeleccionarSubasta(item)}
                  style={{
                    width: '100%',
                    padding: '10px',
                    backgroundColor: '#1e1e1e',
                    color: '#ffffff',
                    border: 'none',
                    borderRadius: '8px',
                    fontSize: '14px',
                    fontWeight: 'bold',
                    cursor: 'pointer'
                  }}
                >
                  Ver Detalle y Pujar
                </button>
              </div>
            );
          })}
        </div>
      )}
    </div>
  );
}

export default SubastaList;