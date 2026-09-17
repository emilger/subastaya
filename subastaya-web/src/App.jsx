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
      )}
    </div>
  );
}

export default App;