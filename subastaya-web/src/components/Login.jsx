// src/components/Login.jsx
import React, { useState } from 'react';

// Emails simulados ya registrados en el sistema
const EMAILS_REGISTRADOS_MOCK = [
  'usuario@subastaya.com',
  'vendedor@test.com',
  'comprador1@test.com',
  'comprador2@test.com'
];

export default function Login({ onLoginSuccess }) {
  const [esRegistro, setEsRegistro] = useState(false);

  // Campos del formulario
  const [nombre, setNombre] = useState('');
  const [email, setEmail] = useState('');
  const [password, setPassword] = useState('');
  const [confirmarPassword, setConfirmarPassword] = useState('');
  const [error, setError] = useState('');
  const [mensajeExito, setMensajeExito] = useState('');

  const handleSubmit = (e) => {
    e.preventDefault();
    setError('');
    setMensajeExito('');

    const emailLimpio = email.trim().toLowerCase();

    if (esRegistro) {
      // 1. Validación de campos vacíos
      if (!nombre.trim() || !emailLimpio || !password || !confirmarPassword) {
        setError('Por favor completá todos los campos.');
        return;
      }

      // 2. Validación de coincidencia de contraseña
      if (password !== confirmarPassword) {
        setError('Las contraseñas no coinciden.');
        return;
      }

      // 3. Validación de Email ya existente
      if (EMAILS_REGISTRADOS_MOCK.includes(emailLimpio)) {
        setError('El correo electrónico ya se encuentra registrado en el sistema.');
        return;
      }

      // Registro exitoso
      setMensajeExito('¡Cuenta creada con éxito! Iniciando sesión...');
      setTimeout(() => {
        localStorage.setItem('token', 'mock-token-12345');
        localStorage.setItem('lastActiveTime', Date.now().toString());
        onLoginSuccess();
      }, 1200);

    } else {
      // Validación de Login
      if (!emailLimpio || !password) {
        setError('Por favor ingresá tu email y contraseña.');
        return;
      }

      localStorage.setItem('token', 'mock-token-12345');
      localStorage.setItem('lastActiveTime', Date.now().toString());
      onLoginSuccess();
    }
  };

  const alternarModo = (modoRegistro) => {
    setEsRegistro(modoRegistro);
    setError('');
    setMensajeExito('');
  };

  return (
    <div style={{
      backgroundColor: '#CFA182',
      minHeight: '100vh',
      width: '100%',
      display: 'flex',
      alignItems: 'center',
      justifyContent: 'center',
      padding: '20px',
      boxSizing: 'border-box'
    }}>
      <div style={{
        backgroundColor: '#E3C3B1',
        padding: '40px',
        borderRadius: '16px',
        width: '100%',
        maxWidth: '420px',
        boxShadow: '0 8px 24px rgba(0, 0, 0, 0.15)',
        color: '#000000'
      }}>
        {/* Título de la Plataforma */}
        <h1 style={{
          textAlign: 'center',
          marginTop: 0,
          marginBottom: '8px',
          fontSize: '28px',
          fontWeight: 'bold',
          color: '#000000'
        }}>
          SubastaYA
        </h1>
        <p style={{
          textAlign: 'center',
          marginTop: 0,
          marginBottom: '24px',
          color: '#000000',
          fontSize: '14px'
        }}>
          {esRegistro ? 'Creá tu cuenta para empezar a pujar' : 'Ingresá a tu cuenta'}
        </p>

        {/* Pestañas Conmutables */}
        <div style={{
          display: 'flex',
          backgroundColor: '#c49a7c',
          borderRadius: '8px',
          padding: '4px',
          marginBottom: '24px'
        }}>
          <button
            type="button"
            onClick={() => alternarModo(false)}
            style={{
              flex: 1,
              padding: '10px 0',
              border: 'none',
              borderRadius: '6px',
              backgroundColor: !esRegistro ? '#FFD2B5' : 'transparent',
              color: '#000000',
              fontWeight: !esRegistro ? 'bold' : 'normal',
              cursor: 'pointer',
              fontSize: '14px',
              transition: 'all 0.2s ease'
            }}
          >
            Iniciar Sesión
          </button>
          <button
            type="button"
            onClick={() => alternarModo(true)}
            style={{
              flex: 1,
              padding: '10px 0',
              border: 'none',
              borderRadius: '6px',
              backgroundColor: esRegistro ? '#FFD2B5' : 'transparent',
              color: '#000000',
              fontWeight: esRegistro ? 'bold' : 'normal',
              cursor: 'pointer',
              fontSize: '14px',
              transition: 'all 0.2s ease'
            }}
          >
            Crear Cuenta
          </button>
        </div>

        {/* Alerta de Error */}
        {error && (
          <div style={{
            padding: '10px 14px',
            backgroundColor: 'rgba(220, 53, 69, 0.15)',
            border: '1px solid #dc3545',
            color: '#dc3545',
            borderRadius: '8px',
            marginBottom: '16px',
            fontSize: '13px',
            textAlign: 'center',
            fontWeight: 'bold'
          }}>
            {error}
          </div>
        )}

        {/* Alerta de Éxito */}
        {mensajeExito && (
          <div style={{
            padding: '10px 14px',
            backgroundColor: 'rgba(40, 167, 69, 0.15)',
            border: '1px solid #28a745',
            color: '#28a745',
            borderRadius: '8px',
            marginBottom: '16px',
            fontSize: '13px',
            textAlign: 'center',
            fontWeight: 'bold'
          }}>
            {mensajeExito}
          </div>
        )}

        {/* Formulario con campos #FFD2B5 */}
        <form onSubmit={handleSubmit}>
          {esRegistro && (
            <div style={{ marginBottom: '16px' }}>
              <label style={{ display: 'block', marginBottom: '6px', fontSize: '14px', fontWeight: 'bold', color: '#000000' }}>
                Nombre Completo
              </label>
              <input
                type="text"
                placeholder="Juan Pérez"
                value={nombre}
                onChange={(e) => setNombre(e.target.value)}
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
          )}

          <div style={{ marginBottom: '16px' }}>
            <label style={{ display: 'block', marginBottom: '6px', fontSize: '14px', fontWeight: 'bold', color: '#000000' }}>
              Correo Electrónico
            </label>
            <input
              type="email"
              placeholder="usuario@ejemplo.com"
              value={email}
              onChange={(e) => setEmail(e.target.value)}
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

          <div style={{ marginBottom: esRegistro ? '16px' : '24px' }}>
            <label style={{ display: 'block', marginBottom: '6px', fontSize: '14px', fontWeight: 'bold', color: '#000000' }}>
              Contraseña
            </label>
            <input
              type="password"
              placeholder="••••••••"
              value={password}
              onChange={(e) => setPassword(e.target.value)}
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

          {esRegistro && (
            <div style={{ marginBottom: '24px' }}>
              <label style={{ display: 'block', marginBottom: '6px', fontSize: '14px', fontWeight: 'bold', color: '#000000' }}>
                Confirmar Contraseña
              </label>
              <input
                type="password"
                placeholder="••••••••"
                value={confirmarPassword}
                onChange={(e) => setConfirmarPassword(e.target.value)}
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
          )}

          <button
            type="submit"
            style={{
              width: '100%',
              padding: '14px',
              backgroundColor: '#1e1e1e',
              color: '#ffffff',
              border: 'none',
              borderRadius: '8px',
              fontSize: '15px',
              fontWeight: 'bold',
              cursor: 'pointer',
              transition: 'background-color 0.2s ease'
            }}
          >
            {esRegistro ? 'Registrarse' : 'Ingresar'}
          </button>
        </form>
      </div>
    </div>
  );
}