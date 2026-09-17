import { useState } from 'react';
import { Billetera } from './components/Billetera';
import './App.css';

function App() {
  const [usuarioId, setUsuarioId] = useState(1);

  return (
    <div className="App" style={{ textAlign: 'center', fontFamily: 'sans-serif' }}>
      <h1>SubastaYa - Panel Principal</h1>

      <div style={{ marginBottom: '20px' }}>
        <label><strong>Simular Usuario: </strong></label>
        <select 
          value={usuarioId} 
          onChange={(e) => setUsuarioId(Number(e.target.value))}
          style={{ padding: '6px 12px', fontSize: '1rem' }}
        >
          <option value={1}>Comprador #1</option>
          <option value={2}>Comprador #2</option>
        </select>
      </div>

      <Billetera usuarioId={usuarioId} />
    </div>
  );
}

export default App;