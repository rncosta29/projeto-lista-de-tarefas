import { Router } from '@angular/router';
import { UserService } from './../../services/user.service';
import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';

import UserModel from 'src/app/models/user.model';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {

  public registerForm: FormGroup;
  public userModel = new UserModel();

  public id: string;

  constructor(
    private userService: UserService,
    private fb: FormBuilder,
    private router: Router
  ) { }

  ngOnInit(): void {
    this.buildForm();
  }

  buildForm() {
    this.registerForm = this.fb.group({
      nome: [this.registerForm, [Validators.required]],
      email: [this.registerForm, [Validators.required]],
      cpf: [this.registerForm, [Validators.required, Validators.maxLength(11)]],
      senha: [this.registerForm, [Validators.required, Validators.maxLength(6)]],
      confirmarSenha: [this.registerForm, [Validators.required, Validators.maxLength(6)]]
    });
  }

  setFormulario() {
    this.userModel.name = this.registerForm.value.nome;
    this.userModel.email = this.registerForm.value.email;
    this.userModel.cpf = Number(this.registerForm.value.cpf);
    this.userModel.password = this.registerForm.value.senha;
    this.userModel.confirmPassword = this.registerForm.value.confirmarSenha;
  }

  voltar() {
    this.router.navigate(['']);
  }

  async submit() {
    this.setFormulario();

    if (this.userModel.password === this.userModel.confirmPassword) {
      console.log(this.userModel);
      this.userService.cadastrarNovoUsuario(this.userModel)
        .subscribe(resp => {
          localStorage.setItem('id', resp.id);
          this.router.navigate(['home']);
        // tslint:disable-next-line: no-shadowed-variable
        }, error => {
          console.log(error);
        });
    } else {
      alert('Senhas devem ser iguais');
    }
  }

}
