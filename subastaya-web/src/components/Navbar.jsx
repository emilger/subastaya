// src/components/Navbar.jsx
import React, { useState } from 'react';

export function Navbar({ 
  onLogout, 
  onNavigate,
  busqueda = '',
  setBusqueda,
  categoriaFiltro = 'todas',
  setCategoriaFiltro,
  estadoFiltro = 'todos',
  setEstadoFiltro,
  ordenValor = 'recientes',
  setOrdenValor
}) {
  const [dropdownCuenta, setDropdownCuenta] = useState(false);
  const [dropdownFiltros, setDropdownFiltros] = useState(false);
  const [hoverFiltros, setHoverFiltros] = useState(false);
  const [hoverCuenta, setHoverCuenta] = useState(false);
  const [hoverLupa, setHoverLupa] = useState(false);
  const [expandirBuscador, setExpandirBuscador] = useState(false);

  const handleNavegar = (vista) => {
    onNavigate(vista);
    setDropdownCuenta(false);
    setDropdownFiltros(false);
  };

  const handleLogoutMenu = () => {
    onLogout();
    setDropdownCuenta(false);
    setDropdownFiltros(false);
  };

  return (
    <nav style={{ 
      backgroundColor: '#1e1e1e', 
      padding: '16px 30px', 
      display: 'flex', 
      alignItems: 'center',
      justify: 'space-between',
      color: '#ffffff', 
      boxShadow: '0 2px 10px rgba(0,0,0,0.4)', 
      position: 'relative',
      zIndex: 1000
    }}>
      
      {/* 1. SECCIÓN IZQUIERDA: LUPA + BUSCADOR + MENÚ FILTROS */}
      <div style={{ display: 'flex', alignItems: 'center', gap: '20px', flex: 1, justifyContent: 'flex-start' }}>
        
        {/* LUPA Y CAMPO DE BÚSQUEDA DESPLEGABLE EN HOVER */}
        <div 
          onMouseEnter={() => {
            setExpandirBuscador(true);
            setHoverLupa(true);
          }}
          onMouseLeave={() => {
            setExpandirBuscador(false);
            setHoverLupa(false);
          }}
          style={{ 
            display: 'flex', 
            alignItems: 'center', 
            backgroundColor: expandirBuscador ? '#2b2b2b' : 'transparent',
            borderRadius: '20px',
            padding: '6px 10px',
            transition: 'all 0.3s ease',
            border: expandirBuscador ? '1px solid #444' : '1px solid transparent',
            minWidth: '36px'
          }}
        >
          <svg 
            width="18" 
            height="18" 
            viewBox="0 0 24 24" 
            fill="none" 
            stroke={hoverLupa || expandirBuscador ? '#00FF66' : '#ffffff'} 
            strokeWidth="2.5" 
            strokeLinecap="round" 
            strokeLinejoin="round"
            style={{ cursor: 'pointer', flexShrink: 0, transition: 'stroke 0.2s ease' }}
          >
            <circle cx="11" cy="11" r="8"></circle>
            <line x1="21" y1="21" x2="16.65" y2="16.65"></line>
          </svg>

          <input
            type="text"
            placeholder="Buscar palabras clave..."
            value={busqueda}
            onChange={(e) => setBusqueda && setBusqueda(e.target.value)}
            style={{
              width: expandirBuscador ? '180px' : '0px',
              opacity: expandirBuscador ? 1 : 0,
              padding: expandirBuscador ? '4px 8px' : '0px',
              border: 'none',
              backgroundColor: 'transparent',
              color: '#ffffff',
              fontSize: '14px',
              outline: 'none',
              transition: 'all 0.3s ease',
              marginLeft: expandirBuscador ? '6px' : '0px'
            }}
          />
        </div>

        {/* MENÚ "FILTROS" CON SUBRAYADO EN HOVER */}
        <div style={{ position: 'relative' }}>
          <button
            onClick={() => {
              setDropdownFiltros(!dropdownFiltros);
              setDropdownCuenta(false);
            }}
            onMouseEnter={() => setHoverFiltros(true)}
            onMouseLeave={() => setHoverFiltros(false)}
            style={{
              background: 'none',
              border: 'none',
              color: '#ffffff',
              fontSize: '15px',
              cursor: 'pointer',
              fontWeight: '500',
              padding: '4px 0',
              borderBottom: hoverFiltros || dropdownFiltros ? '2px solid #00FF66' : '2px solid transparent',
              transition: 'border-color 0.2s ease',
              outline: 'none'
            }}
          >
            Filtros ▾
          </button>

          {/* DESPLEGABLE CON LOS FILTROS SEGÚN MODELO DE DOMINIO */}
          {dropdownFiltros && (
            <div style={{
              position: 'absolute',
              left: 0,
              top: '140%',
              backgroundColor: '#2b2b2b',
              borderRadius: '10px',
              boxShadow: '0 6px 20px rgba(0,0,0,0.6)',
              padding: '16px',
              minWidth: '230px',
              display: 'flex',
              flexDirection: 'column',
              gap: '12px',
              zIndex: 1001,
              border: '1px solid #444'
            }}>
              <div>
                <label style={{ display: 'block', fontSize: '12px', color: '#aaa', marginBottom: '4px', fontWeight: 'bold' }}>
                  Categoría
                </label>
                <select
                  value={categoriaFiltro}
                  onChange={(e) => setCategoriaFiltro && setCategoriaFiltro(e.target.value)}
                  style={{
                    width: '100%',
                    padding: '8px',
                    borderRadius: '6px',
                    border: '1px solid #444',
                    backgroundColor: '#1e1e1e',
                    color: '#ffffff',
                    fontSize: '13px',
                    outline: 'none'
                  }}
                >
                  <option value="todas">Todas las categorías</option>
                  <option value="1">Tecnología</option>
                  <option value="2">Coleccionables</option>
                  <option value="3">Indumentaria</option>
                  <option value="4">Vehículos</option>
                  <option value="5">Arte</option>
                </select>
              </div>

              <div>
                <label style={{ display: 'block', fontSize: '12px', color: '#aaa', marginBottom: '4px', fontWeight: 'bold' }}>
                  Estado
                </label>
                <select
                  value={estadoFiltro}
                  onChange={(e) => setEstadoFiltro && setEstadoFiltro(e.target.value)}
                  style={{
                    width: '100%',
                    padding: '8px',
                    borderRadius: '6px',
                    border: '1px solid #444',
                    backgroundColor: '#1e1e1e',
                    color: '#ffffff',
                    fontSize: '13px',
                    outline: 'none'
                  }}
                >
                  <option value="todos">Todos los estados</option>
                  <option value="ACTIVA">Activas </option>
                  <option value="PROGRAMADA">Programadas </option>
                  <option value="FINALIZADA">Finalizadas</option>
                  <option value="DESIERTA">Desiertas</option>
                </select>
              </div>

              <div>
                <label style={{ display: 'block', fontSize: '12px', color: '#aaa', marginBottom: '4px', fontWeight: 'bold' }}>
                  Ordenar por Valor
                </label>
                <select
                  value={ordenValor}
                  onChange={(e) => setOrdenValor && setOrdenValor(e.target.value)}
                  style={{
                    width: '100%',
                    padding: '8px',
                    borderRadius: '6px',
                    border: '1px solid #444',
                    backgroundColor: '#1e1e1e',
                    color: '#ffffff',
                    fontSize: '13px',
                    outline: 'none'
                  }}
                >
                  <option value="recientes">Más recientes</option>
                  <option value="precio_asc">Precio: más bajo</option>
                  <option value="precio_desc">Precio: más alto</option>
                </select>
              </div>
            </div>
          )}
        </div>

      </div>

      {/* 2. SECCIÓN CENTRO: NOMBRE DE LA PÁGINA ("SubastaYA") */}
      <div 
        onClick={() => handleNavegar('catalogo')} 
        style={{ 
          cursor: 'pointer', 
          fontSize: '22px', 
          fontWeight: 'bold', 
          textAlign: 'center',
          flex: 1
        }}
      >
        <span style={{ color: '#ffffff' }}>Subasta</span>
        <span style={{ color: '#00FF66' }}>YA</span>
      </div>

      {/* 3. SECCIÓN DERECHA: MENÚ "MI CUENTA" CON SUBRAYADO EN HOVER */}
      <div style={{ flex: 1, display: 'flex', justifyContent: 'flex-end', position: 'relative' }}>
        <div style={{ position: 'relative' }}>
          <button
            onClick={() => {
              setDropdownCuenta(!dropdownCuenta);
              setDropdownFiltros(false);
            }}
            onMouseEnter={() => setHoverCuenta(true)}
            onMouseLeave={() => setHoverCuenta(false)}
            style={{
              background: 'none',
              border: 'none',
              color: '#ffffff',
              fontSize: '15px',
              cursor: 'pointer',
              fontWeight: '500',
              padding: '4px 0',
              borderBottom: hoverCuenta || dropdownCuenta ? '2px solid #00FF66' : '2px solid transparent',
              transition: 'border-color 0.2s ease',
              outline: 'none'
            }}
          >
            Mi Cuenta ▾
          </button>

          {dropdownCuenta && (
            <div style={{
              position: 'absolute',
              right: 0,
              top: '140%',
              backgroundColor: '#2b2b2b',
              borderRadius: '8px',
              boxShadow: '0 4px 16px rgba(0,0,0,0.5)',
              minWidth: '180px',
              overflow: 'hidden',
              zIndex: 1001,
              border: '1px solid #444'
            }}>
              <button
                onClick={() => handleNavegar('mis-subastas')}
                style={{
                  width: '100%',
                  padding: '12px 16px',
                  background: 'none',
                  border: 'none',
                  color: '#ffffff',
                  textAlign: 'left',
                  cursor: 'pointer',
                  fontSize: '14px'
                }}
                onMouseEnter={(e) => e.target.style.backgroundColor = '#3a3a3a'}
                onMouseLeave={(e) => e.target.style.backgroundColor = 'transparent'}
              >
                Mis subastas
              </button>

              <button
                onClick={() => handleNavegar('billetera')}
                style={{
                  width: '100%',
                  padding: '12px 16px',
                  background: 'none',
                  border: 'none',
                  color: '#ffffff',
                  textAlign: 'left',
                  cursor: 'pointer',
                  fontSize: '14px'
                }}
                onMouseEnter={(e) => e.target.style.backgroundColor = '#3a3a3a'}
                onMouseLeave={(e) => e.target.style.backgroundColor = 'transparent'}
              >
                Mi billetera
              </button>

              <button
                onClick={() => handleNavegar('configuracion')}
                style={{
                  width: '100%',
                  padding: '12px 16px',
                  background: 'none',
                  border: 'none',
                  color: '#ffffff',
                  textAlign: 'left',
                  cursor: 'pointer',
                  fontSize: '14px'
                }}
                onMouseEnter={(e) => e.target.style.backgroundColor = '#3a3a3a'}
                onMouseLeave={(e) => e.target.style.backgroundColor = 'transparent'}
              >
                Configuración
              </button>

              <div style={{ borderTop: '1px solid #444' }} />

              <button
                onClick={handleLogoutMenu}
                style={{
                  width: '100%',
                  padding: '12px 16px',
                  background: 'none',
                  border: 'none',
                  color: '#dc3545',
                  textAlign: 'left',
                  cursor: 'pointer',
                  fontSize: '14px',
                  fontWeight: 'bold'
                }}
                onMouseEnter={(e) => e.target.style.backgroundColor = '#3a3a3a'}
                onMouseLeave={(e) => e.target.style.backgroundColor = 'transparent'}
              >
                Cerrar sesión
              </button>
            </div>
          )}
        </div>
      </div>

    </nav>
  );
}

export default Navbar;