import { useState } from 'react';
import { realizarPuja, configurarPujaAutomatica } from '../api';

export function SubastaDetalle({ subasta, usuarioId, onVolver, onActualizar }) {
  const [montoPuja, setMontoPuja] = useState('');
  const [montoMaximoAuto, setMontoMaximoAuto] = useState('');
  const [cargando, setCargando] = useState(false);
  const [mensaje, setMensaje] = useState(null);
  const [error, setError] = useState(null);

  const ofertaMinimaSugerida = (subasta.precioActual || subasta.precioBase) + 1;

  // Manejar Puja Manual
  const handlePujaManual = async (e) => {
    e.preventDefault();
    const monto = parseFloat(montoPuja);
    if (!monto || monto < ofertaMinimaSugerida) {
      return alert(`La oferta debe ser de al menos $${ofertaMinimaSugerida}`);
    }

    try {
      setCargando(true);
      setError(null);
      setMensaje(null);
      const res = await realizarPuja(subasta.subastaId || subasta.id, usuarioId, monto);
      setMensaje(res.data.mensaje || '¡Puja realizada con éxito!');
      setMontoPuja('');
      if (onActualizar) onActualizar();
    } catch (err) {
      setError(err.response?.data?.mensaje || 'Error al realizar la puja.');
    } finally {
      setCargando(false);
    }
  };

  // Manejar Puja Automática (Proxy Bidding)
  const handlePujaAutomatica = async (e) => {
    e.preventDefault();
    const montoMax = parseFloat(montoMaximoAuto);
    if (!montoMax || montoMax < ofertaMinimaSugerida) {
      return alert(`El monto máximo debe ser superior a $${ofertaMinimaSugerida}`);
    }

    try {
      setCargando(true);
      setError(null);
      setMensaje(null);
      const res = await configurarPujaAutomatica(subasta.subastaId || subasta.id, usuarioId, montoMax);
      setMensaje(res.data.mensaje || '¡Puja automática configurada!');
      setMontoMaximoAuto('');
      if (onActualizar) onActualizar();
    } catch (err) {
      setError(err.response?.data?.mensaje || 'Error al configurar la puja automática.');
    } finally {
      setCargando(false);
    }
  };

  return (
    <div style={{ maxWidth: '600px', margin: '20px auto', padding: '20px', border: '1px solid #ccc', borderRadius: '8px', backgroundColor: '#fff' }}>
      <button onClick={onVolver} style={{ padding: '6px 12px', cursor: 'pointer', marginBottom: '15px' }}>
        ⬅ Volver al Catálogo
      </button>

      <h2>{subasta.titulo}</h2>
      <p style={{ color: '#666' }}>{subasta.descripcion}</p>

      <div style={{ backgroundColor: '#f5f5f5', padding: '15px', borderRadius: '6px', margin: '15px 0' }}>
        <p><strong>Precio Base:</strong> ${subasta.precioBase}</p>
        <p style={{ fontSize: '1.3rem', color: '#2e7d32' }}>
          <strong>Oferta Actual:</strong> ${subasta.precioActual || subasta.precioBase}
        </p>
      </div>

      {mensaje && <p style={{ color: 'green', fontWeight: 'bold' }}>{mensaje}</p>}
      {error && <p style={{ color: 'red', fontWeight: 'bold' }}>{error}</p>}

      <div style={{ display: 'grid', gridTemplateColumns: '1fr 1fr', gap: '20px', marginTop: '20px' }}>
        {/* Formulario Puja Manual */}
        <form onSubmit={handlePujaManual} style={{ border: '1px solid #e0e0e0', padding: '15px', borderRadius: '6px' }}>
          <h4>Puja Manual</h4>
          <input
            type="number"
            placeholder={`Mínimo $${ofertaMinimaSugerida}`}
            value={montoPuja}
            onChange={(e) => setMontoPuja(e.target.value)}
            style={{ width: '90%', padding: '8px', marginBottom: '10px' }}
          />
          <button type="submit" disabled={cargando} style={{ width: '100%', padding: '8px', backgroundColor: '#2e7d32', color: '#fff', border: 'none', borderRadius: '4px', cursor: 'pointer' }}>
            Ofertar
          </button>
        </form>

        {/* Formulario Puja Automática */}
        <form onSubmit={handlePujaAutomatica} style={{ border: '1px solid #e0e0e0', padding: '15px', borderRadius: '6px' }}>
          <h4>Puja Automática 🤖</h4>
          <input
            type="number"
            placeholder="Monto Máximo"
            value={montoMaximoAuto}
            onChange={(e) => setMontoMaximoAuto(e.target.value)}
            style={{ width: '90%', padding: '8px', marginBottom: '10px' }}
          />
          <button type="submit" disabled={cargando} style={{ width: '100%', padding: '8px', backgroundColor: '#0288d1', color: '#fff', border: 'none', borderRadius: '4px', cursor: 'pointer' }}>
            Activar Bot
          </button>
        </form>
      </div>
    </div>
  );
}