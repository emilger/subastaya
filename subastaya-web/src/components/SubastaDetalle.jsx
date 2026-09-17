// src/components/SubastaDetalle.jsx
import React, { useState, useEffect } from 'react';
import * as api from '../api';

export function SubastaDetalle({ subasta, subastaId, usuarioId = 1, onVolver }) {
  const idObjetivo = subastaId || subasta?.subastaId || subasta?.id;
  const [detalleSubasta, setDetalleSubasta] = useState(subasta || null);
  const [montoPuja, setMontoPuja] = useState('');
  const [limiteAutoBid, setLimiteAutoBid] = useState('');
  const [cargando, setCargando] = useState(false);
  const [notificacion, setNotificacion] = useState(null);

  useEffect(() => {
    let montado = true;
    const cargarDetalle = async () => {
      if (!idObjetivo) return;
      setCargando(true);
      try {
        if (typeof api.getSubastaDetalle === 'function') {
          const res = await api.getSubastaDetalle(idObjetivo);
          const datos = res?.data || res;
          if (montado && datos && (datos.id || datos.subastaId || datos.titulo)) {
            setDetalleSubasta(datos);
          }
        }
      } catch (err) {
        console.warn('[API] Detalle remoto no disponible, utilizando datos locales:', err);
      } finally {
        if (montado) setCargando(false);
      }
    };

    cargarDetalle();
    return () => {
      montado = false;
    };
  }, [idObjetivo]);

  const obtenerNombreCategoria = (item) => {
    if (!item) return 'General';
    if (typeof item.nombreCategoria === 'string') return item.nombreCategoria;
    if (typeof item.categoria === 'string') return item.categoria;
    if (typeof item.categoria === 'object' && item.categoria !== null) {
      return item.categoria.nombre || item.categoria.nombreCategoria || 'General';
    }
    return 'General';
  };

  const obtenerEstado = (item) => {
    if (!item) return 'ACTIVA';
    if (typeof item.estado === 'string') return item.estado;
    if (typeof item.estado === 'object' && item.estado !== null) {
      return item.estado.nombre || item.estado.descripcion || 'ACTIVA';
    }
    return 'ACTIVA';
  };

  const mostrarMensaje = (texto, tipo = 'exito') => {
    setNotificacion({ texto, tipo });
    setTimeout(() => setNotificacion(null), 4000);
  };

  const handlePujaManual = async (e) => {
    e.preventDefault();
    const monto = parseFloat(montoPuja);
    if (!monto || isNaN(monto) || monto <= 0) {
      mostrarMensaje('Ingresá un monto válido para pujar.', 'error');
      return;
    }

    try {
      if (typeof api.realizarPuja === 'function') {
        await api.realizarPuja(idObjetivo, monto);
      }
      mostrarMensaje(`¡Puja por $${monto.toLocaleString('es-AR')} realizada con éxito!`, 'exito');
      setMontoPuja('');
    } catch (err) {
      mostrarMensaje(err.response?.data?.mensaje || 'Error al procesar la puja.', 'error');
    }
  };

  const handleConfigurarAutoBid = async (e) => {
    e.preventDefault();
    const limite = parseFloat(limiteAutoBid);
    if (!limite || isNaN(limite) || limite <= 0) {
      mostrarMensaje('Ingresá un límite máximo válido.', 'error');
      return;
    }

    try {
      if (typeof api.configurarPujaAutomatica === 'function') {
        await api.configurarPujaAutomatica(idObjetivo, limite);
      }
      mostrarMensaje(`¡Puja automática configurada con límite de $${limite.toLocaleString('es-AR')}!`, 'exito');
      setLimiteAutoBid('');
    } catch (err) {
      mostrarMensaje(err.response?.data?.mensaje || 'Error al configurar la puja automática.', 'error');
    }
  };

  const formatearFecha = (fechaStr) => {
    if (!fechaStr) return 'No especificada';
    try {
      const f = new Date(fechaStr);
      return isNaN(f.getTime()) ? String(fechaStr) : f.toLocaleString('es-AR');
    } catch (e) {
      return String(fechaStr);
    }
  };

  const formatearMonto = (val) => {
    const num = Number(val);
    return isNaN(num) ? '0' : num.toLocaleString('es-AR');
  };

  if (cargando) {
    return (
      <div style={{ maxWidth: '650px', margin: '0 auto', color: '#000000', padding: '20px 0', textAlign: 'center' }}>
        <p style={{ fontStyle: 'italic', color: '#000000', fontWeight: 'bold' }}>Cargando detalle de la subasta...</p>
      </div>
    );
  }

  const item = detalleSubasta || subasta;

  if (!item || (!item.titulo && !item.descripcion && !item.id && !item.subastaId)) {
    return (
      <div style={{ maxWidth: '650px', margin: '0 auto', color: '#000000', padding: '20px 0' }}>
        <p style={{ fontStyle: 'italic', color: '#000000' }}>No se encontró la información de esta subasta.</p>
      </div>
    );
  }

  const estadoTexto = obtenerEstado(item).toUpperCase();
  const tienePujas = item.ofertaMasAlta && item.ofertaMasAlta > item.precioInicial;
  const precioAMostrar = tienePujas ? item.ofertaMasAlta : (item.precioInicial || 0);
  const etiquetaPrecio = tienePujas ? 'Precio actual:' : 'Precio inicial:';
  const imagenSrc = item.urlImagen || item.imagenUrl || 'https://via.placeholder.com/600x300?text=Sin+Imagen';
  const nombreCategoria = obtenerNombreCategoria(item);

  return (
    <div style={{ maxWidth: '650px', margin: '0 auto', color: '#000000', padding: '10px 0' }}>
      {/* OCULTAR SPINNERS/FLECHAS NATIVAS EN INPUTS NUMÉRICOS */}
      <style>{`
        input[type=number]::-webkit-inner-spin-button, 
        input[type=number]::-webkit-outer-spin-button { 
          -webkit-appearance: none; 
          margin: 0; 
        }
        input[type=number] {
          -moz-appearance: textfield;
        }
      `}</style>

      {notificacion && (
        <div style={{
          padding: '12px 16px',
          backgroundColor: notificacion.tipo === 'exito' ? 'rgba(40, 167, 69, 0.2)' : 'rgba(220, 53, 69, 0.2)',
          border: `1px solid ${notificacion.tipo === 'exito' ? '#28a745' : '#dc3545'}`,
          color: notificacion.tipo === 'exito' ? '#155724' : '#721c24',
          borderRadius: '10px',
          marginBottom: '20px',
          fontWeight: 'bold',
          textAlign: 'center',
          fontSize: '14px'
        }}>
          {notificacion.texto}
        </div>
      )}

      {/* TARJETA DE DETALLE PRINCIPAL */}
      <div style={{ backgroundColor: '#FFD2B5', padding: '24px', borderRadius: '14px', border: '1px solid #b38b6d', marginBottom: '24px' }}>
        <div style={{ width: '100%', maxHeight: '320px', borderRadius: '10px', overflow: 'hidden', marginBottom: '18px', backgroundColor: '#E3C3B1' }}>
          <img
            src={imagenSrc}
            alt={item.titulo || 'Detalle de Subasta'}
            style={{ width: '100%', height: '100%', objectFit: 'cover', display: 'block' }}
            onError={(e) => {
              e.target.onerror = null;
              e.target.src = 'https://via.placeholder.com/600x300?text=Imagen+No+Disponible';
            }}
          />
        </div>

        <h2 style={{ marginTop: 0, marginBottom: '12px', fontSize: '24px', fontWeight: 'bold', color: '#000000' }}>
          {item.titulo || 'Subasta sin título'}
        </h2>

        {item.descripcion && (
          <p style={{ fontSize: '15px', color: '#333333', marginBottom: '16px', lineHeight: '1.5' }}>
            {item.descripcion}
          </p>
        )}

        <p style={{ fontSize: '14px', color: '#000000', marginBottom: '16px' }}>
          Categoría: <strong>{nombreCategoria}</strong>
        </p>

        <div style={{ display: 'grid', gridTemplateColumns: '1fr 1fr', gap: '16px', marginBottom: '16px' }}>
          <div>
            <span style={{ display: 'block', fontSize: '13px', fontWeight: 'bold', color: '#000000' }}>{etiquetaPrecio}</span>
            <span style={{ fontSize: '24px', fontWeight: 'bold', color: '#000000' }}>
              ${formatearMonto(precioAMostrar)}
            </span>
          </div>
          <div>
            <span style={{ display: 'block', fontSize: '13px', fontWeight: 'bold', color: '#000000' }}>Estado:</span>
            <span style={{
              fontSize: '15px',
              fontWeight: 'bold',
              color: estadoTexto === 'ACTIVA' ? '#155724' : '#495057'
            }}>
              {estadoTexto}
            </span>
          </div>
        </div>

        {item.fechaFin && (
          <p style={{ fontSize: '13px', color: '#000000', margin: 0 }}>
            Fecha de Cierre: <strong>{formatearFecha(item.fechaFin)}</strong>
          </p>
        )}
      </div>

      {/* BLOQUE DE OPCIONES DE PUJA SEGÚN ESTADO */}
      {estadoTexto === 'ACTIVA' && (
        <>
          {/* PUJA MANUAL */}
          <div style={{ backgroundColor: '#FFD2B5', padding: '20px', borderRadius: '12px', border: '1px solid #b38b6d', marginBottom: '20px' }}>
            <h3 style={{ marginTop: 0, marginBottom: '14px', fontSize: '18px', fontWeight: 'bold', color: '#000000' }}>
              Realizar Puja Manual
            </h3>
            <form onSubmit={handlePujaManual} style={{ display: 'flex', gap: '12px' }}>
              <input
                type="number"
                placeholder="Monto a pujar"
                value={montoPuja}
                onChange={(e) => setMontoPuja(e.target.value)}
                style={{ flex: 1, padding: '12px', borderRadius: '8px', border: '1px solid #b38b6d', color: '#000000', backgroundColor: '#ffffff', fontWeight: 'bold', outline: 'none' }}
              />
              <button
                type="submit"
                style={{ padding: '12px 20px', backgroundColor: '#1e1e1e', color: '#ffffff', border: 'none', borderRadius: '8px', fontWeight: 'bold', cursor: 'pointer' }}
              >
                Pujar
              </button>
            </form>
          </div>

          {/* PUJA AUTOMÁTICA */}
          <div style={{ backgroundColor: '#FFD2B5', padding: '20px', borderRadius: '12px', border: '1px solid #b38b6d' }}>
            <h3 style={{ marginTop: 0, marginBottom: '14px', fontSize: '18px', fontWeight: 'bold', color: '#000000' }}>
              Configurar Puja Automática
            </h3>
            <form onSubmit={handleConfigurarAutoBid} style={{ display: 'flex', gap: '12px' }}>
              <input
                type="number"
                placeholder="Límite máximo"
                value={limiteAutoBid}
                onChange={(e) => setLimiteAutoBid(e.target.value)}
                style={{ flex: 1, padding: '12px', borderRadius: '8px', border: '1px solid #b38b6d', color: '#000000', backgroundColor: '#ffffff', fontWeight: 'bold', outline: 'none' }}
              />
              <button
                type="submit"
                style={{ padding: '12px 20px', backgroundColor: '#1e1e1e', color: '#ffffff', border: 'none', borderRadius: '8px', fontWeight: 'bold', cursor: 'pointer' }}
              >
                Guardar Límite
              </button>
            </form>
          </div>
        </>
      )}

      {/* MENSAJE PARA SUBASTA PROGRAMADA */}
      {estadoTexto === 'PROGRAMADA' && (
        <div style={{ backgroundColor: '#FFD2B5', padding: '20px', borderRadius: '12px', border: '1px solid #b38b6d', textAlign: 'center' }}>
          <p style={{ margin: 0, fontSize: '15px', fontWeight: 'bold', color: '#000000' }}>
             Esta subasta empezará el <strong>{formatearFecha(item.fechaInicio)}</strong>.
          </p>
        </div>
      )}

      {/* MENSAJE PARA SUBASTAS FINALIZADAS O DESIERTAS */}
      {(estadoTexto === 'FINALIZADA' || estadoTexto === 'DESIERTA') && (
        <div style={{ backgroundColor: '#FFD2B5', padding: '20px', borderRadius: '12px', border: '1px solid #b38b6d', textAlign: 'center' }}>
          <p style={{ margin: 0, fontSize: '15px', fontWeight: 'bold', color: '#721c24' }}>
            {estadoTexto === 'FINALIZADA' ? ' La subasta ha finalizado. No se aceptan más pujas.' : ' Subasta declarada desierta.'}
          </p>
        </div>
      )}
    </div>
  );
}

export default SubastaDetalle;