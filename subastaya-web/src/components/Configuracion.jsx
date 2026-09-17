// src/components/Configuracion.jsx
import React, { useState } from 'react';

export default function Configuracion() {
  const [nombre, setNombre] = useState('Usuario Subasta');
  const [email, setEmail] = useState('usuario@subastaya.com');
  const [limitePujaAuto, setLimitePujaAuto] = useState('50000');
  const [notificacionesEmail, setNotificacionesEmail] = useState(true);
  const [passwordActual, setPasswordActual] = useState('');
  const [nuevaPassword, setNuevaPassword] = useState('');
  const [mensaje, setMensaje] = useState('');

  const handleGuardarPerfil = (e) => {
    e.preventDefault();
    setMensaje(' Configuración guardada correctamente.');
    setTimeout(() => setMensaje(''), 3000);
  };

  const handleCambiarPassword = (e) => {
    e.preventDefault();
    if (!passwordActual || !nuevaPassword) {
      setMensaje('Error: Por favor completá ambos campos de contraseña.');
      return;
    }
    setMensaje(' Contraseña actualizada con éxito.');
    setPasswordActual('');
    setNuevaPassword('');
    setTimeout(() => setMensaje(''), 3000);
  };

  return (
    <div style={{ maxWidth: '650px', margin: '0 auto', backgroundColor: '#E3C3B1', padding: '30px', borderRadius: '12px', color: '#1a1a1a', boxShadow: '0 4px 16px rgba(0,0,0,0.15)' }}>
      <h2 style={{ textAlign: 'center', marginTop: 0, marginBottom: '24px', color: '#1a1a1a' }}>
        Configuración de la Cuenta
      </h2>

      {mensaje && (
        <div style={{ 
          padding: '12px', 
                  backgroundColor: mensaje.startsWith('✓') ? 'rgba(40, 167, 69, 0.2)' : 'rgba(220, 53, 69, 0.2)', 
                  border: `1px solid ${mensaje.startsWith('✓') ? '#28a745' : '#dc3545'}`, 
          color: '#1a1a1a', 
          borderRadius: '8px', 
          marginBottom: '20px', 
          textAlign: 'center', 
          fontWeight: 'bold' 
        }}>
          {mensaje}
        </div>
      )}

      {/* Sección 1: Datos Personales y Preferencias */}
      <form onSubmit={handleGuardarPerfil} style={{ marginBottom: '32px' }}>
        <h3 style={{ borderBottom: '1px solid #c49a7c', paddingBottom: '8px', color: '#1a1a1a' }}>Datos de Perfil</h3>
        
        <div style={{ marginBottom: '16px' }}>
          <label style={{ display: 'block', marginBottom: '6px', fontSize: '14px', color: '#333' }}>Nombre y Apellido</label>
          <input
            type="text"
            value={nombre}
            onChange={(e) => setNombre(e.target.value)}
            style={{ width: '100%', padding: '10px', borderRadius: '6px', border: '1px solid #b38b6d', backgroundColor: '#FFD2B5', color: '#1a1a1a', fontSize: '14px', boxSizing: 'border-box' }}
          />
        </div>

        <div style={{ marginBottom: '16px' }}>
          <label style={{ display: 'block', marginBottom: '6px', fontSize: '14px', color: '#333' }}>Correo Electrónico</label>
          <input
            type="email"
            value={email}
            onChange={(e) => setEmail(e.target.value)}
            style={{ width: '100%', padding: '10px', borderRadius: '6px', border: '1px solid #b38b6d', backgroundColor: '#FFD2B5', color: '#1a1a1a', fontSize: '14px', boxSizing: 'border-box' }}
          />
        </div>

        <h3 style={{ borderBottom: '1px solid #c49a7c', paddingBottom: '8px', marginTop: '24px', color: '#1a1a1a' }}>Preferencias de Subastas</h3>

        <div style={{ marginBottom: '16px' }}>
          <label style={{ display: 'block', marginBottom: '6px', fontSize: '14px', color: '#333' }}>Límite Máximo para Pujas Automáticas (\$)</label>
          <input
            type="number"
            value={limitePujaAuto}
            onChange={(e) => setLimitePujaAuto(e.target.value)}
            style={{ width: '100%', padding: '10px', borderRadius: '6px', border: '1px solid #b38b6d', backgroundColor: '#FFD2B5', color: '#1a1a1a', fontSize: '14px', boxSizing: 'border-box' }}
          />
        </div>

        <div style={{ marginBottom: '20px', display: 'flex', alignItems: 'center', gap: '10px' }}>
          <input
            type="checkbox"
            id="notif"
            checked={notificacionesEmail}
            onChange={(e) => setNotificacionesEmail(e.target.checked)}
            style={{ width: '18px', height: '18px', cursor: 'pointer' }}
          />
          <label htmlFor="notif" style={{ fontSize: '14px', color: '#333', cursor: 'pointer' }}>
            Recibir alertas cuando superen mi puja o finalice una subasta
          </label>
        </div>

        <button
          type="submit"
          style={{ padding: '12px 20px', backgroundColor: '#28a745', color: '#fff', border: 'none', borderRadius: '8px', fontSize: '14px', fontWeight: 'bold', cursor: 'pointer' }}
        >
          Guardar Cambios
        </button>
      </form>

      {/* Sección 2: Seguridad */}
      <form onSubmit={handleCambiarPassword}>
        <h3 style={{ borderBottom: '1px solid #c49a7c', paddingBottom: '8px', color: '#1a1a1a' }}>Seguridad</h3>

        <div style={{ marginBottom: '16px' }}>
          <label style={{ display: 'block', marginBottom: '6px', fontSize: '14px', color: '#333' }}>Contraseña Actual</label>
          <input
            type="password"
            value={passwordActual}
            onChange={(e) => setPasswordActual(e.target.value)}
            style={{ width: '100%', padding: '10px', borderRadius: '6px', border: '1px solid #b38b6d', backgroundColor: '#FFD2B5', color: '#1a1a1a', fontSize: '14px', boxSizing: 'border-box' }}
          />
        </div>

        <div style={{ marginBottom: '20px' }}>
          <label style={{ display: 'block', marginBottom: '6px', fontSize: '14px', color: '#333' }}>Nueva Contraseña</label>
          <input
            type="password"
            value={nuevaPassword}
            onChange={(e) => setNuevaPassword(e.target.value)}
            style={{ width: '100%', padding: '10px', borderRadius: '6px', border: '1px solid #b38b6d', backgroundColor: '#FFD2B5', color: '#1a1a1a', fontSize: '14px', boxSizing: 'border-box' }}
          />
        </div>

        <button
          type="submit"
          style={{ padding: '12px 20px', backgroundColor: '#007bff', color: '#fff', border: 'none', borderRadius: '8px', fontSize: '14px', fontWeight: 'bold', cursor: 'pointer' }}
        >
          Actualizar Contraseña
        </button>
      </form>
    </div>
  );
}