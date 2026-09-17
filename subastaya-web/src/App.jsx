// src/App.jsx
import React, { useState, useEffect } from 'react';
import './App.css';
import Login from './components/Login';
import Navbar from './components/Navbar';
import { Billetera } from './components/Billetera';
import CrearSubasta from './components/CrearSubasta';
import MisSubastas from './components/MisSubastas';
import { SubastaList } from './components/SubastaList';
import { SubastaDetalle } from './components/SubastaDetalle';
import Configuracion from './components/Configuracion';

const TIMEOUT_MINUTOS = 5;

export default function App() {
  const [isAuthenticated, setIsAuthenticated] = useState(false);
  
  // Persistir la vista actual al recargar la página
  const [vistaActual, setVistaActual] = useState(() => {
    return localStorage.getItem('vistaActual') || 'catalogo';
  });

  const [subastaSeleccionada, setSubastaSeleccionada] = useState(null);

  // Cambiar de pantalla y guardar en localStorage
  const navegarA = (nuevaVista) => {
    setVistaActual(nuevaVista);
    localStorage.setItem('vistaActual', nuevaVista);
    if (nuevaVista !== 'catalogo') {
      setSubastaSeleccionada(null);
    }
  };

  // Acción global del botón Volver
  const handleVolver = () => {
    if (subastaSeleccionada) {
      setSubastaSeleccionada(null);
    } else {
      navegarA('catalogo');
    }
  };

  const handleLogout = () => {
    localStorage.removeItem('token');
    localStorage.removeItem('lastActiveTime');
    localStorage.removeItem('vistaActual');
    setIsAuthenticated(false);
  };

  const handleLoginSuccess = () => {
    localStorage.setItem('lastActiveTime', Date.now().toString());
    setIsAuthenticated(true);
  };

  useEffect(() => {
    const token = localStorage.getItem('token');
    const lastActive = localStorage.getItem('lastActiveTime');
    const now = Date.now();
    const limiteMs = TIMEOUT_MINUTOS * 60 * 1000;

    if (token) {
      if (lastActive && (now - Number(lastActive) > limiteMs)) {
        handleLogout();
      } else {
        localStorage.setItem('lastActiveTime', now.toString());
        setIsAuthenticated(true);
      }
    } else {
      setIsAuthenticated(false);
    }
  }, []);

  const mostrarBotonVolver = vistaActual !== 'catalogo' || subastaSeleccionada !== null;

  return (
    <div style={{ backgroundColor: '#CFA182', minHeight: '100vh', width: '100%', color: '#1a1a1a' }}>
      {isAuthenticated ? (
        <div>
          <Navbar onLogout={handleLogout} onNavigate={navegarA} />

          <main style={{ padding: '30px', maxWidth: '1200px', margin: '0 auto' }}>
            {mostrarBotonVolver && (
              <div style={{ display: 'flex', justifyContent: 'flex-start', marginBottom: '16px' }}>
                <button
                  onClick={handleVolver}
                  title="Volver al Catálogo"
                  style={{
                    background: 'none',
                    border: 'none',
                    color: '#1a1a1a',
                    fontSize: '28px',
                    fontWeight: 'bold',
                    cursor: 'pointer',
                    padding: 0,
                    lineHeight: '1',
                    transition: 'opacity 0.2s ease'
                  }}
                >
                  ←
                </button>
              </div>
            )}

            {vistaActual === 'catalogo' && (
              <div>
                {subastaSeleccionada ? (
                  <SubastaDetalle
                    subasta={subastaSeleccionada}
                    onVolver={handleVolver}
                  />
                ) : (
                  <div>
                    <div style={{ 
                      display: 'flex', 
                      alignItems: 'center', 
                      gap: '22px', 
                      marginBottom: '24px' 
                    }}>
                      <h1 style={{ 
                        margin: 0, 
                        color: '#1a1a1a', 
                        fontSize: '32px', 
                        lineHeight: '1'
                      }}>
                        Catálogo
                      </h1>
                      <button
                        onClick={() => navegarA('publicar')}
                        title="Crear Nueva Subasta"
                        style={{
                          background: 'none',
                          border: 'none',
                          color: '#28a745',
                          fontSize: '36px',
                          fontWeight: 'bold',
                          cursor: 'pointer',
                          padding: 0,
                          margin: 0,
                          lineHeight: '1',
                          display: 'inline-flex',
                          alignItems: 'center',
                          justifyContent: 'center',
                          transform: 'translateY(4px)',
                          transition: 'opacity 0.2s ease'
                        }}
                      >
                        +
                      </button>
                    </div>

                    <SubastaList
                      onSeleccionarSubasta={(subasta) => setSubastaSeleccionada(subasta)}
                    />
                  </div>
                )}
              </div>
            )}

            {vistaActual === 'publicar' && (
              <CrearSubasta
                onSubastaCreada={() => navegarA('catalogo')}
              />
            )}

            {vistaActual === 'mis-subastas' && (
              <MisSubastas onCrearNueva={() => navegarA('publicar')} />
            )}

            {vistaActual === 'billetera' && (
              <div>
                <Billetera />
              </div>
            )}

            {vistaActual === 'configuracion' && (
              <Configuracion />
            )}
          </main>
        </div>
      ) : (
        <Login onLoginSuccess={handleLoginSuccess} />
      )}
    </div>
  );
}