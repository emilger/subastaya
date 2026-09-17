import { useState, useEffect } from 'react';
import { getBilletera, cargarSaldo } from '../api';

export function Billetera({ usuarioId }) {
  const [billetera, setBilletera] = useState(null);
  const [montoIngresado, setMontoIngresado] = useState('');
  const [cargando, setCargando] = useState(false);
  const [error, setError] = useState(null);

  // Consultar la API para traer la billetera del usuario
  const cargarDatos = async () => {
    try {
      setCargando(true);
      setError(null);
      const res = await getBilletera(usuarioId);
      setBilletera(res.data);
    } catch (err) {
      setError(err.response?.data?.mensaje || 'Error de conexión con el backend.');
    } finally {
      setCargando(false);
    }
  };

  useEffect(() => {
    if (usuarioId) cargarDatos();
  }, [usuarioId]);

  // Manejar el formulario de depósito
 const handleDepositar = async (e) => {
    e.preventDefault();
    const monto = parseFloat(montoIngresado);
    if (!monto || monto <= 0) return alert('Ingresá un monto válido');

    try {
      setCargando(true);
      const res = await cargarSaldo(usuarioId, monto);
      
      // Actualizar el estado directamente con la billetera devuelta por el POST
      setBilletera(res.data.billetera);
      setMontoIngresado('');
      alert(res.data.mensaje);
    } catch (err) {
      alert(err.response?.data?.mensaje || 'Error al realizar el depósito');
    } finally {
      setCargando(false);
    }
  };

  if (cargando && !billetera) return <p>Cargando billetera...</p>;

  return (
    <div style={{ border: '1px solid #ddd', padding: '20px', borderRadius: '8px', maxWidth: '400px', margin: '20px auto' }}>
      <h2>Mi Billetera (Usuario #{usuarioId})</h2>

      {error && <p style={{ color: 'red' }}>{error}</p>}

      {billetera && (
        <div style={{ textAlign: 'left', marginBottom: '20px' }}>
          <p><strong>Saldo Total:</strong> ${billetera.saldoTotal}</p>
          <p><strong>En Garantía (Retenido):</strong> ${billetera.saldoRetenido}</p>
          <p style={{ color: '#2e7d32', fontSize: '1.2rem' }}>
            <strong>Saldo Disponible:</strong> ${billetera.saldoDisponible}
          </p>
        </div>
      )}

      <form onSubmit={handleDepositar}>
        <h3>Cargar Saldo</h3>
        <input
          type="number"
          placeholder="Monto"
          value={montoIngresado}
          onChange={(e) => setMontoIngresado(e.target.value)}
          style={{ padding: '8px', marginRight: '8px', width: '60%' }}
        />
        <button type="submit" disabled={cargando} style={{ padding: '8px 16px' }}>
          Depositar
        </button>
      </form>
    </div>
  );
}