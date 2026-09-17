import axios from 'axios';

const API = axios.create({
  baseURL: 'http://localhost:5118/api',
  headers: {
    'Content-Type': 'application/json'
  }
});

// --- BILLETERA ---
export const getBilletera = (usuarioId) => API.get(`/wallets/${usuarioId}`);
export const cargarSaldo = (usuarioId, monto) => API.post(`/wallets/cargar/${usuarioId}`, { monto });
export const getMovimientos = (usuarioId) => API.get(`/wallets/users/${usuarioId}/movements`);

// --- SUBASTAS ---
// Obtener el catálogo de subastas
export const getSubastas = () => API.get('/auctions');

// Obtener el detalle de una subasta específica
export const getSubastaById = (id) => API.get(`/auctions/${id}`);

// Realizar una puja manual
export const realizarPuja = (subastaId, compradorId, montoPuja) => 
  API.post(`/auctions/${subastaId}/bids`, { compradorId, montoPuja });

// Configurar una puja automática (Proxy Bidding)
export const configurarPujaAutomatica = (subastaId, compradorId, montoMaximo) => 
  API.post(`/auctions/${subastaId}/auto-bids`, { compradorId, montoMaximo });

export default API;