export interface MedicoVMR {
  id: number;
  cedula: string;
  nombre: string;
  apellidoPaterno?: string;
  apellidoMaterno?: string;
  esEspecialista: boolean;
  habilitado: boolean;
}

export interface MedicoCrear {
  cedula: string;
  nombre: string;
  apellidoPaterno: string;
  apellidoMaterno?: string;
  esEspecialista: boolean;
  habilitado: boolean;
}
