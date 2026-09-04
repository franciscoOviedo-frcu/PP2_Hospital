import { Routes } from '@angular/router';
import { Index as MedicoIndex } from './modules/medico/components/index/index';
import { Form as MedicoForm } from './modules/medico/components/form/form';

export const routes: Routes = [
  { path: '', redirectTo: 'medicos', pathMatch: 'full' },
  { path: 'medicos', component: MedicoIndex, title: 'Directorio de Médicos' },
  { path: 'medicos/nuevo', component: MedicoForm, title: 'Nuevo Médico' },
  { path: 'medicos/editar/:id', component: MedicoForm, title: 'Editar Médico' },
  { path: '**', redirectTo: 'medicos' },
];
