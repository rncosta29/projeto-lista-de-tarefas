import { UserService } from './../../services/user.service';
import { TaskService } from './../../services/task.service';
import { Component, OnInit } from '@angular/core';

import TaskModel from 'src/app/models/task.model';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  constructor(private userService: UserService,
              private taskService: TaskService) { }

  public title: string;
  public date: Date;
  public type: number;
  public filterActived = 'all';
  public filterAll = true;
  public filterToday = false;
  public filterWeek = false;
  public filterMonth = false;
  public filterYear = false;

  public cpf: number;
  public id: string;

  public taskModel: Array<TaskModel>;

  ngOnInit(): void {
    this.id = localStorage.getItem('id');
    console.log(this.id);
    this.exibirTasks();
  }

  async onAll() {
    this.filterAll = true;
    this.filterWeek = false;
    this.filterMonth = false;
    this.filterToday = false;
    this.filterYear = false;
    this.filterActived = 'all';
    await this.exibirTasks();
  }
  async onToday() {
    this.filterToday = true;
    this.filterAll = false;
    this.filterWeek = false;
    this.filterMonth = false;
    this.filterYear = false;
    this.filterActived = 'today';
    await this.exibirTasks();
  }
  async onWeek() {
    this.filterWeek = true;
    this.filterAll = false;
    this.filterToday = false;
    this.filterMonth = false;
    this.filterYear = false;
    this.filterActived = 'week';
    await this.exibirTasks();
  }
  async onMonth() {
    this.filterWeek = false;
    this.filterAll = false;
    this.filterToday = false;
    this.filterMonth = true;
    this.filterYear = false;
    this.filterActived = 'month';
    await this.exibirTasks();
  }
  async onYear() {
    this.filterWeek = false;
    this.filterAll = false;
    this.filterToday = false;
    this.filterMonth = false;
    this.filterYear = true;
    this.filterActived = 'year';
    await this.exibirTasks();
  }
  async onLate(event) {
    this.filterWeek = false;
    this.filterAll = false;
    this.filterToday = false;
    this.filterMonth = false;
    this.filterYear = false;
    this.filterActived = event;
    await this.exibirTasks();
  }

  async exibirTasks() {
    await this.taskService.exibiirTasks(this.filterActived, this.id)
      .subscribe(task => {
        this.taskModel = task;
      });
  }
}
