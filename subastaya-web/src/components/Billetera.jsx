// src/components/Billetera.jsx
import React, { useState, useEffect } from 'react';
import { getBilletera, cargarSaldo, retirarSaldo, getMovimientos } from '../api';

const capitalizarTexto = (texto) => {
  if (!texto) return '';
  const limpio = texto.toString().replace(/_/g, ' ').trim();
  return limpio
    .toLowerCase()
    .split(' ')
    .filter(Boolean)
    .map((palabra) => palabra.charAt(0).toUpperCase() + palabra.slice(1))
    .join(' ');
};

export function Billetera({ usuarioId = 1 }) {
  const [billetera, setBilletera] = useState(() => {
    const guardado = localStorage.getItem('billeteraLocal');
    if (guardado) {
      try {
        return JSON.parse(guardado);
      } catch (e) {
        console.error(e);
      }
    }
    return { saldoDisponible: 10000, saldoRetenido: 2000 };
  });

  const [historial, setHistorial] = useState(() => {
    const guardado = localStorage.getItem('historialLocal');
    if (guardado) {
      try {
        return JSON.parse(guardado);
      } catch (e) {
        console.error(e);
      }
    }
    return [
      { id: 1, tipo: 'Depósito', monto: 10000, fecha: '17/09/2026 10:00 hs', estado: 'Completado' },
      { id: 2, tipo: 'Retención De Garantía', monto: -2000, fecha: '17/09/2026 10:30 hs', estado: 'En Custodia', esRetencion: true },
      { id: 3, tipo: 'Retiro', monto: -1500, fecha: '17/09/2026 11:00 hs', estado: 'Completado' }
    ];
  });

  const [cargando, setCargando] = useState(false);

  // Modales
  const [mostrarModalIngresar, setMostrarModalIngresar] = useState(false);
  const [mostrarModalRetirar, setMostrarModalRetirar] = useState(false);

  // Inputs numéricos
  const [montoIngreso, setMontoIngreso] = useState('');
  const [montoRetiro, setMontoRetiro] = useState('');

  // Notificaciones dinámicas
  const [notificacion, setNotificacion] = useState(null);
  const [errorModalIngreso, setErrorModalIngreso] = useState(null);
  const [errorModalRetiro, setErrorModalRetiro] = useState(null);

  const disponible = Number(billetera?.saldoDisponible ?? billetera?.SaldoDisponible ?? 0);
  const retenido = Number(billetera?.saldoRetenido ?? billetera?.SaldoRetenido ?? 0);

  useEffect(() => {
    localStorage.setItem('billeteraLocal', JSON.stringify({ saldoDisponible: disponible, saldoRetenido: retenido }));
  }, [disponible, retenido]);

  useEffect(() => {
    localStorage.setItem('historialLocal', JSON.stringify(historial));
  }, [historial]);

  const mostrarAvisoDinamico = (texto, tipo = 'exito') => {
    setNotificacion({ texto, tipo });
    setTimeout(() => {
      setNotificacion(null);
    }, 4000);
  };

  const cargarDatos = async () => {
    setCargando(true);
    try {
      const [resBilletera, resMovimientos] = await Promise.all([
        getBilletera(usuarioId),
        getMovimientos(usuarioId)
      ]);

      if (resBilletera?.data) {
        const data = resBilletera.data;
        const nuevoDisp = Number(data.saldoDisponible ?? data.SaldoDisponible ?? disponible);
        const nuevoRet = Number(data.saldoRetenido ?? data.SaldoRetenido ?? retenido);
        setBilletera({ saldoDisponible: nuevoDisp, saldoRetenido: nuevoRet });
      }

      if (Array.isArray(resMovimientos?.data) && resMovimientos.data.length > 0) {
        const movsFormat = resMovimientos.data.map((m) => ({
          id: m.id,
          tipo: capitalizarTexto(m.tipo),
          monto: Number(m.monto),
          fecha: new Date(m.fecha).toLocaleString('es-AR'),
          estado: 'Completado',
          esRetencion: m.tipo?.toString().toUpperCase().includes('RETENCION')
        }));
        setHistorial(movsFormat);
      }
    } catch (err) {
      console.warn('Backend offline o sin respuesta, operando con estado local.');
    } finally {
      setCargando(false);
    }
  };

  useEffect(() => {
    if (usuarioId) cargarDatos();
  }, [usuarioId]);

  const handleConfirmarIngreso = async (e) => {
    e.preventDefault();
    setErrorModalIngreso(null);
    const montoNum = parseFloat(montoIngreso);

    if (isNaN(montoNum) || montoNum <= 0) {
      setErrorModalIngreso('Por favor ingresá un monto válido mayor a $0.');
      return;
    }

    try {
      if (typeof cargarSaldo === 'function') {
        await cargarSaldo(usuarioId, montoNum);
      }
    } catch (err) {
      console.warn('Servidor offline: procesando depósito localmente.');
    }

    const nuevoDisponible = disponible + montoNum;
    setBilletera({ saldoDisponible: nuevoDisponible, saldoRetenido: retenido });

    const nuevoMovimiento = {
      id: Date.now(),
      tipo: 'Depósito',
      monto: montoNum,
      fecha: new Date().toLocaleString('es-AR'),
      estado: 'Completado'
    };

    setHistorial((prev) => [nuevoMovimiento, ...prev]);
    setMostrarModalIngresar(false);
    setMontoIngreso('');
    mostrarAvisoDinamico(`¡Se acreditaron $${montoNum.toLocaleString('es-AR')} correctamente a tu saldo!`, 'exito');
  };

  const handleConfirmarRetiro = async (e) => {
    e.preventDefault();
    setErrorModalRetiro(null);
    const montoNum = parseFloat(montoRetiro);

    if (isNaN(montoNum) || montoNum <= 0) {
      setErrorModalRetiro('Por favor ingresá un monto válido mayor a $0.');
      return;
    }

    if (montoNum > disponible) {
      setErrorModalRetiro(`Saldo disponible insuficiente. Podés retirar hasta $${disponible.toLocaleString('es-AR')}.`);
      return;
    }

    try {
      if (typeof retirarSaldo === 'function') {
        await retirarSaldo(usuarioId, montoNum);
      }
    } catch (err) {
      console.warn('Servidor offline: procesando retiro localmente.');
    }

    const nuevoDisponible = disponible - montoNum;
    setBilletera({ saldoDisponible: nuevoDisponible, saldoRetenido: retenido });

    const nuevoMovimiento = {
      id: Date.now(),
      tipo: 'Retiro',
      monto: -montoNum,
      fecha: new Date().toLocaleString('es-AR'),
      estado: 'Completado'
    };

    setHistorial((prev) => [nuevoMovimiento, ...prev]);
    setMostrarModalRetirar(false);
    setMontoRetiro('');
    mostrarAvisoDinamico(`¡Retiro procesado! Se descontaron $${montoNum.toLocaleString('es-AR')} de tu cuenta.`, 'exito');
  };

  const getEstilosMovimiento = (item) => {
    const tipoLower = item.tipo ? item.tipo.toLowerCase() : '';
    if (item.esRetencion || tipoLower.includes('retencion') || item.estado === 'En Custodia') {
      return { color: '#6c757d' };
    }
    if (item.monto < 0 || tipoLower.includes('retiro') || tipoLower.includes('pago')) {
      return { color: '#dc3545' };
    }
    return { color: '#155724' };
  };

  return (
    <div style={{ maxWidth: '650px', margin: '0 auto', color: '#000000', padding: '10px 0' }}>
      {/* Regla CSS para ocultar las flechitas (spinners) de los inputs de tipo number */}
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

      {/* BANNER DINÁMICO DE NOTIFICACIÓN INTEGRADA */}
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

      {/* 1. SECCIÓN SALDO */}
      <div style={{ marginBottom: '28px' }}>
        <h2 style={{ marginTop: 0, marginBottom: '16px', color: '#000000', fontSize: '24px', fontWeight: 'bold' }}>
          Saldo
        </h2>

        <div style={{ display: 'grid', gridTemplateColumns: '1fr 1fr', gap: '16px' }}>
          <div style={{ backgroundColor: '#FFD2B5', padding: '20px', borderRadius: '12px', border: '1px solid #b38b6d', textAlign: 'center' }}>
            <span style={{ display: 'block', fontSize: '13px', fontWeight: 'bold', color: '#000000', marginBottom: '6px' }}>DISPONIBLE</span>
            <span style={{ fontSize: '26px', fontWeight: 'bold', color: '#000000' }}>
              ${disponible.toLocaleString('es-AR')}
            </span>
          </div>

          <div style={{ backgroundColor: '#FFD2B5', padding: '20px', borderRadius: '12px', border: '1px solid #b38b6d', textAlign: 'center' }}>
            <span style={{ display: 'block', fontSize: '13px', fontWeight: 'bold', color: '#000000', marginBottom: '6px' }}>RETENIDO</span>
            <span style={{ fontSize: '26px', fontWeight: 'bold', color: '#000000' }}>
              ${retenido.toLocaleString('es-AR')}
            </span>
          </div>
        </div>
      </div>

      {/* 2. BOTONES DE ACCIÓN */}
      <div style={{ display: 'grid', gridTemplateColumns: '1fr 1fr', gap: '16px', marginBottom: '36px' }}>
        <button
          onClick={() => {
            setErrorModalIngreso(null);
            setMostrarModalIngresar(true);
          }}
          style={{ padding: '14px', backgroundColor: '#1e1e1e', color: '#ffffff', border: 'none', borderRadius: '10px', fontSize: '15px', fontWeight: 'bold', cursor: 'pointer' }}
        >
          Ingresar dinero
        </button>

        <button
          onClick={() => {
            setErrorModalRetiro(null);
            setMostrarModalRetirar(true);
          }}
          style={{ padding: '14px', backgroundColor: '#1e1e1e', color: '#ffffff', border: 'none', borderRadius: '10px', fontSize: '15px', fontWeight: 'bold', cursor: 'pointer' }}
        >
          Retirar dinero
        </button>
      </div>

      {/* 3. HISTORIAL DE MOVIMIENTOS */}
      <div>
        <h3 style={{ marginTop: 0, marginBottom: '16px', color: '#000000', fontSize: '20px', borderBottom: '2px solid #b38b6d', paddingBottom: '8px', fontWeight: 'bold' }}>
          Historial de Movimientos
        </h3>

        {cargando ? (
          <p style={{ fontStyle: 'italic', color: '#000000' }}>Cargando datos...</p>
        ) : historial.length === 0 ? (
          <p style={{ fontStyle: 'italic', color: '#000000' }}>No hay movimientos registrados.</p>
        ) : (
          <div style={{ display: 'flex', flexDirection: 'column', gap: '10px' }}>
            {historial.map((item) => {
              const estiloMonto = getEstilosMovimiento(item);
              const nombreCapitalizado = capitalizarTexto(item.tipo);
              return (
                <div key={item.id} style={{ backgroundColor: '#FFD2B5', padding: '14px 18px', borderRadius: '10px', display: 'flex', justifyContent: 'space-between', alignItems: 'center', border: '1px solid #b38b6d' }}>
                  <div style={{ textAlign: 'left', paddingLeft: 0, marginLeft: 0 }}>
                    <div style={{ fontWeight: 'bold', fontSize: '14px', color: '#000000', textAlign: 'left' }}>{nombreCapitalizado}</div>
                    <div style={{ fontSize: '12px', color: '#000000', marginTop: '4px', textAlign: 'left' }}>{item.fecha}</div>
                  </div>
                  <div style={{ textAlign: 'right' }}>
                    <div style={{ fontWeight: 'bold', fontSize: '15px', color: estiloMonto.color }}>
                      {item.monto > 0 ? `+ $${item.monto.toLocaleString('es-AR')}` : `- $${Math.abs(item.monto).toLocaleString('es-AR')}`}
                    </div>
                  </div>
                </div>
              );
            })}
          </div>
        )}
      </div>

      {/* MODAL INGRESAR DINERO */}
      {mostrarModalIngresar && (
        <div style={{ position: 'fixed', top: 0, left: 0, right: 0, bottom: 0, backgroundColor: 'rgba(0,0,0,0.5)', display: 'flex', alignItems: 'center', justifyContent: 'center', zIndex: 1000 }}>
          <div style={{ backgroundColor: '#E3C3B1', padding: '28px', borderRadius: '14px', width: '90%', maxWidth: '400px', border: '1px solid #b38b6d', color: '#000000' }}>
            <h3 style={{ marginTop: 0, marginBottom: '16px', fontSize: '20px', color: '#000000', fontWeight: 'bold' }}>Ingresar dinero</h3>

            {errorModalIngreso && (
              <div style={{ padding: '8px 12px', backgroundColor: 'rgba(220, 53, 69, 0.2)', border: '1px solid #dc3545', color: '#dc3545', borderRadius: '6px', marginBottom: '12px', fontSize: '13px', fontWeight: 'bold' }}>
                {errorModalIngreso}
              </div>
            )}

            <form onSubmit={handleConfirmarIngreso}>
              <label style={{ display: 'block', marginBottom: '8px', fontSize: '14px', fontWeight: 'bold', color: '#000000' }}>
                Monto a ingresar ($):
              </label>
              <input
                type="number"
                placeholder="Ingresá el monto (ej: 5000)"
                value={montoIngreso}
                onChange={(e) => setMontoIngreso(e.target.value)}
                min="1"
                required
                style={{ width: '100%', padding: '12px', borderRadius: '8px', border: '1px solid #b38b6d', backgroundColor: '#FFD2B5', color: '#000000', marginBottom: '20px', boxSizing: 'border-box', fontWeight: 'bold', outline: 'none' }}
              />

              <div style={{ display: 'flex', gap: '10px', justifyContent: 'flex-end' }}>
                <button
                  type="button"
                  onClick={() => setMostrarModalIngresar(false)}
                  style={{ padding: '10px 16px', backgroundColor: 'transparent', border: '1px solid #000000', color: '#000000', borderRadius: '6px', fontWeight: 'bold', cursor: 'pointer' }}
                >
                  Cancelar
                </button>
                <button
                  type="submit"
                  style={{ padding: '10px 20px', backgroundColor: '#1e1e1e', color: '#ffffff', border: 'none', borderRadius: '6px', fontWeight: 'bold', cursor: 'pointer' }}
                >
                  Confirmar Ingreso
                </button>
              </div>
            </form>
          </div>
        </div>
      )}

      {/* MODAL RETIRAR DINERO */}
      {mostrarModalRetirar && (
        <div style={{ position: 'fixed', top: 0, left: 0, right: 0, bottom: 0, backgroundColor: 'rgba(0,0,0,0.5)', display: 'flex', alignItems: 'center', justifyContent: 'center', zIndex: 1000 }}>
          <div style={{ backgroundColor: '#E3C3B1', padding: '28px', borderRadius: '14px', width: '90%', maxWidth: '400px', border: '1px solid #b38b6d', color: '#000000' }}>
            <h3 style={{ marginTop: 0, marginBottom: '12px', fontSize: '20px', color: '#000000', fontWeight: 'bold' }}>Retirar dinero</h3>
            <p style={{ fontSize: '13px', color: '#000000', marginBottom: '16px', fontWeight: '500' }}>
              Saldo disponible: <strong>${disponible.toLocaleString('es-AR')}</strong>
            </p>

            {errorModalRetiro && (
              <div style={{ padding: '8px 12px', backgroundColor: 'rgba(220, 53, 69, 0.2)', border: '1px solid #dc3545', color: '#dc3545', borderRadius: '6px', marginBottom: '12px', fontSize: '13px', fontWeight: 'bold' }}>
                {errorModalRetiro}
              </div>
            )}

            <form onSubmit={handleConfirmarRetiro}>
              <label style={{ display: 'block', marginBottom: '8px', fontSize: '14px', fontWeight: 'bold', color: '#000000' }}>
                Monto a retirar ($):
              </label>
              <input
                type="number"
                placeholder="Ingresá el monto (ej: 2000)"
                value={montoRetiro}
                onChange={(e) => setMontoRetiro(e.target.value)}
                min="1"
                max={disponible}
                required
                style={{ width: '100%', padding: '12px', borderRadius: '8px', border: '1px solid #b38b6d', backgroundColor: '#FFD2B5', color: '#000000', marginBottom: '20px', boxSizing: 'border-box', fontWeight: 'bold', outline: 'none' }}
              />

              <div style={{ display: 'flex', gap: '10px', justifyContent: 'flex-end' }}>
                <button
                  type="button"
                  onClick={() => setMostrarModalRetirar(false)}
                  style={{ padding: '10px 16px', backgroundColor: 'transparent', border: '1px solid #000000', color: '#000000', borderRadius: '6px', fontWeight: 'bold', cursor: 'pointer' }}
                >
                  Cancelar
                </button>
                <button
                  type="submit"
                  style={{ padding: '10px 20px', backgroundColor: '#1e1e1e', color: '#ffffff', border: 'none', borderRadius: '6px', fontWeight: 'bold', cursor: 'pointer' }}
                >
                  Confirmar Retiro
                </button>
              </div>
            </form>
          </div>
        </div>
      )}
    </div>
  );
}