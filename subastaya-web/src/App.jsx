// src/App.jsx
import React, { useState } from 'react';
import Login from './components/Login';
import { Billetera } from './components/Billetera';

function App() {
  const [token, setToken] = useState(localStorage.getItem('token'));

  const handleLoginSuccess = () => {
    setToken(localStorage.getItem('token'));
  };

  const handleLogout = () => {
    localStorage.removeItem('token');
    localStorage.removeItem('usuario');
    setToken(null);
  };

  // 1. Si NO hay token: Muestra pantalla de Login
  if (!token) {
    return <Login onLoginSuccess={handleLoginSuccess} />;
  }

  // 2. Si SÍ hay token: Muestra la pantalla principal con la Billetera
  return (
    <div style={{ padding: '20px', fontFamily: 'sans-serif', maxWidth: '1200px', margin: '0 auto' }}>
      <header style={{ 
        display: 'flex', 
        justifyContent: 'space-between', 
        alignItems: 'center', 
        paddingBottom: '15px', 
        borderBottom: '2px solid #eee',
        marginBottom: '20px' 
      }}>
        <h2>SubastaYa - Panel Principal</h2>
        <button 
          onClick={handleLogout} 
          style={{ 
            padding: '8px 16px', 
            cursor: 'pointer', 
            backgroundColor: '#dc3545', 
            color: '#fff', 
            border: 'none', 
            borderRadius: '4px' 
          }}
        >
          Cerrar Sesión
        </button>
      </header>

      <main>
        <Billetera />
      </main>
    </div>
  );
}

export default App;