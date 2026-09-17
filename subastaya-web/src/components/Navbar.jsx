// src/components/Navbar.jsx
import React, { useState } from 'react';

export default function Navbar({ onLogout, onNavigate }) {
  const [dropdownOpen, setDropdownOpen] = useState(false);

  return (
    <header style={{
      display: 'flex',
      justify: 'space-between',
      alignItems: 'center',
      padding: '14px 24px',
      backgroundColor: '#1a1a1a',
      color: '#fff',
      boxShadow: '0 2px 10px rgba(0,0,0,0.3)',
      position: 'relative'
    }}>
      {/* Lado izquierdo vacío para equilibrar el flexbox y mantener el logo 100% centrado */}
      <div style={{ width: '130px' }}></div>

      {/* Logo Centrado: Al hacer clic vuelve al Catálogo */}
      <h2 
        onClick={() => onNavigate('catalogo')}
        style={{
          margin: 0,
          textAlign: 'center',
          flex: 1,
          letterSpacing: '2px',
          fontWeight: 'bold',
          color: '#f8f9fa',
          cursor: 'pointer',
          userSelect: 'none'
        }}
      >
        SUBASTA<span style={{ color: '#28a745' }}>YA</span>
      </h2>

      {/* Lado Derecho: Botón de Usuario */}
      <div style={{ width: '130px', display: 'flex', justifyContent: 'flex-end', position: 'relative' }}>
        <button
          onClick={() => setDropdownOpen(!dropdownOpen)}
          style={{
            backgroundColor: '#2d2d2d',
            color: '#fff',
            border: '1px solid #444',
            padding: '8px 16px',
            borderRadius: '20px',
            cursor: 'pointer',
            fontWeight: 'bold',
            display: 'flex',
            alignItems: 'center',
            gap: '6px'
          }}
        >
          👤 Mi Perfil ▾
        </button>

        {/* Menú Desplegable */}
        {dropdownOpen && (
          <div style={{
            position: 'absolute',
            top: '45px',
            right: '0',
            backgroundColor: '#222',
            border: '1px solid #444',
            borderRadius: '8px',
            width: '190px',
            boxShadow: '0px 6px 16px rgba(0,0,0,0.6)',
            zIndex: 1000,
            overflow: 'hidden'
          }}>
            <div
              onClick={() => {
                onNavigate('billetera'); // 👈 Redirige al menú de Billetera
                setDropdownOpen(false);
              }}
              style={{
                padding: '12px 16px',
                cursor: 'pointer',
                borderBottom: '1px solid #333',
                color: '#fff',
                transition: 'background 0.2s'
              }}
            >
              💳 Mi Billetera
            </div>
            <div
              onClick={() => {
                alert('Sección de Configuración en desarrollo.');
                setDropdownOpen(false);
              }}
              style={{
                padding: '12px 16px',
                cursor: 'pointer',
                borderBottom: '1px solid #333',
                color: '#fff'
              }}
            >
              ⚙️ Configuración
            </div>
            <div
              onClick={() => {
                setDropdownOpen(false);
                onLogout();
              }}
              style={{
                padding: '12px 16px',
                cursor: 'pointer',
                color: '#ff4d4d',
                fontWeight: 'bold'
              }}
            >
              🚪 Cerrar Sesión
            </div>
          </div>
        )}
      </div>
    </header>
  );
}