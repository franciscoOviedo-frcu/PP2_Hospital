export interface RespuestaVMR<T> {
  codigo: number;
  mensajes: string[];
  datos: T;
}

export interface ListadoPaginadoVMR<T> {
  cantidadTotal: number;
  elementos: T[];
}
