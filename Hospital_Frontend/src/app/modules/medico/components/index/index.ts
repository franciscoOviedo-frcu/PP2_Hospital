import { Component, OnInit, inject, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { MatTableModule } from '@angular/material/table';
import { MatPaginatorModule, PageEvent } from '@angular/material/paginator';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatChipsModule } from '@angular/material/chips';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatTooltipModule } from '@angular/material/tooltip';
import { ToastrService } from 'ngx-toastr';

import { MedicoService } from '../../../../services/medico.service';
import { MedicoVMR } from '../../../../models/medico.model';

@Component({
  selector: 'app-medico-index',
  standalone: true,
  imports: [
    CommonModule,
    FormsModule,
    MatTableModule,
    MatPaginatorModule,
    MatProgressSpinnerModule,
    MatButtonModule,
    MatIconModule,
    MatFormFieldModule,
    MatInputModule,
    MatChipsModule,
    MatCheckboxModule,
    MatTooltipModule,
  ],
  templateUrl: './index.html',
  styleUrl: './index.scss',
})
export class Index implements OnInit {
  private readonly medicoService = inject(MedicoService);
  private readonly toastr = inject(ToastrService);
  private readonly router = inject(Router);

  // Estados reactivos con Signals
  readonly medicos = signal<MedicoVMR[]>([]);
  readonly totalRegistros = signal<number>(0);
  readonly cargando = signal<boolean>(false);
  readonly pagina = signal<number>(0);
  readonly cantidadPorPagina = signal<number>(10);
  readonly textoBusqueda = signal<string>('');
  readonly seleccionados = signal<Set<number>>(new Set<number>());

  readonly displayedColumns: string[] = [
    'select',
    'id',
    'cedula',
    'nombre',
    'esEspecialista',
    'habilitado',
    'acciones',
  ];

  readonly pageSizeOptions = [5, 10, 25, 50];

  ngOnInit(): void {
    this.cargarMedicos();
  }

  cargarMedicos(): void {
    this.cargando.set(true);
    this.medicoService
      .leerTodo(
        this.cantidadPorPagina(),
        this.pagina(),
        this.textoBusqueda()
      )
      .subscribe({
        next: (resp) => {
          this.cargando.set(false);
          if (resp && resp.datos) {
            this.medicos.set(resp.datos.elementos || []);
            this.totalRegistros.set(resp.datos.cantidadTotal || 0);
          } else {
            this.medicos.set([]);
            this.totalRegistros.set(0);
          }
          // Limpiar selección no válida
          this.seleccionados.set(new Set());
        },
        error: (err) => {
          this.cargando.set(false);
          this.toastr.error(
            'Error al cargar el listado de médicos. Verifique la conexión con el servidor.',
            'Error de Servidor'
          );
          console.error(err);
        },
      });
  }

  onBuscar(): void {
    this.pagina.set(0);
    this.cargarMedicos();
  }

  onLimpiarBusqueda(): void {
    this.textoBusqueda.set('');
    this.pagina.set(0);
    this.cargarMedicos();
  }

  onCambioPagina(event: PageEvent): void {
    this.pagina.set(event.pageIndex);
    this.cantidadPorPagina.set(event.pageSize);
    this.cargarMedicos();
  }

  estaSeleccionado(id: number): boolean {
    return this.seleccionados().has(id);
  }

  toggleSeleccion(id: number): void {
    const actual = new Set(this.seleccionados());
    if (actual.has(id)) {
      actual.delete(id);
    } else {
      actual.add(id);
    }
    this.seleccionados.set(actual);
  }

  todosSeleccionados(): boolean {
    const lista = this.medicos();
    if (lista.length === 0) return false;
    return lista.every((m) => this.seleccionados().has(m.id));
  }

  algunoSeleccionado(): boolean {
    const lista = this.medicos();
    const count = lista.filter((m) => this.seleccionados().has(m.id)).length;
    return count > 0 && count < lista.length;
  }

  toggleSeleccionarTodos(): void {
    if (this.todosSeleccionados()) {
      this.seleccionados.set(new Set());
    } else {
      const todos = new Set(this.medicos().map((m) => m.id));
      this.seleccionados.set(todos);
    }
  }

  navegarCrear(): void {
    this.router.navigate(['/medicos/nuevo']);
  }

  navegarEditar(id: number): void {
    this.router.navigate(['/medicos/editar', id]);
  }

  eliminarUno(medico: MedicoVMR): void {
    if (
      confirm(
        `¿Está seguro de que desea eliminar al médico "${medico.nombre}" (Cédula: ${medico.cedula})?`
      )
    ) {
      this.cargando.set(true);
      this.medicoService.eliminar([medico.id]).subscribe({
        next: (resp) => {
          this.cargando.set(false);
          if (resp && resp.datos) {
            this.toastr.success(
              `Médico "${medico.nombre}" eliminado correctamente`,
              'Operación Exitosa'
            );
            this.cargarMedicos();
          } else {
            const errorMsg =
              resp?.mensajes?.join(', ') || 'No se pudo eliminar el registro.';
            this.toastr.error(errorMsg, 'Error al Eliminar');
          }
        },
        error: (err) => {
          this.cargando.set(false);
          this.toastr.error(
            'Ocurrió un error al intentar eliminar el registro.',
            'Error'
          );
          console.error(err);
        },
      });
    }
  }

  eliminarSeleccionados(): void {
    const ids = Array.from(this.seleccionados());
    if (ids.length === 0) return;

    if (
      confirm(
        `¿Está seguro de que desea eliminar ${ids.length} médico(s) seleccionado(s)?`
      )
    ) {
      this.cargando.set(true);
      this.medicoService.eliminar(ids).subscribe({
        next: (resp) => {
          this.cargando.set(false);
          if (resp && resp.datos) {
            this.toastr.success(
              `Se eliminaron ${ids.length} médico(s) correctamente`,
              'Operación Exitosa'
            );
            this.seleccionados.set(new Set());
            this.cargarMedicos();
          } else {
            const errorMsg =
              resp?.mensajes?.join(', ') || 'No se pudieron eliminar los registros.';
            this.toastr.error(errorMsg, 'Error al Eliminar');
          }
        },
        error: (err) => {
          this.cargando.set(false);
          this.toastr.error(
            'Ocurrió un error al intentar eliminar los registros seleccionados.',
            'Error'
          );
          console.error(err);
        },
      });
    }
  }
}
