# Análisis del Proyecto Hospital

Este documento detalla la arquitectura, las tecnologías empleadas y las características principales del proyecto ubicado en el repositorio `Hospital`. Se trata de una aplicación cliente-servidor para la gestión hospitalaria.

---

## 🏛️ Arquitectura del Proyecto

El sistema está dividido en dos partes principales: un **Frontend** (interfaz de usuario) y un **Backend** (API y lógica de negocio).

### 1. Backend (`Hospital_backend`)
El backend está construido bajo una **Arquitectura en N-Capas (N-Tier)** utilizando **.NET Framework 4.7.2**. Esta separación de responsabilidades asegura un código más mantenible y escalable. Las capas del proyecto son:

*   **01- Comun**: Contiene elementos transversales a toda la aplicación, como enumeraciones, constantes, utilidades o DTOs (Data Transfer Objects).
*   **02- Modelo**: Define el modelo de datos. Utiliza **Entity Framework 6.5** con un enfoque _Database First_ o _Model First_ (evidenciado por el uso del archivo `Model.edmx`). Aquí se definen las entidades principales de la base de datos: `Medico`, `Paciente`, `Ingreso` y `Egreso`.
*   **03- Acceso a Datos (Datos)**: Capa encargada de interactuar directamente con la base de datos utilizando el contexto (`DbContext`) definido en la capa Modelo.
*   **04- Cliente Rest (Rest)**: Capa destinada probablemente a consumir APIs externas o servicios de terceros.
*   **05- Lógica de Negocio (Logica)**: Contiene las reglas del negocio de la aplicación. Actúa como puente entre los controladores de la API y la capa de acceso a datos.
*   **06- Web API (WebAPI)**: Capa de presentación del backend. Utiliza **ASP.NET Web API 2** para exponer endpoints RESTful (como `MedicoController`) que son consumidos por el Frontend.

### 2. Frontend (`Hospital_Frontend`)
El frontend es una aplicación web de una sola página (SPA - Single Page Application) desarrollada con **Angular 21**.

*   **Estructura**: Sigue una arquitectura modular por características (Feature Modules). Dentro de `src/app/modules`, el código está dividido en directorios específicos para cada funcionalidad.
*   **Diseño e Interfaz**: Emplea **Angular Material** y **Bootstrap 5.3** para los componentes visuales y el sistema de grillas, asegurando un diseño moderno y responsivo. También utiliza **Sass (SCSS)** para el manejo de los estilos.
*   **Otras Librerías**: Utiliza `ngx-toastr` para mostrar notificaciones al usuario y `RxJS` para el manejo de operaciones asíncronas y programación reactiva (estándar en Angular).

---

## ⚙️ Características y Funcionalidades (Features)

Basado en la estructura de los módulos del frontend y las entidades del modelo de base de datos en el backend, el sistema gestiona cuatro entidades o procesos principales del dominio hospitalario:

| Módulo | Entidad Backend | Descripción |
| :--- | :--- | :--- |
| **👨‍⚕️ Gestión de Médicos** | `Medico.cs` | Administración de la información del personal médico (altas, bajas, modificaciones y consultas). |
| **🤒 Gestión de Pacientes** | `Paciente.cs` | Mantenimiento del registro y los datos personales de los pacientes. |
| **🏥 Ingresos (Admissions)** | `Ingreso.cs` | Gestión del proceso de internación o admisión de un paciente al hospital (asignación de camas, médico a cargo, fecha, etc.). |
| **👋 Egresos (Discharges)** | `Egreso.cs` | Procesamiento del alta de los pacientes (fecha de salida, motivo, etc.). |

---

## 🔍 Resumen del Stack Tecnológico

> [!NOTE]
> **Frontend:**
> *   Angular 21, TypeScript
> *   Angular Material, Bootstrap 5.3
> *   Sass (SCSS), RxJS
>
> **Backend:**
> *   C#, .NET Framework 4.7.2
> *   ASP.NET Web API 2
>
> **ORM / Base de Datos:**
> *   Entity Framework 6.5 (EDMX)
> *   SQL Server (presumiblemente)
