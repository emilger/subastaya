// src/App.jsx
<<<<<<< HEAD
import { useState } from 'react';
import { Billetera } from './components/Billetera';
import { SubastaList } from './components/SubastaList';
import { SubastaDetalle } from './components/SubastaDetalle';
import './App.css';

function App() {
  const [usuarioId, setUsuarioId] = useState(1);
  const [subastaSeleccionada, setSubastaSeleccionada] = useState(null);

  return (
    <div className="App" style={{ fontFamily: 'sans-serif', paddingBottom: '50px' }}>
      <header style={{ backgroundColor: '#1a237e', color: '#fff', padding: '15px', textAlign: 'center' }}>
        <h1 style={{ margin: 0 }}>SubastaYa - Panel Principal</h1>
      </header>

      <div style={{ textAlign: 'center', margin: '20px 0' }}>
        <label><strong>Simular Usuario: </strong></label>
        <select 
          value={usuarioId} 
          onChange={(e) => setUsuarioId(Number(e.target.value))}
          style={{ padding: '6px 12px', fontSize: '1rem', borderRadius: '4px' }}
        >
          <option value={1}>Comprador #1</option>
          <option value={2}>Comprador #2</option>
        </select>
      </div>

      {/* Sección Billetera */}
      <Billetera usuarioId={usuarioId} />

      {/* Alternar entre Catálogo y Detalle */}
      {subastaSeleccionada ? (
        <SubastaDetalle 
          subasta={subastaSeleccionada} 
          usuarioId={usuarioId} 
          onVolver={() => setSubastaSeleccionada(null)}
        />
      ) : (
        <SubastaList 
          onSeleccionarSubasta={(subasta) => setSubastaSeleccionada(subasta)} 
        />
=======
import React, { useState, useEffect } from 'react';
import Login from './components/Login';
import Navbar from './components/Navbar';
import { Billetera } from './components/Billetera'; // El componente en el que trabaja tu compañero

const TIMEOUT_MINUTOS = 5; 

export default function App() {
  const [isAuthenticated, setIsAuthenticated] = useState(false);
  // Control de vista: 'catalogo' o 'billetera'
  const [vistaActual, setVistaActual] = useState('catalogo');

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
    <div style={{ backgroundColor: '#121212', minHeight: '100vh', color: '#fff' }}>
      {isAuthenticated ? (
        <div>
          {/* Navbar recibe la función para cambiar de pantalla */}
          <Navbar onLogout={handleLogout} onNavigate={setVistaActual} />
          
          <main style={{ padding: '30px' }}>
            {/* VISTA 1: Catálogo Principal */}
            {vistaActual === 'catalogo' && (
              <div style={{ textAlign: 'center' }}>
                <h1>Catálogo de Subastas</h1>
                <p style={{ color: '#aaa' }}>Acá van a aparecer las tarjetas de los productos subastados...</p>
              </div>
            )}

            {/* VISTA 2: Pantalla / Menú de Billetera (de tu compañero) */}
            {vistaActual === 'billetera' && (
              <div>
                <Billetera />
              </div>
            )}
          </main>
        </div>
      ) : (
        <Login onLoginSuccess={handleLoginSuccess} />
>>>>>>> b08dc086e0c0b7a4f76cc04c3fabe032a1bd488b
      )}
    </div>
  );
}