import { UserService } from './../../services/user.service';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import UserModel from 'src/app/models/user.model';
import TokenModel from 'src/app/models/token.model';

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

  public resultLogin = false;

  public userModel: UserModel;
  public tokenModel: TokenModel;

  constructor(
    private fb: FormBuilder,
    private userService: UserService,
    private router: Router
  ) { }

  buildForm() {
    this.loginForm = this.fb.group({
      cpf: [this.loginForm, [Validators.required, Validators.maxLength(11)]],
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

  async validarLogin(model: UserModel) {
    await this.userService.validarLogin(model)
      .subscribe(resp => {
        if (resp.token.length > 0) {
          this.resultLogin = true;
          this.tokenModel = resp;
          localStorage.setItem('token', this.tokenModel.token);
          console.log(this.resultLogin);
        } else {
          this.resultLogin = false;
        }
      });
  }

  async submit() {
    this.setFormulario();
    await this.userService.pegarIdUsuarioPorCpf(this.cpf)
    .subscribe(resp => {
      this.userModel = resp;
      this.validarLogin(this.userModel).then(r => {
        if (resp.cpf === this.cpf && resp.password === this.senha) {
          console.log(this.resultLogin);
          if (this.resultLogin === true) {
            localStorage.setItem('id', this.userModel.id);
            this.router.navigate(['home']);
          } else {
            alert('Alguma coisa deu errado');
          }

        } else {
          alert('Cpf e/ou Senha não conferem');
        }
      });
    });
  }
}
