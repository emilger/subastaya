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

const TIMEOUT_MINUTOS = 5;

export default function App() {
  const [isAuthenticated, setIsAuthenticated] = useState(false);
  const [vistaActual, setVistaActual] = useState('catalogo');
  const [vistaAnterior, setVistaAnterior] = useState('catalogo');
  const [subastaSeleccionada, setSubastaSeleccionada] = useState(null);

  // Navegación fluida entre secciones
  const navegarA = (nuevaVista) => {
    setVistaAnterior(vistaActual);
    setVistaActual(nuevaVista);
    if (nuevaVista !== 'catalogo') {
      setSubastaSeleccionada(null);
    }
  };

  // Botón Volver para el formulario de subastas
  const handleVolver = () => {
    setVistaActual(vistaAnterior);
  };

  const handleLogout = () => {
    localStorage.removeItem('token');
    localStorage.removeItem('lastActiveTime');
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

  return (
    <div style={{ backgroundColor: '#E3C3B1', minHeight: '100vh', width: '100%', color: '#1a1a1a' }}>
      {isAuthenticated ? (
        <div>
          {/* Barra de navegación superior */}
          <Navbar onLogout={handleLogout} onNavigate={navegarA} />

          <main style={{ padding: '30px', maxWidth: '1200px', margin: '0 auto' }}>
            {/* VISTA 1: Catálogo y Detalle de Subasta */}
            {vistaActual === 'catalogo' && (
              <div>
                {subastaSeleccionada ? (
                  /* Detalle de la subasta seleccionada */
                  <SubastaDetalle
                    subasta={subastaSeleccionada}
                    onVolver={() => setSubastaSeleccionada(null)}
                  />
                ) : (
                  /* Catálogo general de productos */
                  <div>
                    <div style={{ display: 'flex', justifyContent: 'space-between', alignItems: 'center', marginBottom: '24px' }}>
                      <h1 style={{ margin: 0, color: '#1a1a1a' }}>Catálogo de Subastas</h1>
                      <button
                        onClick={() => navegarA('publicar')}
                        style={{
                          padding: '12px 20px',
                          backgroundColor: '#28a745',
                          color: '#fff',
                          border: 'none',
                          borderRadius: '8px',
                          fontSize: '15px',
                          fontWeight: 'bold',
                          cursor: 'pointer',
                          boxShadow: '0 4px 12px rgba(40, 167, 69, 0.3)'
                        }}
                      >
                        ➕ Crear Subasta
                      </button>
                    </div>

                    {/* Grilla de subastas traída de la API */}
                    <SubastaList
                      onSeleccionarSubasta={(subasta) => setSubastaSeleccionada(subasta)}
                    />
                  </div>
                )}
              </div>
            )}

            {/* VISTA 2: Formulario de Crear Subasta */}
            {vistaActual === 'publicar' && (
              <CrearSubasta
                onSubastaCreada={() => navegarA('catalogo')}
                onVolver={handleVolver}
              />
            )}

            {/* VISTA 3: Historial de Mis Subastas */}
            {vistaActual === 'mis-subastas' && (
              <MisSubastas onCrearNueva={() => navegarA('publicar')} />
            )}

            {/* VISTA 4: Menú Billetera */}
            {vistaActual === 'billetera' && (
              <div>
                <Billetera />
              </div>
            )}
          </main>
        </div>
      ) : (
        <Login onLoginSuccess={handleLoginSuccess} />
      )}
    </div>
  );
}
