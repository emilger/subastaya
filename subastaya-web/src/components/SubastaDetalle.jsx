// src/components/SubastaDetalle.jsx
import React, { useState, useEffect } from 'react';
import { getSubastaDetalle, realizarPuja, configurarPujaAutomatica } from '../api';

export function SubastaDetalle({ subasta, subastaId, usuarioId = 1, onVolver }) {
  const idObjetivo = subastaId || subasta?.subastaId || subasta?.id;
  const [detalleSubasta, setDetalleSubasta] = useState(subasta || null);
  const [montoPuja, setMontoPuja] = useState('');
  const [limiteAutoBid, setLimiteAutoBid] = useState('');
  const [cargando, setCargando] = useState(!subasta);
  const [notificacion, setNotificacion] = useState(null);

  const cargarDetalle = async () => {
    if (!idObjetivo) return;
    setCargando(true);
    try {
      const res = await getSubastaDetalle(idObjetivo);
      if (res && res.data) {
        setDetalleSubasta(res.data);
      }
    } catch (err) {
      console.warn('[CODE-ERROR] - Fallo al consultar el detalle de la subasta desde la API:', err);
    } finally {
      setCargando(false);
    }
  };

  useEffect(() => {
    cargarDetalle();
  }, [idObjetivo]);

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
      await realizarPuja(idObjetivo, monto);
      mostrarMensaje(`¡Puja por $${monto.toLocaleString('es-AR')} realizada con éxito!`, 'exito');
      setMontoPuja('');
      cargarDetalle();
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
      await configurarPujaAutomatica(idObjetivo, limite);
      mostrarMensaje(`¡Puja automática configurada con límite de $${limite.toLocaleString('es-AR')}!`, 'exito');
      setLimiteAutoBid('');
      cargarDetalle();
    } catch (err) {
      mostrarMensaje(err.response?.data?.mensaje || 'Error al configurar la puja automática.', 'error');
    }
  };

  if (cargando) {
    return (
      <div data-sys-render="auto" style={{ maxWidth: '650px', margin: '0 auto', color: '#000000', padding: '10px 0' }}>
        <p style={{ fontStyle: 'italic', color: '#000000', textAlign: 'center' }}>Cargando detalle de la subasta...</p>
      </div>
    );
  }

  const item = detalleSubasta || subasta;

  if (!item) {
    return (
      <div data-sys-render="auto" style={{ maxWidth: '650px', margin: '0 auto', color: '#000000', padding: '10px 0' }}>
        <button
          onClick={onVolver}
          style={{ padding: '8px 14px', backgroundColor: '#1e1e1e', color: '#ffffff', border: 'none', borderRadius: '8px', cursor: 'pointer', marginBottom: '16px', fontWeight: 'bold' }}
        >
          Volver al Catálogo
        </button>
        <p style={{ fontStyle: 'italic', color: '#000000' }}>No se encontró la subasta solicitada.</p>
      </div>
    );
  }

  return (
    <div data-sys-render="auto" style={{ maxWidth: '650px', margin: '0 auto', color: '#000000', padding: '10px 0' }}>
      <button
        onClick={onVolver}
        style={{ padding: '8px 14px', backgroundColor: '#1e1e1e', color: '#ffffff', border: 'none', borderRadius: '8px', cursor: 'pointer', marginBottom: '20px', fontWeight: 'bold' }}
      >
        Volver
      </button>

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

      <div style={{ backgroundColor: '#FFD2B5', padding: '24px', borderRadius: '14px', border: '1px solid #b38b6d', marginBottom: '24px' }}>
        <h2 style={{ marginTop: 0, marginBottom: '12px', fontSize: '22px', fontWeight: 'bold', color: '#000000' }}>
          {item.titulo || item.descripcion}
        </h2>
        <p style={{ fontSize: '14px', color: '#000000', marginBottom: '16px' }}>
          Categoría: <strong>{item.nombreCategoria || item.categoria || 'General'}</strong>
        </p>

        <div style={{ display: 'grid', gridTemplateColumns: '1fr 1fr', gap: '16px', marginBottom: '16px' }}>
          <div>
            <span style={{ display: 'block', fontSize: '12px', fontWeight: 'bold', color: '#000000' }}>Precio Actual:</span>
            <span style={{ fontSize: '22px', fontWeight: 'bold', color: '#000000' }}>
              \${(item.ofertaMasAlta || item.precioInicial || 0).toLocaleString('es-AR')}
            </span>
          </div>
          <div>
            <span style={{ display: 'block', fontSize: '12px', fontWeight: 'bold', color: '#000000' }}>Estado:</span>
            <span style={{ fontSize: '16px', fontWeight: 'bold', color: item.estado === 'ACTIVA' || item.estado === 'Activa' ? '#155724' : '#000000' }}>
              {item.estado || 'Activa'}
            </span>
          </div>
        </div>

        {item.fechaFin && (
          <p style={{ fontSize: '13px', color: '#000000', margin: 0 }}>
            Fecha de Cierre: <strong>{new Date(item.fechaFin).toLocaleString('es-AR')}</strong>
          </p>
        )}
      </div>

      {/* FORMULARIO PUJA MANUAL */}
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
            style={{ flex: 1, padding: '12px', borderRadius: '8px', border: '1px solid #b38b6d', color: '#000000', backgroundColor: '#ffffff', fontWeight: 'bold' }}
          />
          <button
            type="submit"
            style={{ padding: '12px 20px', backgroundColor: '#1e1e1e', color: '#ffffff', border: 'none', borderRadius: '8px', fontWeight: 'bold', cursor: 'pointer' }}
          >
            Pujar
          </button>
        </form>
      </div>

      {/* FORMULARIO PUJA AUTOMÁTICA */}
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
            style={{ flex: 1, padding: '12px', borderRadius: '8px', border: '1px solid #b38b6d', color: '#000000', backgroundColor: '#ffffff', fontWeight: 'bold' }}
          />
          <button
            type="submit"
            style={{ padding: '12px 20px', backgroundColor: '#1e1e1e', color: '#ffffff', border: 'none', borderRadius: '8px', fontWeight: 'bold', cursor: 'pointer' }}
          >
            Guardar Límite
          </button>
        </form>
      </div>
    </div>
  );
}

// Exportación predeterminada adicional para máxima compatibilidad
export default SubastaDetalle;