// src/api.js
import axios from 'axios';

const API = axios.create({
  baseURL: 'http://localhost:5118/api', // URL Base oficial
  headers: {
    'Content-Type': 'application/json',
  },
});

// Interceptor para adjuntar automáticamente el Token JWT en cada petición
API.interceptors.request.use(
  (config) => {
    const token = localStorage.getItem('token');
    if (token) {
      config.headers.Authorization = `Bearer ${token}`;
    }
    return config;
  },
  (error) => Promise.reject(error)
);

// --- FUNCIONES PARA SUBASTAS ---

// Obtener el catálogo general de subastas
export const getSubastas = async (filtros = {}) => {
  const response = await API.get('/v1/auctions', { params: filtros });
  return response.data;
};
export const obtenerSubastas = getSubastas;

// Obtener el detalle de una subasta específica
export const getSubastaById = async (id) => {
  const response = await API.get(`/v1/auctions/${id}`);
  return response.data;
};
export const obtenerSubastaPorId = getSubastaById;

// Crear / Publicar una nueva subasta
export const crearSubasta = async (subastaData) => {
  const response = await API.post('/v1/auctions', subastaData);
  return response.data;
};

// Realizar una oferta o puja manual
export const realizarPuja = async (subastaId, monto) => {
  const response = await API.post(`/v1/auctions/${subastaId}/bids`, { montoPuja: Number(monto) });
  return response.data;
};
export const pujar = realizarPuja;

// Configurar puja automática (Proxy Bidding)
export const configurarPujaAutomatica = async (subastaId, montoMaximo) => {
  const response = await API.post(`/v1/auctions/${subastaId}/auto-bids`, { montoMaximo: Number(montoMaximo) });
  return response.data;
};
export const autoPuja = configurarPujaAutomatica;
export const pujaAutomatica = configurarPujaAutomatica;

// --- FUNCIONES PARA LA BILLETERA ---

// Consulta de saldo y billetera
export const getBilletera = async () => {
  const response = await API.get('/v1/wallet');
  return response.data;
};
export const obtenerBilletera = getBilletera;
export const getWallet = getBilletera;

// Depósito de saldo
export const cargarSaldo = async (monto) => {
  const response = await API.post('/v1/wallet/deposit', { monto: Number(monto) });
  return response.data;
};
export const depositarSaldo = cargarSaldo;

// Retiro de fondos
export const retirarSaldo = async (monto) => {
  const response = await API.post('/v1/wallet/withdraw', { monto: Number(monto) });
  return response.data;
};

// --- EXPORTACIONES PRINCIPALES DE AXIOS ---
export { API };
export default API;