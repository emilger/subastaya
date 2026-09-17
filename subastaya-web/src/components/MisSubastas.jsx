// src/components/MisSubastas.jsx
import React from 'react';

export default function MisSubastas({ onCrearNueva }) {
  return (
    <div style={{ maxWidth: '900px', margin: '0 auto', color: '#fff' }}>
      <div style={{ display: 'flex', justifyContent: 'space-between', alignItems: 'center', marginBottom: '20px' }}>
        <h2>📦 Mis Subastas Publicadas</h2>
        <button
          onClick={onCrearNueva}
          style={{
            padding: '10px 18px',
            backgroundColor: '#28a745',
            color: '#fff',
            border: 'none',
            borderRadius: '6px',
            fontWeight: 'bold',
            cursor: 'pointer'
          }}
        >
          ➕ Publicar Nueva
        </button>
      </div>

      {/* Tarjeta de historial vacía / de prueba */}
      <div style={{ backgroundColor: '#1e1e1e', padding: '20px', borderRadius: '8px', border: '1px solid #333', textAlign: 'center', color: '#aaa' }}>
        <p>Aún no tenés subastas publicadas o se cargarán desde el backend.</p>
      </div>
    </div>
  );
}