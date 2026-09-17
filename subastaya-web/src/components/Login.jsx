import React, { useState } from 'react';
import API from '../api'; // Importamos la instancia de Axios que configuraste
// Componente de Login
export default function Login({ onLoginSuccess }) {
    const [formData, setFormData] = useState({
        email: '',
        password: '',
    });
    const [loading, setLoading] = useState(false);
    const [error, setError] = useState('');

    const handleChange = (e) => {
        setFormData({
            ...formData,
            [e.target.name]: e.target.value,
        });
    };

    const handleSubmit = async (e) => {
        e.preventDefault();
        setLoading(true);
        setError('');

        try {
            const response = await API.post('/Auth/Login', formData);

            // 🔍 Mirá esto en la consola F12 para ver la estructura exacta:
            console.log('Respuesta del Backend:', response.data);

            const data = response.data;

            // Intentamos extraer el token de cualquier estructura posible:
            const token = data.token || data.Token || data.result?.token || (typeof data === 'string' ? data : null);

            if (token) {
                localStorage.setItem('token', token);

                // Notificamos a App.jsx para cambiar de pantalla
                if (onLoginSuccess) {
                    onLoginSuccess();
                }
            } else {
                setError('Se recibió respuesta 200 OK pero no se detectó el token en el JSON.');
            }
        } catch (err) {
            console.error('Error en Login:', err);
            setError('Error al iniciar sesión.');
        } finally {
            setLoading(false);
        }
    };

    return (
        <div style={styles.container}>
            <form onSubmit={handleSubmit} style={styles.card}>
                <h2 style={{ textAlign: 'center', marginBottom: '1.5rem' }}>Iniciar Sesión - SubastaYa</h2>

                {error && <div style={styles.errorAlert}>{error}</div>}

                <div style={styles.field}>
                    <label>Email / Usuario:</label>
                    <input
                        id="email-input"
                        type="email"
                        name="email"
                        autoComplete="username"
                        value={formData.email}
                        onChange={handleChange}
                        required
                        placeholder="ejemplo@correo.com"
                    />
                </div>

                <div style={styles.field}>
                    <label>Contraseña:</label>
                    <input
                        id="password-input"
                        type="password"
                        name="password"
                        autoComplete="current-password"
                        value={formData.password}
                        onChange={handleChange}
                        required
                        placeholder="••••••••"
                    />
                </div>

                <button type="submit" disabled={loading} style={styles.button}>
                    {loading ? 'Ingresando...' : 'Iniciar Sesión'}
                </button>
            </form>
        </div>
    );
}

// Estilos en línea para el componente
const styles = {
    container: {
        display: 'flex',
        justifyContent: 'center',
        alignItems: 'center',
        minHeight: '80vh',
        padding: '1rem',
    },
    card: {
        width: '100%',
        maxWidth: '380px',
        padding: '2rem',
        borderRadius: '8px',
        boxShadow: '0 4px 12px rgba(0,0,0,0.15)',
        backgroundColor: '#ffffff',
    },
    field: {
        marginBottom: '1rem',
        display: 'flex',
        flexDirection: 'column',
        gap: '0.4rem',
    },
    input: {
        padding: '0.6rem',
        borderRadius: '4px',
        border: '1px solid #ccc',
        fontSize: '1rem',
    },
    button: {
        width: '100%',
        padding: '0.75rem',
        backgroundColor: '#007bff',
        color: '#ffffff',
        border: 'none',
        borderRadius: '4px',
        fontSize: '1rem',
        fontWeight: 'bold',
        cursor: 'pointer',
        marginTop: '0.5rem',
    },
    errorAlert: {
        padding: '0.75rem',
        marginBottom: '1rem',
        color: '#721c24',
        backgroundColor: '#f8d7da',
        border: '1px solid #f5c6cb',
        borderRadius: '4px',
        fontSize: '0.9rem',
    },
};
