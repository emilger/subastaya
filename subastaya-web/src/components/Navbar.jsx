// src/components/Navbar.jsx
import React, { useState } from 'react';

export default function Navbar({ onLogout, onNavigate }) {
  const [dropdownAbierto, setDropdownAbierto] = useState(false);

  const handleNavegar = (vista) => {
    onNavigate(vista);
    setDropdownAbierto(false);
  };

  const handleLogoutMenu = () => {
    onLogout();
    setDropdownAbierto(false);
  };

  return (
    <nav style={{
      backgroundColor: '#1e1e1e',
      padding: '16px 30px',
      display: 'flex',
      alignItems: 'center',
      justifyContent: 'space-between',
      color: '#fff',
      boxShadow: '0 2px 10px rgba(0,0,0,0.4)',
      position: 'relative'
    }}>
      {/* Espacio vacío a la izquierda para garantizar el centrado exacto */}
      <div style={{ flex: 1 }} />

      {/* Nombre de la página centrado ("Subasta" en blanco y "YA" en verde flúor) */}
      <div 
        onClick={() => handleNavegar('catalogo')} 
        style={{ 
          cursor: 'pointer', 
          fontSize: '22px', 
          fontWeight: 'bold', 
          textAlign: 'center',
          flex: 1
        }}
      >
        <span style={{ color: '#ffffff' }}>Subasta</span>
        <span style={{ color: '#00FF66' }}>YA</span>
      </div>

      {/* Menú Desplegable "Mi Cuenta" a la derecha */}
      <div style={{ flex: 1, display: 'flex', justifyContent: 'flex-end', position: 'relative' }}>
        <button
          onClick={() => setDropdownAbierto(!dropdownAbierto)}
          style={{
            background: 'none',
            border: 'none',
            color: '#fff',
            fontSize: '15px',
            cursor: 'pointer',
            fontWeight: '500',
            display: 'flex',
            alignItems: 'center',
            gap: '6px'
          }}
        >
          Mi Cuenta ▾
        </button>

        {/* Lista desplegable */}
        {dropdownAbierto && (
          <div style={{
            position: 'absolute',
            right: 0,
            top: '130%',
            backgroundColor: '#2b2b2b',
            borderRadius: '8px',
            boxShadow: '0 4px 16px rgba(0,0,0,0.5)',
            minWidth: '180px',
            overflow: 'hidden',
            zIndex: 1000,
            border: '1px solid #444'
          }}>
            {/* 1. Mis subastas */}
            <button
              onClick={() => handleNavegar('mis-subastas')}
              style={{
                width: '100%',
                padding: '12px 16px',
                background: 'none',
                border: 'none',
                color: '#fff',
                textAlign: 'left',
                cursor: 'pointer',
                fontSize: '14px',
                transition: 'background-color 0.2s ease'
              }}
              onMouseEnter={(e) => e.target.style.backgroundColor = '#3a3a3a'}
              onMouseLeave={(e) => e.target.style.backgroundColor = 'transparent'}
            >
              Mis subastas
            </button>

            {/* 2. Mi billetera */}
            <button
              onClick={() => handleNavegar('billetera')}
              style={{
                width: '100%',
                padding: '12px 16px',
                background: 'none',
                border: 'none',
                color: '#fff',
                textAlign: 'left',
                cursor: 'pointer',
                fontSize: '14px',
                transition: 'background-color 0.2s ease'
              }}
              onMouseEnter={(e) => e.target.style.backgroundColor = '#3a3a3a'}
              onMouseLeave={(e) => e.target.style.backgroundColor = 'transparent'}
            >
              Mi billetera
            </button>

            {/* 3. Configuración */}
            <button
              onClick={() => handleNavegar('configuracion')}
              style={{
                width: '100%',
                padding: '12px 16px',
                background: 'none',
                border: 'none',
                color: '#fff',
                textAlign: 'left',
                cursor: 'pointer',
                fontSize: '14px',
                transition: 'background-color 0.2s ease'
              }}
              onMouseEnter={(e) => e.target.style.backgroundColor = '#3a3a3a'}
              onMouseLeave={(e) => e.target.style.backgroundColor = 'transparent'}
            >
              Configuración
            </button>

            <div style={{ borderTop: '1px solid #444' }} />

            {/* 4. Cerrar sesión */}
            <button
              onClick={handleLogoutMenu}
              style={{
                width: '100%',
                padding: '12px 16px',
                background: 'none',
                border: 'none',
                color: '#dc3545',
                textAlign: 'left',
                cursor: 'pointer',
                fontSize: '14px',
                fontWeight: 'bold',
                transition: 'background-color 0.2s ease'
              }}
              onMouseEnter={(e) => e.target.style.backgroundColor = '#3a3a3a'}
              onMouseLeave={(e) => e.target.style.backgroundColor = 'transparent'}
            >
              Cerrar sesión
            </button>
          </div>
        )}
      </div>
    </nav>
  );
}