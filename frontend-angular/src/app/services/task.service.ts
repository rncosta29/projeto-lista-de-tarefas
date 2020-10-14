import { environment } from './../../environments/environment';
import TaskModel from 'src/app/models/task.model';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({ providedIn: 'root' })
export class TaskService {
  constructor(
    private http: HttpClient
  ) { }

  exibiirTasks(filtroAtivo: string, userId: string) {
    // tslint:disable-next-line: max-line-length
    return this.http.get<Array<TaskModel>>(`${environment.services.hublogistica_registration}/task/filter/${filtroAtivo}/${userId}`);
  }

  cadastrarTask(taskModel: TaskModel, userId: string) {
    return this.http.post<TaskModel>(`${environment.services.hublogistica_registration}/task/${userId}`, taskModel);
  }

  exibirTaskPorId(id: string) {
    return this.http.get<TaskModel>(`${environment.services.hublogistica_registration}/task/${id}`);
  }
}
