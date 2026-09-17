// src/App.jsx
import React, { useState, useEffect } from 'react';
import './App.css';
import Login from './components/Login';
import Navbar from './components/Navbar';
import { Billetera } from './components/Billetera';
import CrearSubasta from './components/CrearSubasta';
import MisSubastas from './components/MisSubastas';
import SubastaList from './components/SubastaList';
import SubastaDetalle from './components/SubastaDetalle';
import Configuracion from './components/Configuracion';

const TIMEOUT_MINUTOS = 5;

export default function App() {
  const [isAuthenticated, setIsAuthenticated] = useState(false);
  const [vistaActual, setVistaActual] = useState(() => {
    return localStorage.getItem('vistaActual') || 'catalogo';
  });
  const [subastaSeleccionada, setSubastaSeleccionada] = useState(null);
  const [usuarioActual] = useState({ id: 1, nombre: 'Usuario Demo' });

  // Estados de Búsqueda y Filtros compartidos
  const [busqueda, setBusqueda] = useState('');
  const [categoriaFiltro, setCategoriaFiltro] = useState('todas');
  const [estadoFiltro, setEstadoFiltro] = useState('todos');
  const [ordenValor, setOrdenValor] = useState('recientes');

  const navegarA = (nuevaVista) => {
    setVistaActual(nuevaVista);
    localStorage.setItem('vistaActual', nuevaVista);
    if (nuevaVista !== 'catalogo') {
      setSubastaSeleccionada(null);
    }
  };

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
      if (lastActive && now - Number(lastActive) > limiteMs) {
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
    <div style={{ backgroundColor: '#E5B295', minHeight: '100vh', width: '100%', color: '#000000', fontFamily: 'sans-serif' }}>
      {isAuthenticated ? (
        <div>
          <Navbar 
            onLogout={handleLogout} 
            onNavigate={navegarA}
            busqueda={busqueda}
            setBusqueda={setBusqueda}
            categoriaFiltro={categoriaFiltro}
            setCategoriaFiltro={setCategoriaFiltro}
            estadoFiltro={estadoFiltro}
            setEstadoFiltro={setEstadoFiltro}
            ordenValor={ordenValor}
            setOrdenValor={setOrdenValor}
          />

          <main style={{ padding: '24px 20px', maxWidth: '1200px', margin: '0 auto' }}>
            {mostrarBotonVolver && (
              <div style={{ display: 'flex', justifyContent: 'flex-start', marginBottom: '16px' }}>
                <button
                  onClick={handleVolver}
                  title="Volver"
                  style={{
                    background: 'none',
                    border: 'none',
                    color: '#000000',
                    fontSize: '28px',
                    fontWeight: 'bold',
                    cursor: 'pointer',
                    padding: 0,
                    lineHeight: '1'
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
                    usuarioId={usuarioActual.id}
                    onVolver={handleVolver}
                  />
                ) : (
                  <div>
                    <div style={{ display: 'flex', alignItems: 'center', justifyContent: 'space-between', marginBottom: '24px' }}>
                      <h1 style={{ margin: 0, color: '#000000', fontSize: '28px', fontWeight: 'bold' }}>
                        Subastas
                      </h1>
                      <button
                        onClick={() => navegarA('publicar')}
                        style={{
                          padding: '10px 18px',
                          backgroundColor: '#1e1e1e',
                          color: '#ffffff',
                          border: 'none',
                          borderRadius: '8px',
                          fontSize: '14px',
                          fontWeight: 'bold',
                          cursor: 'pointer'
                        }}
                      >
                        Crear Nueva Subasta
                      </button>
                    </div>

                    <SubastaList
                      onSeleccionarSubasta={(subasta) => setSubastaSeleccionada(subasta)}
                      busqueda={busqueda}
                      categoriaFiltro={categoriaFiltro}
                      estadoFiltro={estadoFiltro}
                      ordenValor={ordenValor}
                    />
                  </div>
                )}
              </div>
            )}

            {vistaActual === 'publicar' && (
              <CrearSubasta 
                usuarioId={usuarioActual.id}
                onSubastaCreada={() => navegarA('catalogo')} 
                onVolver={handleVolver}
              />
            )}

            {vistaActual === 'mis-subastas' && (
              <MisSubastas
                usuarioId={usuarioActual.id}
                onCrearSubasta={() => navegarA('publicar')}
                onSeleccionarSubasta={(id) => {
                  setSubastaSeleccionada({ id });
                  navegarA('catalogo');
                }}
              />
            )}

            {vistaActual === 'billetera' && (
              <Billetera usuarioId={usuarioActual.id} />
            )}

            {vistaActual === 'configuracion' && <Configuracion />}
          </main>
        </div>
      ) : (
        <Login onLoginSuccess={handleLoginSuccess} />
      )}
    </div>
  );
}