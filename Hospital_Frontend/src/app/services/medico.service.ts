import { Injectable, inject } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { API_CONFIG } from '../config/api.config';
import { RespuestaVMR, ListadoPaginadoVMR } from '../models/respuesta.model';
import { MedicoVMR, MedicoCrear } from '../models/medico.model';

@Injectable({
  providedIn: 'root',
})
export class MedicoService {
  private readonly http = inject(HttpClient);
  private readonly apiUrl = API_CONFIG.endpoints.medico;

  /**
   * Obtiene la lista paginada de médicos con filtro de búsqueda opcional.
   * @param cantidad Cantidad de elementos por página (default 10)
   * @param pagina Índice de página (base 0)
   * @param textobusqueda Texto para buscar por cédula o nombre
   */
  leerTodo(
    cantidad: number = 10,
    pagina: number = 0,
    textobusqueda: string = ''
  ): Observable<RespuestaVMR<ListadoPaginadoVMR<MedicoVMR>>> {
    let params = new HttpParams()
      .set('cantidad', cantidad.toString())
      .set('pagina', pagina.toString());

    if (textobusqueda && textobusqueda.trim() !== '') {
      params = params.set('textobusqueda', textobusqueda.trim());
    }

    return this.http.get<RespuestaVMR<ListadoPaginadoVMR<MedicoVMR>>>(
      `${this.apiUrl}/leer-todo`,
      { params }
    );
  }

  /**
   * Obtiene los datos detallados de un médico por su ID.
   * @param id Identificador del médico
   */
  leerUno(id: number): Observable<RespuestaVMR<MedicoVMR>> {
    return this.http.get<RespuestaVMR<MedicoVMR>>(`${this.apiUrl}/${id}`);
  }

  /**
   * Crea un nuevo registro de médico.
   * @param item Objeto con los datos del médico
   */
  crear(item: MedicoCrear): Observable<RespuestaVMR<number>> {
    return this.http.post<RespuestaVMR<number>>(this.apiUrl, item);
  }

  /**
   * Actualiza la información de un médico existente.
   * @param id Identificador del médico
   * @param item Datos a actualizar
   */
  actualizar(id: number, item: MedicoVMR): Observable<RespuestaVMR<boolean>> {
    return this.http.put<RespuestaVMR<boolean>>(`${this.apiUrl}/${id}`, item);
  }

  /**
   * Realiza el borrado lógico de uno o varios médicos según sus IDs.
   * @param ids Arreglo con los IDs de los médicos a eliminar
   */
  eliminar(ids: number[]): Observable<RespuestaVMR<boolean>> {
    return this.http.delete<RespuestaVMR<boolean>>(this.apiUrl, {
      body: ids,
    });
  }
}
