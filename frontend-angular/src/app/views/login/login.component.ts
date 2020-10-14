import { UserService } from './../../services/user.service';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import UserModel from 'src/app/models/user.model';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  public loginForm: FormGroup;

  public id: string;
  public cpf: number;
  public senha: string;

  public userModel: UserModel;

  constructor(
    private fb: FormBuilder,
    private userService: UserService,
    private router: Router
  ) { }

  buildForm() {
    this.loginForm = this.fb.group({
      cpf: [this.loginForm, [Validators.required]],
      senha: [this.loginForm, [Validators.required]]
    });
  }

  ngOnInit(): void {
    this.buildForm();
  }

  async setFormulario() {
    this.cpf = Number(this.loginForm.value.cpf);
    this.senha = this.loginForm.value.senha;
  }

  async submit() {
    this.setFormulario();
    await this.userService.pegarIdUsuarioPorCpf(this.cpf)
    .subscribe(resp => {
      if (resp.cpf === this.cpf && resp.password === this.senha) {
        localStorage.setItem('id', resp.id);
        console.log(resp.id);
        this.router.navigate(['home']);
        } else {
          alert('Cpf e/ou Senha não conferem');
        }
      });
  }
}
