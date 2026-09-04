import { Component, OnInit, inject, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import {
  FormBuilder,
  FormGroup,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatSlideToggleModule } from '@angular/material/slide-toggle';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatCardModule } from '@angular/material/card';
import { ToastrService } from 'ngx-toastr';

import { MedicoService } from '../../../../services/medico.service';
import { MedicoCrear, MedicoVMR } from '../../../../models/medico.model';

@Component({
  selector: 'app-medico-form',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatFormFieldModule,
    MatInputModule,
    MatSlideToggleModule,
    MatButtonModule,
    MatIconModule,
    MatProgressSpinnerModule,
    MatCardModule,
  ],
  templateUrl: './form.html',
  styleUrl: './form.scss',
})
export class Form implements OnInit {
  private readonly fb = inject(FormBuilder);
  private readonly route = inject(ActivatedRoute);
  private readonly router = inject(Router);
  private readonly medicoService = inject(MedicoService);
  private readonly toastr = inject(ToastrService);

  readonly esEdicion = signal<boolean>(false);
  readonly medicoId = signal<number | null>(null);
  readonly cargando = signal<boolean>(false);
  readonly guardando = signal<boolean>(false);

  form: FormGroup = this.fb.group({
    cedula: ['', [Validators.required, Validators.maxLength(10)]],
    nombre: ['', [Validators.required, Validators.maxLength(50)]],
    apellidoPaterno: ['', [Validators.required, Validators.maxLength(50)]],
    apellidoMaterno: ['', [Validators.maxLength(50)]],
    esEspecialista: [false, [Validators.required]],
    habilitado: [true, [Validators.required]],
  });

  ngOnInit(): void {
    const idParam = this.route.snapshot.paramMap.get('id');
    if (idParam) {
      const id = Number(idParam);
      if (!isNaN(id) && id > 0) {
        this.esEdicion.set(true);
        this.medicoId.set(id);
        this.cargarDatos(id);
      } else {
        this.toastr.warning('El identificador del médico no es válido', 'Atención');
        this.router.navigate(['/medicos']);
      }
    }
  }

  cargarDatos(id: number): void {
    this.cargando.set(true);
    this.medicoService.leerUno(id).subscribe({
      next: (resp) => {
        this.cargando.set(false);
        if (resp && resp.datos) {
          this.form.patchValue({
            cedula: resp.datos.cedula,
            nombre: resp.datos.nombre,
            apellidoPaterno: resp.datos.apellidoPaterno,
            apellidoMaterno: resp.datos.apellidoMaterno || '',
            esEspecialista: resp.datos.esEspecialista ?? false,
            habilitado: resp.datos.habilitado ?? true,
          });
        } else {
          this.toastr.error('No se encontraron datos para el médico especificado', 'Error');
          this.router.navigate(['/medicos']);
        }
      },
      error: (err) => {
        this.cargando.set(false);
        this.toastr.error('Error al recuperar información del médico', 'Error');
        console.error(err);
        this.router.navigate(['/medicos']);
      },
    });
  }

  guardar(): void {
    if (this.form.invalid) {
      this.form.markAllAsTouched();
      this.toastr.warning('Por favor complete todos los campos obligatorios.', 'Formulario Inválido');
      return;
    }

    this.guardando.set(true);
    const formValue = this.form.value;

    if (this.esEdicion() && this.medicoId()) {
      const itemUpdate: MedicoVMR = {
        id: this.medicoId()!,
        cedula: formValue.cedula,
        nombre: formValue.nombre,
        apellidoPaterno: formValue.apellidoPaterno,
        apellidoMaterno: formValue.apellidoMaterno,
        esEspecialista: formValue.esEspecialista,
        habilitado: formValue.habilitado,
      };

      this.medicoService.actualizar(this.medicoId()!, itemUpdate).subscribe({
        next: (resp) => {
          this.guardando.set(false);
          if (resp && resp.datos) {
            this.toastr.success('Médico actualizado con éxito', 'Guardado');
            this.router.navigate(['/medicos']);
          } else {
            const msg = resp?.mensajes?.join(', ') || 'No se pudo actualizar el médico.';
            this.toastr.error(msg, 'Error');
          }
        },
        error: (err) => {
          this.guardando.set(false);
          this.toastr.error('Ocurrió un error al actualizar el médico.', 'Error');
          console.error(err);
        },
      });
    } else {
      const itemCreate: MedicoCrear = {
        cedula: formValue.cedula,
        nombre: formValue.nombre,
        apellidoPaterno: formValue.apellidoPaterno,
        apellidoMaterno: formValue.apellidoMaterno,
        esEspecialista: formValue.esEspecialista,
        habilitado: formValue.habilitado,
      };

      this.medicoService.crear(itemCreate).subscribe({
        next: (resp) => {
          this.guardando.set(false);
          if (resp && resp.datos) {
            this.toastr.success('Médico creado con éxito', 'Registro Exitoso');
            this.router.navigate(['/medicos']);
          } else {
            const msg = resp?.mensajes?.join(', ') || 'No se pudo registrar el médico.';
            this.toastr.error(msg, 'Error');
          }
        },
        error: (err) => {
          this.guardando.set(false);
          this.toastr.error('Ocurrió un error al crear el médico.', 'Error');
          console.error(err);
        },
      });
    }
  }

  cancelar(): void {
    this.router.navigate(['/medicos']);
  }
}
