import axios from 'axios';

const API = axios.create({
  baseURL: 'http://localhost:5118/api',
  headers: {
    'Content-Type': 'application/json'
  }
});

// GET /api/v1/wallets/1
export const getBilletera = (usuarioId) => API.get(`/wallets/${usuarioId}`);

// POST /api/v1/wallets/cargar/1 (Ruta corregida)
export const cargarSaldo = (usuarioId, monto) => API.post(`/wallets/cargar/${usuarioId}`, { monto });

// GET /api/v1/wallets/users/1/movements
export const getMovimientos = (usuarioId) => API.get(`/wallets/users/${usuarioId}/movements`);

export default API;