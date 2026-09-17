// src/api.js
import axios from 'axios';

const API_BASE_URL = 'http://localhost:5118/api/v1';

// Interceptor para adjuntar el Token JWT si el usuario inició sesión
axios.interceptors.request.use((config) => {
  const token = localStorage.getItem('token');
  if (token) {
    config.headers.Authorization = `Bearer ${token}`;
  }
  return config;
}, (error) => Promise.reject(error));

// ==========================================
// 1. BILLETERA / WALLET
// ==========================================

// Obtener estado de la billetera desde PostgreSQL
export const getBilletera = async (usuarioId = 1) => {
  return await axios.get(`${API_BASE_URL}/wallets/${usuarioId}`);
};

// Cargar saldo (Registra en TRANSACCION_LEDGER y AUDITORIA_LOG)
export const cargarSaldo = async (usuarioId = 1, monto) => {
  return await axios.post(`${API_BASE_URL}/wallets/cargar/${usuarioId}`, {
    monto: Number(monto)
  });
};

// Obtener el historial de movimientos del Ledger
export const getMovimientos = async (usuarioId = 1) => {
  return await axios.get(`${API_BASE_URL}/wallets/users/${usuarioId}/movements`);
};

// ==========================================
// 2. SUBASTAS Y PUJAS / AUCTIONS & BIDS
// ==========================================

// Crear/Publicar una nueva subasta
export const crearSubasta = async (subastaData) => {
  return await axios.post(`${API_BASE_URL}/auctions`, subastaData);
};

// Obtener todas las subastas activas (Catálogo)
export const getSubastas = async () => {
  return await axios.get(`${API_BASE_URL}/auctions`);
};

// Obtener detalle de una subasta por ID
export const getSubastaDetalle = async (id) => {
  return await axios.get(`${API_BASE_URL}/auctions/${id}`);
};

// Realizar una puja manual en una subasta (exportado como realizarPuja y crearPuja)
export const realizarPuja = async (subastaId, monto) => {
  return await axios.post(`${API_BASE_URL}/auctions/${subastaId}/bids`, {
    monto: Number(monto)
  });
};

export const crearPuja = realizarPuja;

// Configurar o actualizar Puja Automática (Proxy Bidding)
export const configurarPujaAutomatica = async (subastaId, limiteMaximo) => {
  return await axios.post(`${API_BASE_URL}/auctions/${subastaId}/auto-bid`, {
    limiteMaximo: Number(limiteMaximo)
  });
};
// Agregar a src/api.js
export const retirarSaldo = async (usuarioId = 1, monto) => {
  return await axios.post(`${API_BASE_URL}/wallets/retirar/${usuarioId}`, {
    monto: Number(monto)
  });
};