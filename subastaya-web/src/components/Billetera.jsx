// src/components/Billetera.jsx
import React, { useState, useEffect } from 'react';
import { getBilletera, cargarSaldo } from '../api';

// Helper para formato de fecha: dd/mm/aaaa hh:mm hs
const obtenerFechaFormateada = () => {
  const ahora = new Date();
  const dia = String(ahora.getDate()).padStart(2, '0');
  const mes = String(ahora.getMonth() + 1).padStart(2, '0');
  const anio = ahora.getFullYear();
  const horas = String(ahora.getHours()).padStart(2, '0');
  const minutos = String(ahora.getMinutes()).padStart(2, '0');
  return `${dia}/${mes}/${anio} ${horas}:${minutos} hs`;
};

export function Billetera({ usuarioId = 1 }) {
  // 1. Estado inicial de Billetera (LocalStorage o valores base)
  const [billetera, setBilletera] = useState(() => {
    const guardado = localStorage.getItem('datosBilletera');
    if (guardado) {
      try {
        const parsed = JSON.parse(guardado);
        if (parsed && typeof parsed.saldoDisponible === 'number') {
          return parsed;
        }
      } catch (e) {}
    }
    return { saldoRetenido: 2500, saldoDisponible: 6500 };
  });

  // 2. Estado inicial de Historial (LocalStorage o datos base)
  const [historial, setHistorial] = useState(() => {
    const guardado = localStorage.getItem('historialBilletera');
    if (guardado) {
      try {
        const parsed = JSON.parse(guardado);
        if (Array.isArray(parsed)) return parsed;
      } catch (e) {}
    }
    return [
      { id: 1, tipo: 'Depósito', monto: 10000, fecha: '15/09/2026 14:30 hs', estado: 'Completado', esRetencion: false },
      { id: 2, tipo: 'Saldo Retenido (Garantía Subasta #102)', monto: -2500, fecha: '16/09/2026 10:15 hs', estado: 'En Custodia', esRetencion: true },
      { id: 3, tipo: 'Pago de Subasta #101', monto: -4500, fecha: '16/09/2026 12:00 hs', estado: 'Completado', esRetencion: false },
      { id: 4, tipo: 'Devolución (Puja superada Subasta #98)', monto: 1500, fecha: '16/09/2026 18:00 hs', estado: 'Completado', esRetencion: false }
    ];
  });

  // Modales
  const [mostrarModalIngresar, setMostrarModalIngresar] = useState(false);
  const [mostrarModalRetirar, setMostrarModalRetirar] = useState(false);

  // Formulario
  const [opcionIngreso, setOpcionIngreso] = useState('5000');
  const [montoIngresoPersonalizado, setMontoIngresoPersonalizado] = useState('');
  const [montoRetiro, setMontoRetiro] = useState('');

  // Solo cargar desde backend si NO hay datos locales guardados
  useEffect(() => {
    let activo = true;
    const cargarDatosBackend = async () => {
      if (localStorage.getItem('datosBilletera')) return;
      try {
        const res = await getBilletera(usuarioId);
        if (activo && res?.data && typeof res.data.saldoDisponible === 'number') {
          setBilletera(res.data);
          localStorage.setItem('datosBilletera', JSON.stringify(res.data));
        }
      } catch (err) {
        // Mantiene el estado local inicial
      }
    };
    if (usuarioId) cargarDatosBackend();
    return () => { activo = false; };
  }, [usuarioId]);

  // Manejar Depósito (Ingresar dinero)
  const handleConfirmarIngreso = (e) => {
    e.preventDefault();
    const montoFinal = opcionIngreso === 'otro' ? parseFloat(montoIngresoPersonalizado) : parseFloat(opcionIngreso);

    if (!montoFinal || isNaN(montoFinal) || montoFinal <= 0) {
      alert('Por favor ingresá un monto válido.');
      return;
    }

    const nuevaTransaccion = {
      id: Date.now(),
      tipo: 'Depósito',
      monto: montoFinal,
      fecha: obtenerFechaFormateada(),
      estado: 'Completado',
      esRetencion: false
    };

    // 1. Actualización SÍNCRONA e INMEDIATA del saldo en React y LocalStorage
    setBilletera((prev) => {
      const disponibleActual = Number(prev?.saldoDisponible) || 0;
      const retenidoActual = Number(prev?.saldoRetenido) || 0;
      const nueva = {
        saldoRetenido: retenidoActual,
        saldoDisponible: disponibleActual + montoFinal
      };
      localStorage.setItem('datosBilletera', JSON.stringify(nueva));
      return nueva;
    });

    // 2. Actualización SÍNCRONA del historial
    setHistorial((prev) => {
      const nuevoHist = [nuevaTransaccion, ...prev];
      localStorage.setItem('historialBilletera', JSON.stringify(nuevoHist));
      return nuevoHist;
    });

    // 3. Cerrar emergente y resetear inputs
    setMostrarModalIngresar(false);
    setMontoIngresoPersonalizado('');
    setOpcionIngreso('5000');

    // 4. Notificar a la API en segundo plano sin congelar la interfaz
    cargarSaldo(usuarioId, montoFinal).catch(() => {});
  };

  // Manejar Retiro de dinero
  const handleConfirmarRetiro = (e) => {
    e.preventDefault();
    const monto = parseFloat(montoRetiro);

    if (!monto || isNaN(monto) || monto <= 0) {
      alert('Por favor ingresá un monto válido a retirar.');
      return;
    }

    const disponibleActual = Number(billetera?.saldoDisponible) || 0;
    if (monto > disponibleActual) {
      alert('El monto a retirar no puede superar tu saldo disponible.');
      return;
    }

    const nuevaTransaccion = {
      id: Date.now(),
      tipo: 'Retiro de dinero',
      monto: -monto,
      fecha: obtenerFechaFormateada(),
      estado: 'Completado',
      esRetencion: false
    };

    // 1. Actualización SÍNCRONA e INMEDIATA del saldo
    setBilletera((prev) => {
      const disp = Number(prev?.saldoDisponible) || 0;
      const ret = Number(prev?.saldoRetenido) || 0;
      const nueva = {
        saldoRetenido: ret,
        saldoDisponible: Math.max(0, disp - monto)
      };
      localStorage.setItem('datosBilletera', JSON.stringify(nueva));
      return nueva;
    });

    // 2. Actualización del historial
    setHistorial((prev) => {
      const nuevoHist = [nuevaTransaccion, ...prev];
      localStorage.setItem('historialBilletera', JSON.stringify(nuevoHist));
      return nuevoHist;
    });

    // 3. Cerrar emergente y resetear inputs
    setMontoRetiro('');
    setMostrarModalRetirar(false);
  };

  // Asignación de colores para el historial
  const getEstilosMovimiento = (item) => {
    if (item.esRetencion || item.estado === 'En Custodia') {
      return {
        colorMonto: '#6c757d',
        bgColorBadge: 'rgba(108, 117, 125, 0.2)',
        colorBadge: '#495057'
      };
    }
    if (item.monto < 0) {
      return {
        colorMonto: '#dc3545',
        bgColorBadge: 'rgba(220, 53, 69, 0.15)',
        colorBadge: '#dc3545'
      };
    }
    return {
      colorMonto: '#155724',
      bgColorBadge: 'rgba(40, 167, 69, 0.2)',
      colorBadge: '#155724'
    };
  };

  return (
    <div style={{ maxWidth: '650px', margin: '0 auto', color: '#000000', padding: '10px 0' }}>

      {/* 1. SECCIÓN SALDO */}
      <div style={{ marginBottom: '28px' }}>
        <h2 style={{ marginTop: 0, marginBottom: '16px', color: '#000000', fontSize: '24px', fontWeight: 'bold' }}>
          Saldo
        </h2>

        <div style={{ display: 'grid', gridTemplateColumns: '1fr 1fr', gap: '16px' }}>
          {/* Disponible */}
          <div style={{
            backgroundColor: '#FFD2B5',
            padding: '20px',
            borderRadius: '12px',
            border: '1px solid #b38b6d',
            textAlign: 'center'
          }}>
            <span style={{ display: 'block', fontSize: '13px', fontWeight: 'bold', color: '#4a4a4a', marginBottom: '6px' }}>
              DISPONIBLE
            </span>
            <span style={{ fontSize: '26px', fontWeight: 'bold', color: '#155724' }}>
              ${(Number(billetera?.saldoDisponible) || 0).toLocaleString('es-AR')}
            </span>
          </div>

          {/* Retenido */}
          <div style={{
            backgroundColor: '#FFD2B5',
            padding: '20px',
            borderRadius: '12px',
            border: '1px solid #b38b6d',
            textAlign: 'center'
          }}>
            <span style={{ display: 'block', fontSize: '13px', fontWeight: 'bold', color: '#4a4a4a', marginBottom: '6px' }}>
              RETENIDO
            </span>
            <span style={{ fontSize: '26px', fontWeight: 'bold', color: '#6c757d' }}>
              ${(Number(billetera?.saldoRetenido) || 0).toLocaleString('es-AR')}
            </span>
          </div>
        </div>
      </div>

      {/* 2. BOTONES DE ACCIÓN */}
      <div style={{ display: 'grid', gridTemplateColumns: '1fr 1fr', gap: '16px', marginBottom: '36px' }}>
        <button
          onClick={() => setMostrarModalIngresar(true)}
          style={{
            padding: '14px',
            backgroundColor: '#1e1e1e',
            color: '#ffffff',
            border: 'none',
            borderRadius: '10px',
            fontSize: '15px',
            fontWeight: 'bold',
            cursor: 'pointer'
          }}
        >
          Ingresar dinero
        </button>

        <button
          onClick={() => setMostrarModalRetirar(true)}
          style={{
            padding: '14px',
            backgroundColor: '#1e1e1e',
            color: '#ffffff',
            border: 'none',
            borderRadius: '10px',
            fontSize: '15px',
            fontWeight: 'bold',
            cursor: 'pointer'
          }}
        >
          Retirar dinero
        </button>
      </div>

      {/* 3. HISTORIAL */}
      <div>
        <h3 style={{ marginTop: 0, marginBottom: '16px', color: '#000000', fontSize: '20px', borderBottom: '2px solid #b38b6d', paddingBottom: '8px' }}>
          Historial
        </h3>

        {historial.length === 0 ? (
          <p style={{ fontStyle: 'italic', color: '#4a4a4a' }}>No hay movimientos registrados.</p>
        ) : (
          <div style={{ display: 'flex', flexDirection: 'column', gap: '10px' }}>
            {historial.map((item) => {
              const estilos = getEstilosMovimiento(item);
              return (
                <div
                  key={item.id}
                  style={{
                    backgroundColor: '#FFD2B5',
                    padding: '14px 18px',
                    borderRadius: '10px',
                    display: 'flex',
                    justifyContent: 'space-between',
                    alignItems: 'center',
                    border: '1px solid #b38b6d'
                  }}
                >
                  <div style={{
                    display: 'flex',
                    flexDirection: 'column',
                    alignItems: 'flex-start',
                    textAlign: 'left',
                    margin: 0,
                    padding: 0
                  }}>
                    <div style={{
                      fontWeight: 'bold',
                      fontSize: '14px',
                      color: '#000000',
                      margin: 0,
                      padding: 0,
                      textAlign: 'left'
                    }}>
                      {item.tipo}
                    </div>
                    <div style={{
                      fontSize: '12px',
                      color: '#555555',
                      marginTop: '4px',
                      margin: 0,
                      padding: 0,
                      textAlign: 'left'
                    }}>
                      {item.fecha}
                    </div>
                  </div>

                  <div style={{ textAlign: 'right' }}>
                    <div style={{
                      fontWeight: 'bold',
                      fontSize: '15px',
                      color: estilos.colorMonto
                    }}>
                      {item.monto > 0 ? `+ $${item.monto.toLocaleString('es-AR')}` : `- $${Math.abs(item.monto).toLocaleString('es-AR')}`}
                    </div>
                    <span style={{
                      fontSize: '11px',
                      fontWeight: 'bold',
                      padding: '2px 6px',
                      borderRadius: '4px',
                      backgroundColor: estilos.bgColorBadge,
                      color: estilos.colorBadge,
                      display: 'inline-block',
                      marginTop: '2px'
                    }}>
                      {item.estado}
                    </span>
                  </div>
                </div>
              );
            })}
          </div>
        )}
      </div>

      {/* MODAL 1: INGRESAR DINERO */}
      {mostrarModalIngresar && (
        <div style={{
          position: 'fixed',
          top: 0, left: 0, right: 0, bottom: 0,
          backgroundColor: 'rgba(0, 0, 0, 0.5)',
          display: 'flex',
          alignItems: 'center',
          justifyContent: 'center',
          zIndex: 1000
        }}>
          <div style={{
            backgroundColor: '#E3C3B1',
            padding: '28px',
            borderRadius: '14px',
            width: '90%',
            maxWidth: '400px',
            border: '1px solid #b38b6d',
            boxShadow: '0 8px 24px rgba(0,0,0,0.3)',
            color: '#000000'
          }}>
            <h3 style={{ marginTop: 0, marginBottom: '16px', fontSize: '20px', color: '#000000' }}>
              Ingresar dinero
            </h3>

            <form onSubmit={handleConfirmarIngreso}>
              <div style={{ marginBottom: '16px' }}>
                <label style={{ display: 'block', marginBottom: '8px', fontSize: '14px', fontWeight: 'bold' }}>
                  Seleccioná o ingresá el monto:
                </label>
                <select
                  value={opcionIngreso}
                  onChange={(e) => setOpcionIngreso(e.target.value)}
                  style={{
                    width: '100%',
                    padding: '12px',
                    borderRadius: '8px',
                    border: '1px solid #b38b6d',
                    backgroundColor: '#FFD2B5',
                    color: '#000000',
                    fontSize: '14px',
                    fontWeight: 'bold',
                    outline: 'none',
                    marginBottom: '12px'
                  }}
                >
                  <option value="1000">\$1.000 ARS</option>
                  <option value="5000">\$5.000 ARS</option>
                  <option value="10000">\$10.000 ARS</option>
                  <option value="25000">\$25.000 ARS</option>
                  <option value="otro">Otro monto...</option>
                </select>

                {opcionIngreso === 'otro' && (
                  <input
                    type="number"
                    placeholder="Monto personalizado"
                    value={montoIngresoPersonalizado}
                    onChange={(e) => setMontoIngresoPersonalizado(e.target.value)}
                    style={{
                      width: '100%',
                      padding: '12px',
                      borderRadius: '8px',
                      border: '1px solid #b38b6d',
                      backgroundColor: '#FFD2B5',
                      color: '#000000',
                      fontSize: '14px',
                      boxSizing: 'border-box',
                      outline: 'none'
                    }}
                  />
                )}
              </div>

              <div style={{ display: 'flex', gap: '10px', justifyContent: 'flex-end' }}>
                <button
                  type="button"
                  onClick={() => setMostrarModalIngresar(false)}
                  style={{
                    padding: '10px 16px',
                    backgroundColor: 'transparent',
                    border: '1px solid #777',
                    borderRadius: '6px',
                    cursor: 'pointer',
                    color: '#000000',
                    fontWeight: 'bold'
                  }}
                >
                  Cancelar
                </button>
                <button
                  type="submit"
                  style={{
                    padding: '10px 20px',
                    backgroundColor: '#1e1e1e',
                    color: '#ffffff',
                    border: 'none',
                    borderRadius: '6px',
                    cursor: 'pointer',
                    fontWeight: 'bold'
                  }}
                >
                  Confirmar
                </button>
              </div>
            </form>
          </div>
        </div>
      )}

      {/* MODAL 2: RETIRAR DINERO */}
      {mostrarModalRetirar && (
        <div style={{
          position: 'fixed',
          top: 0, left: 0, right: 0, bottom: 0,
          backgroundColor: 'rgba(0, 0, 0, 0.5)',
          display: 'flex',
          alignItems: 'center',
          justifyContent: 'center',
          zIndex: 1000
        }}>
          <div style={{
            backgroundColor: '#E3C3B1',
            padding: '28px',
            borderRadius: '14px',
            width: '90%',
            maxWidth: '400px',
            border: '1px solid #b38b6d',
            boxShadow: '0 8px 24px rgba(0,0,0,0.3)',
            color: '#000000'
          }}>
            <h3 style={{ marginTop: 0, marginBottom: '16px', fontSize: '20px', color: '#000000' }}>
              Retirar dinero
            </h3>

            <form onSubmit={handleConfirmarRetiro}>
              <div style={{ marginBottom: '16px' }}>
                <label style={{ display: 'block', marginBottom: '8px', fontSize: '14px', fontWeight: 'bold' }}>
                  Monto a retirar (\$):
                </label>
                <input
                  type="number"
                  placeholder="Ej: 3000"
                  value={montoRetiro}
                  onChange={(e) => setMontoRetiro(e.target.value)}
                  style={{
                    width: '100%',
                    padding: '12px',
                    borderRadius: '8px',
                    border: '1px solid #b38b6d',
                    backgroundColor: '#FFD2B5',
                    color: '#000000',
                    fontSize: '14px',
                    boxSizing: 'border-box',
                    outline: 'none'
                  }}
                />
              </div>

              <div style={{ display: 'flex', gap: '10px', justifyContent: 'flex-end' }}>
                <button
                  type="button"
                  onClick={() => setMostrarModalRetirar(false)}
                  style={{
                    padding: '10px 16px',
                    backgroundColor: 'transparent',
                    border: '1px solid #777',
                    borderRadius: '6px',
                    cursor: 'pointer',
                    color: '#000000',
                    fontWeight: 'bold'
                  }}
                >
                  Cancelar
                </button>
                <button
                  type="submit"
                  style={{
                    padding: '10px 20px',
                    backgroundColor: '#1e1e1e',
                    color: '#ffffff',
                    border: 'none',
                    borderRadius: '6px',
                    cursor: 'pointer',
                    fontWeight: 'bold'
                  }}
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