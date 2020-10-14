import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';

import TaskModel from 'src/app/models/task.model';
import { TaskService } from 'src/app/services/task.service';

import typeIcons from '../../utils/typeIcons';

@Component({
  selector: 'app-task',
  templateUrl: './task.component.html',
  styleUrls: ['./task.component.css']
})
export class TaskComponent implements OnInit {

  public cadastroForm: FormGroup;
  public newTask = new TaskModel();

  public tipo = typeIcons;
  public iconCalendar = '../../../assets/calendar.png';
  public iconClock = '../../../assets/clock.png';

  public numberIcon: number;
  public data: string;
  public hora: string;
  public title: string;
  public description: string;
  public date: string;
  public time: string;
  public id: string;

  public userId: string;

  constructor(
    private fb: FormBuilder,
    private taskService: TaskService,
    private router: Router,
    private route: ActivatedRoute
  ) { }

  ngOnInit(): void {
    this.userId = localStorage.getItem('id');
    this.buildForm();
    this.route.params.subscribe(param => this.id = param.id);
    this.validarId();
  }

  selecionarIcone(type: number) {
    this.numberIcon = type;
  }

  async validarId() {
    if (this.id) {
      await this.taskService.exibirTaskPorId(this.id)
        .subscribe(resp => {
          this.newTask = resp;
          this.title = this.newTask.title;
          this.description = this.newTask.description;
          this.data = '';
          this.time = '12:12';
          this.numberIcon = this.newTask.type;
        });

    }
  }

  buildForm() {
    this.cadastroForm = this.fb.group({
      title: [this.cadastroForm, [Validators.required]],
      description: [this.cadastroForm, [Validators.required]],
      date: [this.cadastroForm, [Validators.required]],
      time: [this.cadastroForm, [Validators.required]]
    });
  }

  setFormulario() {
    const data = this.cadastroForm.value.date + 'T' + this.cadastroForm.value.time + ':00.000';
    this.newTask.title = this.cadastroForm.value.title;
    this.newTask.description = this.cadastroForm.value.description;
    this.newTask.type = this.numberIcon;
    this.newTask.when = data;
    this.newTask.userId = this.userId;
  }

  async submit() {
    this.setFormulario();
    await this.taskService.cadastrarTask(this.newTask, this.newTask.userId)
      .subscribe(response => {
        if (response) {
          alert('Cartão cadastrado com sucesso!');
          this.router.navigate(['home']);
        } else {
          alert('Erro inesperado');
        }
      });
  }

}
