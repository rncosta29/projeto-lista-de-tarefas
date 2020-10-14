import { Router } from '@angular/router';
import { TaskService } from './../../services/task.service';
import { HttpClient } from '@angular/common/http';
import { Component, Input, OnInit, Output, EventEmitter } from '@angular/core';
import TaskModel from 'src/app/models/task.model';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent implements OnInit {

  public logo = '../../../assets/logo.png';
  public bell = '../../../assets/bell.png';

  public id: string;

  public filterWeek = false;
  public filterAll = true;
  public filterToday = false;
  public filterMonth = false;
  public filterYear = false;
  @Output() public filterActived = new EventEmitter();

  public taskModel: Array<TaskModel>;
  public late: number;

  constructor(
    private router: Router,
    private taskService: TaskService
  ) { }

  ngOnInit(): void {
    this.id = localStorage.getItem('id');
    this.onLate();
  }

  async exibirAtrasados() {
    this.filterActived.emit('late');
  }

  navegacao(rota: string) {
    this.router.navigate([rota]);
  }

  async onLate() {
    await this.taskService.exibiirTasks('late', this.id)
      .subscribe(response => {
        this.late = response.length;
      });
  }

}
