// src/components/Configuracion.jsx
import React, { useState } from 'react';

export default function Configuracion() {
  // Estado del perfil sin teléfono de contacto
  const [perfil, setPerfil] = useState(() => {
    const guardado = localStorage.getItem('perfilUsuario');
    return guardado ? JSON.parse(guardado) : {
      nombre: 'Usuario SubastaYa',
      email: 'usuario@subastaya.com',
      notifPujas: true,
      notifCierre: true,
      notifOfertas: false
    };
  });

  const [passwordActual, setPasswordActual] = useState('');
  const [passwordNueva, setPasswordNueva] = useState('');
  const [mensajeExito, setMensajeExito] = useState(null);

  const handleChangeInput = (e) => {
    const { name, value, type, checked } = e.target;
    setPerfil(prev => ({
      ...prev,
      [name]: type === 'checkbox' ? checked : value
    }));
  };

  const handleGuardarPerfil = (e) => {
    e.preventDefault();
    localStorage.setItem('perfilUsuario', JSON.stringify(perfil));
    setMensajeExito('¡Configuración guardada correctamente!');
    setTimeout(() => setMensajeExito(null), 3000);
  };

  const handleCambiarPassword = (e) => {
    e.preventDefault();
    if (!passwordActual || !passwordNueva) {
      alert('Por favor completá los dos campos de contraseña.');
      return;
    }
    alert('¡Contraseña actualizada con éxito!');
    setPasswordActual('');
    setPasswordNueva('');
  };

  return (
    <div style={{ maxWidth: '650px', margin: '0 auto', color: '#000000', padding: '10px 0' }}>
      <h2 style={{ marginTop: 0, marginBottom: '24px', color: '#000000', fontSize: '24px', fontWeight: 'bold' }}>
        Configuración
      </h2>

      {mensajeExito && (
        <div style={{
          padding: '12px',
          backgroundColor: 'rgba(40, 167, 69, 0.2)',
          border: '1px solid #28a745',
          color: '#155724',
          borderRadius: '8px',
          marginBottom: '20px',
          fontWeight: 'bold',
          textAlign: 'center'
        }}>
          {mensajeExito}
        </div>
      )}

      {/* 1. DATOS PERSONALES */}
      <form onSubmit={handleGuardarPerfil} style={{ marginBottom: '32px' }}>
        <h3 style={{ marginTop: 0, marginBottom: '16px', fontSize: '18px', color: '#000000', borderBottom: '2px solid #b38b6d', paddingBottom: '8px' }}>
          Datos Personales
        </h3>

        <div style={{ marginBottom: '16px' }}>
          <label style={{ display: 'block', marginBottom: '6px', fontSize: '14px', fontWeight: 'bold' }}>
            Nombre Completo:
          </label>
          <input
            type="text"
            name="nombre"
            value={perfil.nombre}
            onChange={handleChangeInput}
            style={{
              width: '100%',
              padding: '12px',
              borderRadius: '8px',
              border: '1px solid #b38b6d',
              backgroundColor: '#FFD2B5',
              color: '#000000',
              fontSize: '14px',
              boxSizing: 'border-box',
              outline: 'none',
              fontWeight: '500'
            }}
          />
        </div>

        <div style={{ marginBottom: '24px' }}>
          <label style={{ display: 'block', marginBottom: '6px', fontSize: '14px', fontWeight: 'bold' }}>
            Correo Electrónico:
          </label>
          <input
            type="email"
            name="email"
            value={perfil.email}
            onChange={handleChangeInput}
            style={{
              width: '100%',
              padding: '12px',
              borderRadius: '8px',
              border: '1px solid #b38b6d',
              backgroundColor: '#FFD2B5',
              color: '#000000',
              fontSize: '14px',
              boxSizing: 'border-box',
              outline: 'none',
              fontWeight: '500'
            }}
          />
        </div>

        {/* 2. PREFERENCIAS DE NOTIFICACIÓN */}
        <h3 style={{ marginTop: 0, marginBottom: '16px', fontSize: '18px', color: '#000000', borderBottom: '2px solid #b38b6d', paddingBottom: '8px' }}>
          Preferencias de Notificación
        </h3>

        <label style={{ display: 'flex', alignItems: 'center', gap: '10px', marginBottom: '12px', cursor: 'pointer', fontSize: '14px', fontWeight: '500' }}>
          <input
            type="checkbox"
            name="notifPujas"
            checked={perfil.notifPujas}
            onChange={handleChangeInput}
            style={{ width: '18px', height: '18px', accentColor: '#1e1e1e' }}
          />
          Notificarme cuando superen mi puja en una subasta activa
        </label>

        <label style={{ display: 'flex', alignItems: 'center', gap: '10px', marginBottom: '12px', cursor: 'pointer', fontSize: '14px', fontWeight: '500' }}>
          <input
            type="checkbox"
            name="notifCierre"
            checked={perfil.notifCierre}
            onChange={handleChangeInput}
            style={{ width: '18px', height: '18px', accentColor: '#1e1e1e' }}
          />
          Avisarme cuando una subasta en la que participo esté por finalizar
        </label>

        <label style={{ display: 'flex', alignItems: 'center', gap: '10px', marginBottom: '24px', cursor: 'pointer', fontSize: '14px', fontWeight: '500' }}>
          <input
            type="checkbox"
            name="notifOfertas"
            checked={perfil.notifOfertas}
            onChange={handleChangeInput}
            style={{ width: '18px', height: '18px', accentColor: '#1e1e1e' }}
          />
          Recibir novedades y avisos importantes
        </label>

        <button
          type="submit"
          style={{
            width: '100%',
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
          Guardar Cambios
        </button>
      </form>

      {/* 3. SEGURIDAD Y CONTRASEÑA */}
      <h3 style={{ marginTop: 0, marginBottom: '16px', fontSize: '18px', color: '#000000', borderBottom: '2px solid #b38b6d', paddingBottom: '8px' }}>
        Seguridad y Contraseña
      </h3>

      <form onSubmit={handleCambiarPassword}>
        <div style={{ marginBottom: '16px' }}>
          <label style={{ display: 'block', marginBottom: '6px', fontSize: '14px', fontWeight: 'bold' }}>
            Contraseña Actual:
          </label>
          <input
            type="password"
            placeholder="••••••••"
            value={passwordActual}
            onChange={(e) => setPasswordActual(e.target.value)}
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

        <div style={{ marginBottom: '24px' }}>
          <label style={{ display: 'block', marginBottom: '6px', fontSize: '14px', fontWeight: 'bold' }}>
            Nueva Contraseña:
          </label>
          <input
            type="password"
            placeholder="••••••••"
            value={passwordNueva}
            onChange={(e) => setPasswordNueva(e.target.value)}
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

        <button
          type="submit"
          style={{
            width: '100%',
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
          Actualizar Contraseña
        </button>
      </form>

    </div>
  );
}
