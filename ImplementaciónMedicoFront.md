# Walkthrough: Integración de Endpoints de Médico en Angular Frontend

Se completó la implementación integral para conectar la API REST de **Médicos** (.NET 10 Web API) con la aplicación web en **Angular 21**.

---

## 📂 Archivos Creados y Modificados

### 1. Configuración y Modelos
* [**`api.config.ts`**](file:///home/francisco/proyectos/PP2_Hospital/Hospital_Frontend/src/app/config/api.config.ts): Define la URL base de la API (`http://localhost:5000/api`) y endpoints centralizados.
* [**`respuesta.model.ts`**](file:///home/francisco/proyectos/PP2_Hospital/Hospital_Frontend/src/app/models/respuesta.model.ts): Interfaces TypeScript para `RespuestaVMR<T>` y `ListadoPaginadoVMR<T>`.
* [**`medico.model.ts`**](file:///home/francisco/proyectos/PP2_Hospital/Hospital_Frontend/src/app/models/medico.model.ts): Interfaces `MedicoVMR` y `MedicoCrear`.

### 2. Capa de Servicios
* [**`medico.service.ts`**](file:///home/francisco/proyectos/PP2_Hospital/Hospital_Frontend/src/app/services/medico.service.ts): Servicio inyectable que implementa:
  * `leerTodo(cantidad, pagina, textobusqueda)` ➔ `GET api/Medico/leer-todo`
  * `leerUno(id)` ➔ `GET api/Medico/{id}`
  * `crear(item)` ➔ `POST api/Medico`
  * `actualizar(id, item)` ➔ `PUT api/Medico/{id}`
  * `eliminar(ids)` ➔ `DELETE api/Medico`

### 3. Configuración y Layout Principal
* [**`app.config.ts`**](file:///home/francisco/proyectos/PP2_Hospital/Hospital_Frontend/src/app/app.config.ts): Registra `provideHttpClient(withFetch())` y `provideToastr()`.
* [**`app.ts`**](file:///home/francisco/proyectos/PP2_Hospital/Hospital_Frontend/src/app/app.ts) & [**`app.html`**](file:///home/francisco/proyectos/PP2_Hospital/Hospital_Frontend/src/app/app.html): Barra de navegación responsiva (Bootstrap 5.3 + Material Icons) y contenedor para `<router-outlet />`.
* [**`app.routes.ts`**](file:///home/francisco/proyectos/PP2_Hospital/Hospital_Frontend/src/app/app.routes.ts): Enrutamiento para `/medicos`, `/medicos/nuevo` y `/medicos/editar/:id`.

### 4. Componentes del Módulo Médico
* [**`Index Component`**](file:///home/francisco/proyectos/PP2_Hospital/Hospital_Frontend/src/app/modules/medico/components/index/index.ts) ([Template](file:///home/francisco/proyectos/PP2_Hospital/Hospital_Frontend/src/app/modules/medico/components/index/index.html), [Styles](file:///home/francisco/proyectos/PP2_Hospital/Hospital_Frontend/src/app/modules/medico/components/index/index.scss)):
  * Tabla interactiva con `mat-table`.
  * Paginación enlazada al backend con `mat-paginator`.
  * Filtro de búsqueda por cédula o nombre.
  * Selección simple y múltiple para eliminación individual o en bloque.
  * Badges de estado (*Activo/Inactivo*) y especialidad (*Especialista/General*).
* [**`Form Component`**](file:///home/francisco/proyectos/PP2_Hospital/Hospital_Frontend/src/app/modules/medico/components/form/form.ts) ([Template](file:///home/francisco/proyectos/PP2_Hospital/Hospital_Frontend/src/app/modules/medico/components/form/form.html), [Styles](file:///home/francisco/proyectos/PP2_Hospital/Hospital_Frontend/src/app/modules/medico/components/form/form.scss)):
  * Formulario reactivo (`ReactiveFormsModule`) con validaciones (`Validators.required`, `Validators.maxLength`).
  * Modo dual automático (creación y edición con precarga de datos).
  * Switches visuales (`mat-slide-toggle`).
  * Feedback con notificaciones (`ToastrService`).

### 5. Documento de Procedimiento
* [**`procedimiento_medico_frontend.md`**](file:///home/francisco/proyectos/PP2_Hospital/procedimiento_medico_frontend.md): Guía paso a paso generada en la raíz del proyecto.

---

## 🔍 Resultados de Verificación

Se ejecutó la compilación de producción del frontend:
* **Comando:** `ng build`
* **Resultado:** `Application bundle generation complete` (0 errores, 0 advertencias).
