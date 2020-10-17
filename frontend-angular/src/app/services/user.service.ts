import { environment } from './../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

import UserModel from '../models/user.model';
import TokenModel from '../models/token.model';

@Injectable({ providedIn: 'root' })
export class UserService {
  constructor(
    private http: HttpClient
  ) { }

  pegarIdUsuarioPorCpf(cpf: number) {
    return this.http.get<UserModel>(`${environment.services.hublogistica_registration}/user/filter/cpf/${cpf}`);
  }

  pegarCpfUsuarioPorId(id: string) {
    return this.http.get<UserModel>(`${environment.services.hublogistica_registration}/user/filter/id/${id}`);
  }

  cadastrarNovoUsuario(user: UserModel) {
    return this.http.post<UserModel>(`${environment.services.hublogistica_registration}/user`, user);
  }

  validarLogin(user: UserModel) {
    return this.http.post<TokenModel>(`${environment.services.hublogistica_registration}/user/login`, user);
  }
}
