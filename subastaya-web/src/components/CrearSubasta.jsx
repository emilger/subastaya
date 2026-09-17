// src/components/CrearSubasta.jsx
import React, { useState } from 'react';
import API from '../api';

export default function CrearSubasta({ onSubastaCreada, onVolver }) {
  const [formData, setFormData] = useState({
    titulo: '',
    descripcion: '',
    urlImagen: '',
    categoriaId: 1, // 1: Tecnología, 2: Coleccionables, 3: Indumentaria, 4: Vehículos
    precioBase: '',
    incrementoMinimo: '',
    fechaInicio: new Date().toISOString().slice(0, 16),
    fechaFin: new Date(Date.now() + 86400000).toISOString().slice(0, 16) // Mañana por defecto
  });

  const [cargando, setCargando] = useState(false);
  const [mensaje, setMensaje] = useState({ tipo: '', texto: '' });

  const handleChange = (e) => {
    const { name, value } = e.target;
    setFormData((prev) => ({ ...prev, [name]: value }));
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    setMensaje({ tipo: '', texto: '' });

    if (new Date(formData.fechaFin) <= new Date(formData.fechaInicio)) {
      setMensaje({ tipo: 'error', texto: 'La fecha de fin debe ser posterior a la fecha de inicio.' });
      return;
    }

    if (Number(formData.precioBase) <= 0 || Number(formData.incrementoMinimo) <= 0) {
      setMensaje({ tipo: 'error', texto: 'El precio base e incremento mínimo deben ser números positivos.' });
      return;
    }

    try {
      setCargando(true);
      const payload = {
        titulo: formData.titulo,
        descripcion: formData.descripcion,
        urlImagen: formData.urlImagen,
        categoriaId: Number(formData.categoriaId),
        precioBase: Number(formData.precioBase),
        incrementoMinimo: Number(formData.incrementoMinimo),
        fechaInicio: new Date(formData.fechaInicio).toISOString(),
        fechaFin: new Date(formData.fechaFin).toISOString()
      };

      await API.post('/v1/auctions', payload);

      setMensaje({ tipo: 'exito', texto: '¡Subasta creada y publicada exitosamente!' });
      
      setTimeout(() => {
        if (onSubastaCreada) onSubastaCreada();
      }, 1500);

    } catch (error) {
      console.error('Error al publicar subasta:', error);
      const errorMsg = error.response?.data?.mensaje || 'Error al conectar con la API para crear la subasta.';
      setMensaje({ tipo: 'error', texto: errorMsg });
    } finally {
      setCargando(false);
    }
  };

  return (
    <div style={{ maxWidth: '600px', margin: '0 auto', backgroundColor: '#1e1e1e', padding: '30px', borderRadius: '12px', boxShadow: '0 4px 16px rgba(0,0,0,0.5)' }}>
      {/* Encabezado con Botón Volver y Título Centrado */}
      <div style={{ display: 'flex', alignItems: 'center', marginBottom: '24px' }}>
        <h2 style={{ textAlign: 'center', margin: 0, color: '#fff', flex: 1, paddingRight: onVolver ? '80px' : '0' }}>
          Publicar Nueva Subasta
        </h2>
      </div>

      {mensaje.texto && (
        <div style={{
          padding: '12px',
          borderRadius: '6px',
          marginBottom: '20px',
          textAlign: 'center',
          backgroundColor: mensaje.tipo === 'exito' ? '#155724' : '#721c24',
          color: mensaje.tipo === 'exito' ? '#d4edda' : '#f8d7da',
          border: `1px solid ${mensaje.tipo === 'exito' ? '#c3e6cb' : '#f5c6cb'}`
        }}>
          {mensaje.texto}
        </div>
      )}

      <form onSubmit={handleSubmit}>
        <div style={{ marginBottom: '16px' }}>
          <label style={{ display: 'block', marginBottom: '6px', color: '#ccc' }}>Título del producto:</label>
          <input
            type="text"
            name="titulo"
            required
            placeholder="Ej: Consola Retro Edición Limitada"
            value={formData.titulo}
            onChange={handleChange}
            style={inputStyle}
          />
        </div>

        <div style={{ marginBottom: '16px' }}>
          <label style={{ display: 'block', marginBottom: '6px', color: '#ccc' }}>Descripción detallada:</label>
          <textarea
            name="descripcion"
            required
            rows="3"
            placeholder="Describí el estado, detalles y características del producto..."
            value={formData.descripcion}
            onChange={handleChange}
            style={{ ...inputStyle, resize: 'vertical' }}
          />
        </div>

        <div style={{ display: 'grid', gridTemplateColumns: '1fr 1fr', gap: '16px', marginBottom: '16px' }}>
          <div>
            <label style={{ display: 'block', marginBottom: '6px', color: '#ccc' }}>URL de la Imagen:</label>
            <input
              type="url"
              name="urlImagen"
              required
              placeholder="https://ejemplo.com/imagen.jpg"
              value={formData.urlImagen}
              onChange={handleChange}
              style={inputStyle}
            />
          </div>

          <div>
            <label style={{ display: 'block', marginBottom: '6px', color: '#ccc' }}>Categoría:</label>
            <select
              name="categoriaId"
              value={formData.categoriaId}
              onChange={handleChange}
              style={inputStyle}
            >
              <option value="1"> Tecnología</option>
              <option value="2"> Coleccionables</option>
              <option value="3"> Indumentaria</option>
              <option value="4"> Vehículos</option>
            </select>
          </div>
        </div>

        <div style={{ display: 'grid', gridTemplateColumns: '1fr 1fr', gap: '16px', marginBottom: '16px' }}>
          <div>
            <label style={{ display: 'block', marginBottom: '6px', color: '#ccc' }}>Precio Base Inicial (\$):</label>
            <input
              type="number"
              name="precioBase"
              required
              min="1"
              placeholder="Ej: 10000"
              value={formData.precioBase}
              onChange={handleChange}
              style={inputStyle}
            />
          </div>

          <div>
            <label style={{ display: 'block', marginBottom: '6px', color: '#ccc' }}>Incremento Mínimo (\$):</label>
            <input
              type="number"
              name="incrementoMinimo"
              required
              min="1"
              placeholder="Ej: 500"
              value={formData.incrementoMinimo}
              onChange={handleChange}
              style={inputStyle}
            />
          </div>
        </div>

        <div style={{ display: 'grid', gridTemplateColumns: '1fr 1fr', gap: '16px', marginBottom: '24px' }}>
          <div>
            <label style={{ display: 'block', marginBottom: '6px', color: '#ccc' }}>Fecha de Inicio:</label>
            <input
              type="datetime-local"
              name="fechaInicio"
              required
              value={formData.fechaInicio}
              onChange={handleChange}
              style={inputStyle}
            />
          </div>

          <div>
            <label style={{ display: 'block', marginBottom: '6px', color: '#ccc' }}>Fecha de Cierre:</label>
            <input
              type="datetime-local"
              name="fechaFin"
              required
              value={formData.fechaFin}
              onChange={handleChange}
              style={inputStyle}
            />
          </div>
        </div>

        <div style={{ display: 'flex', gap: '12px' }}>
          {onVolver && (
            <button
              type="button"
              onClick={onVolver}
              style={{
                flex: 1,
                padding: '12px',
                backgroundColor: '#3a3a3a',
                color: '#fff',
                border: 'none',
                borderRadius: '6px',
                fontSize: '16px',
                fontWeight: 'bold',
                cursor: 'pointer'
              }}
            >
              Cancelar
            </button>
          )}

          <button
            type="submit"
            disabled={cargando}
            style={{
              flex: 2,
              padding: '12px',
              backgroundColor: cargando ? '#6c757d' : '#28a745',
              color: '#fff',
              border: 'none',
              borderRadius: '6px',
              fontSize: '16px',
              fontWeight: 'bold',
              cursor: cargando ? 'not-allowed' : 'pointer',
              transition: 'background 0.2s'
            }}
          >
            {cargando ? 'Publicando...' : ' Publicar Subasta'}
          </button>
        </div>
      </form>
    </div>
  );
}

const inputStyle = {
  width: '100%',
  padding: '10px 12px',
  borderRadius: '6px',
  border: '1px solid #444',
  backgroundColor: '#2b2b2b',
  color: '#fff',
  fontSize: '14px',
  boxSizing: 'border-box'
};