# SubastaYa - API RESTful backend

Plataforma web de subastas en tiempo real y comercio electrónico desarrollada como trabajo práctico para la cátedra **Proyecto de Software** de la carrera **Ingeniería en Informática**.

---

## Arquitectura y Tecnologías
- **Lenguaje / Framework:** .NET 8 / C#
- **Base de Datos:** PostgreSQL
- **ORM:** Entity Framework Core
- **Manejo de Concurrencia:** Optimistic Concurrency Control (`[ConcurrencyCheck]`)
- **Procesos en Segundo Plano:** `BackgroundService` para liquidación automática de subastas
- **Seguridad:** Hash de contraseñas con `BCrypt.Net`

---

## Requisitos e Instalación

### Pre-requisitos
1. .NET 8 SDK instalado.
2. Servidor de PostgreSQL corriendo localmente en el puerto `5432`.

### Configuración de la Base de Datos
1. Configura la cadena de conexión en `appsettings.json`: 
```json
   "ConnectionStrings": {
     "DefaultConnection": "Host=localhost;Database=SubastaYA;Username=postgres;Password=1234"
   }