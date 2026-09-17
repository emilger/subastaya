// src/components/Login.jsx
import React, { useState } from 'react';
import API from '../api';

export default function Login({ onLoginSuccess }) {
  const [formData, setFormData] = useState({ email: '', password: '' });
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState('');

  const handleChange = (e) => {
    setFormData({ ...formData, [e.target.name]: e.target.value });
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    setLoading(true);
    setError('');

    try {
      // Petición a la API de C#
      const response = await API.post('/Auth/Login', formData);
      console.log('Respuesta del Backend:', response.data);

      const data = response.data;
      // Buscamos el token en las propiedades habituales de la respuesta
      const token = data.token || data.Token || data.result?.token || (typeof data === 'string' ? data : null);

      if (token) {
        localStorage.setItem('token', token);
        console.log('Token guardado correctamente:', token);
        
        if (onLoginSuccess) {
          onLoginSuccess(); // Le avisa a App.jsx para cambiar la pantalla
        }
      } else {
        console.warn('Estructura recibida sin token:', data);
        setError('El servidor respondió 200 OK, pero no se encontró la propiedad "token" en la respuesta.');
      }
    } catch (err) {
      console.error('Error al intentar iniciar sesión:', err);
      if (err.response) {
        // El servidor respondió con un código de error (ej: 400 Bad Request, 401 Unauthorized)
        const msj = typeof err.response.data === 'string' ? err.response.data : JSON.stringify(err.response.data);
        setError(`Error del servidor (${err.response.status}): ${msj}`);
      } else if (err.request) {
        // La API de C# no está respondiendo
        setError('No se pudo conectar con el Backend. Verificá que la API de C# esté corriendo en http://localhost:5118');
      } else {
        setError('Error inesperado: ' + err.message);
      }
    } finally {
      setLoading(false);
    }
  };

  return (
    <div style={{ maxWidth: '400px', margin: '50px auto', padding: '20px', border: '1px solid #ccc', borderRadius: '8px' }}>
      <h2>Iniciar Sesión - SubastaYa</h2>
      
      {error && (
        <div style={{ padding: '10px', backgroundColor: '#f8d7da', color: '#842029', borderRadius: '4px', marginBottom: '15px', fontSize: '14px', wordBreak: 'break-word' }}>
          {error}
        </div>
      )}
      
      <form onSubmit={handleSubmit}>
        <div style={{ marginBottom: '15px' }}>
          <label htmlFor="email">Email / Usuario:</label>
          <input
            id="email"
            type="text"
            name="email"
            autoComplete="username"
            value={formData.email}
            onChange={handleChange}
            required
            style={{ width: '100%', padding: '8px', marginTop: '5px' }}
          />
        </div>

        <div style={{ marginBottom: '15px' }}>
          <label htmlFor="password">Contraseña:</label>
          <input
            id="password"
            type="password"
            name="password"
            autoComplete="current-password"
            value={formData.password}
            onChange={handleChange}
            required
            style={{ width: '100%', padding: '8px', marginTop: '5px' }}
          />
        </div>

        <button 
          type="submit" 
          disabled={loading}
          style={{ width: '100%', padding: '10px', backgroundColor: '#0d6efd', color: '#fff', border: 'none', borderRadius: '4px', cursor: 'pointer' }}
        >
          {loading ? 'Cargando...' : 'Iniciar Sesión'}
        </button>
      </form>
    </div>
  );
}