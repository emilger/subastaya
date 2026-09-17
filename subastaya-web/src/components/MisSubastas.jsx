// src/components/MisSubastas.jsx
import React, { useState, useEffect } from 'react';
import { getSubastas } from '../api';

export default function MisSubastas({ usuarioId = 1, onSeleccionarSubasta, onCrearSubasta }) {
  const [pestanaActiva, setPestanaActiva] = useState('publicadas');
  const [subastasPublicadas, setSubastasPublicadas] = useState([]);
  const [misPujas, setMisPujas] = useState([]);
  const [cargando, setCargando] = useState(true);

  // Estados para controlar el hover del subrayado
  const [hoverCrearHeader, setHoverCrearHeader] = useState(false);
  const [hoverCrearEmpty, setHoverCrearEmpty] = useState(false);

  useEffect(() => {
    const cargarMisDatosDesdeBD = async () => {
      setCargando(true);
      try {
        const res = await getSubastas();
        if (res && res.data && Array.isArray(res.data)) {
          // 1. Subastas donde el usuario es el Vendedor
          const publicadas = res.data.filter(s => 
            Number(s.vendedorId) === Number(usuarioId) || Number(s.usuarioId) === Number(usuarioId)
          );
          setSubastasPublicadas(publicadas);

          // 2. Subastas donde el usuario realizó ofertas
          const participaciones = res.data.filter(s => 
            Array.isArray(s.pujas) && s.pujas.some(p => Number(p.compradorId) === Number(usuarioId))
          );
          setMisPujas(participaciones);
        } else {
          setSubastasPublicadas([]);
          setMisPujas([]);
        }
      } catch (err) {
        console.warn('[CODE-ERROR] - Fallo al consultar subastas desde la base de datos:', err);
        setSubastasPublicadas([]);
        setMisPujas([]);
      } finally {
        setCargando(false);
      }
    };

    if (usuarioId) {
      cargarMisDatosDesdeBD();
    }
  }, [usuarioId]);

  const handleCrearClick = () => {
    if (typeof onCrearSubasta === 'function') {
      onCrearSubasta();
    } else {
      console.warn('La función onCrearSubasta no fue pasada como prop desde App.jsx.');
    }
  };

  return (
    <div style={{ maxWidth: '650px', margin: '0 auto', color: '#000000', padding: '10px 0' }}>
      {/* ENCABEZADO CON SUBRAYADO ÚNICAMENTE EN HOVER */}
      <div style={{ display: 'flex', justifyContent: 'space-between', alignItems: 'center', marginBottom: '24px' }}>
        <h2 style={{ margin: 0, color: '#000000', fontSize: '24px', fontWeight: 'bold' }}>
          Mis Subastas
        </h2>
        <button
          onClick={handleCrearClick}
          onMouseEnter={() => setHoverCrearHeader(true)}
          onMouseLeave={() => setHoverCrearHeader(false)}
          style={{
            padding: '4px 0',
            backgroundColor: 'transparent',
            color: '#000000',
            border: 'none',
            borderBottom: hoverCrearHeader ? '2px solid #000000' : '2px solid transparent',
            fontSize: '15px',
            fontWeight: 'bold',
            cursor: 'pointer',
            outline: 'none',
            transition: 'border-color 0.2s ease'
          }}
        >
          Crear Nueva Subasta
        </button>
      </div>

      {/* PESTAÑAS DE NAVEGACIÓN SUBRAYADAS */}
      <div style={{ display: 'flex', gap: '24px', marginBottom: '24px', borderBottom: '2px solid #b38b6d' }}>
        <button
          onClick={() => setPestanaActiva('publicadas')}
          style={{
            padding: '10px 4px',
            backgroundColor: 'transparent',
            color: '#000000',
            border: 'none',
            borderBottom: pestanaActiva === 'publicadas' ? '3px solid #000000' : '3px solid transparent',
            marginBottom: '-2px',
            fontSize: '16px',
            fontWeight: pestanaActiva === 'publicadas' ? 'bold' : '500',
            cursor: 'pointer',
            outline: 'none'
          }}
        >
          Mis Publicaciones
        </button>

        <button
          onClick={() => setPestanaActiva('participaciones')}
          style={{
            padding: '10px 4px',
            backgroundColor: 'transparent',
            color: '#000000',
            border: 'none',
            borderBottom: pestanaActiva === 'participaciones' ? '3px solid #000000' : '3px solid transparent',
            marginBottom: '-2px',
            fontSize: '16px',
            fontWeight: pestanaActiva === 'participaciones' ? 'bold' : '500',
            cursor: 'pointer',
            outline: 'none'
          }}
        >
          Mis Pujas
        </button>
      </div>

      {/* CONTENIDO DE PESTAÑAS */}
      {cargando ? (
        <p style={{ fontStyle: 'italic', color: '#000000' }}>Cargando datos desde PostgreSQL...</p>
      ) : pestanaActiva === 'publicadas' ? (
        <div>
          {subastasPublicadas.length === 0 ? (
            <div style={{ margin: '20px 0' }}>
              <p style={{ fontStyle: 'italic', color: '#000000', marginBottom: '16px', fontSize: '15px' }}>
                Aún no tenés subastas publicadas en la base de datos.
              </p>
              <button
                onClick={handleCrearClick}
                onMouseEnter={() => setHoverCrearEmpty(true)}
                onMouseLeave={() => setHoverCrearEmpty(false)}
                style={{
                  padding: '4px 0',
                  backgroundColor: 'transparent',
                  color: '#000000',
                  border: 'none',
                  borderBottom: hoverCrearEmpty ? '2px solid #000000' : '2px solid transparent',
                  fontSize: '14px',
                  fontWeight: 'bold',
                  cursor: 'pointer',
                  outline: 'none',
                  transition: 'border-color 0.2s ease'
                }}
              >
                Publicar mi primera subasta
              </button>
            </div>
          ) : (
            <div style={{ display: 'flex', flexDirection: 'column', gap: '14px' }}>
              {subastasPublicadas.map((item) => (
                <div
                  key={item.subastaId || item.id}
                  style={{
                    backgroundColor: '#FFD2B5',
                    padding: '18px',
                    borderRadius: '12px',
                    border: '1px solid #b38b6d'
                  }}
                >
                  <div style={{ display: 'flex', justifyContent: 'space-between', alignItems: 'flex-start', marginBottom: '10px' }}>
                    <div>
                      <h4 style={{ margin: 0, fontSize: '16px', fontWeight: 'bold', color: '#000000' }}>
                        {item.titulo}
                      </h4>
                      <span style={{ fontSize: '12px', color: '#000000', fontWeight: '500' }}>
                        Categoría: {item.nombreCategoria || item.categoria || 'General'}
                      </span>
                    </div>
                    <span style={{
                      padding: '4px 10px',
                      borderRadius: '6px',
                      fontSize: '12px',
                      fontWeight: 'bold',
                      backgroundColor: item.estado === 'ACTIVA' || item.estado === 'Activa' ? 'rgba(40, 167, 69, 0.2)' : 'rgba(108, 117, 125, 0.2)',
                      color: item.estado === 'ACTIVA' || item.estado === 'Activa' ? '#155724' : '#495057'
                    }}>
                      {item.estado}
                    </span>
                  </div>

                  <div style={{ display: 'grid', gridTemplateColumns: '1fr 1fr', gap: '10px', fontSize: '14px', marginBottom: '14px' }}>
                    <div>
                      <span style={{ display: 'block', fontSize: '12px', fontWeight: 'bold', color: '#000000' }}>Precio Inicial:</span>
                      <strong style={{ color: '#000000' }}>${(item.precioInicial || 0).toLocaleString('es-AR')}</strong>
                    </div>
                    <div>
                      <span style={{ display: 'block', fontSize: '12px', fontWeight: 'bold', color: '#000000' }}>Puja Líder:</span>
                      <strong style={{ color: '#000000' }}>${(item.ofertaMasAlta || item.precioInicial || 0).toLocaleString('es-AR')}</strong>
                    </div>
                  </div>

                  <div style={{ display: 'flex', justifyContent: 'space-between', alignItems: 'center', paddingTop: '10px', borderTop: '1px solid #b38b6d' }}>
                    <span style={{ fontSize: '12px', color: '#000000', fontWeight: '500' }}>
                      Cierre: {new Date(item.fechaFin).toLocaleString('es-AR')}
                    </span>
                    {onSeleccionarSubasta && (
                      <button
                        onClick={() => onSeleccionarSubasta(item.subastaId || item.id)}
                        style={{
                          padding: '8px 14px',
                          backgroundColor: '#1e1e1e',
                          color: '#ffffff',
                          border: 'none',
                          borderRadius: '8px',
                          fontSize: '13px',
                          fontWeight: 'bold',
                          cursor: 'pointer'
                        }}
                      >
                        Ver Detalle
                      </button>
                    )}
                  </div>
                </div>
              ))}
            </div>
          )}
        </div>
      ) : (
        <div>
          {misPujas.length === 0 ? (
            <p style={{ fontStyle: 'italic', color: '#000000', margin: '20px 0', fontSize: '15px' }}>
              Aún no realizaste pujas en ninguna subasta registrada en la base de datos.
            </p>
          ) : (
            <div style={{ display: 'flex', flexDirection: 'column', gap: '14px' }}>
              {misPujas.map((item) => (
                <div
                  key={item.subastaId || item.id}
                  style={{
                    backgroundColor: '#FFD2B5',
                    padding: '18px',
                    borderRadius: '12px',
                    border: '1px solid #b38b6d'
                  }}
                >
                  <div style={{ display: 'flex', justifyContent: 'space-between', alignItems: 'flex-start', marginBottom: '10px' }}>
                    <h4 style={{ margin: 0, fontSize: '16px', fontWeight: 'bold', color: '#000000' }}>
                      {item.titulo}
                    </h4>
                    <span style={{
                      padding: '4px 10px',
                      borderRadius: '6px',
                      fontSize: '12px',
                      fontWeight: 'bold',
                      backgroundColor: 'rgba(40, 167, 69, 0.2)',
                      color: '#155724'
                    }}>
                      {item.estado}
                    </span>
                  </div>

                  <div style={{ display: 'grid', gridTemplateColumns: '1fr 1fr', gap: '10px', fontSize: '14px', marginBottom: '14px' }}>
                    <div>
                      <span style={{ display: 'block', fontSize: '12px', fontWeight: 'bold', color: '#000000' }}>Puja Líder Actual:</span>
                      <strong style={{ color: '#000000' }}>${(item.ofertaMasAlta || 0).toLocaleString('es-AR')}</strong>
                    </div>
                    <div>
                      <span style={{ display: 'block', fontSize: '12px', fontWeight: 'bold', color: '#000000' }}>Total Pujas:</span>
                      <strong style={{ color: '#000000' }}>{item.cantidadPujas || 0}</strong>
                    </div>
                  </div>

                  <div style={{ display: 'flex', justifyContent: 'space-between', alignItems: 'center', paddingTop: '10px', borderTop: '1px solid #b38b6d' }}>
                    <span style={{ fontSize: '12px', color: '#000000', fontWeight: '500' }}>
                      Cierre: {new Date(item.fechaFin).toLocaleString('es-AR')}
                    </span>
                    {onSeleccionarSubasta && (
                      <button
                        onClick={() => onSeleccionarSubasta(item.subastaId || item.id)}
                        style={{
                          padding: '8px 14px',
                          backgroundColor: '#1e1e1e',
                          color: '#ffffff',
                          border: 'none',
                          borderRadius: '8px',
                          fontSize: '13px',
                          fontWeight: 'bold',
                          cursor: 'pointer'
                        }}
                      >
                        Ver Subasta
                      </button>
                    )}
                  </div>
                </div>
              ))}
            </div>
          )}
        </div>
      )}
    </div>
  );
}