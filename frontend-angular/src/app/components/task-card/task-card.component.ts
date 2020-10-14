import { Router } from '@angular/router';
import { Component, OnInit, Input } from '@angular/core';
import { DatePipe } from '@angular/common';
import typeIcons from '../../utils/typeIcons';

@Component({
  selector: 'app-task-card',
  templateUrl: './task-card.component.html',
  styleUrls: ['./task-card.component.css'],
  providers: [DatePipe]
})
export class TaskCardComponent implements OnInit {

  @Input() public title;
  @Input() public date: Date;
  @Input() public type: number;
  @Input() public id: string;
  public data: string;
  public dataAtual = new Date();
  public hora: string;
  public tipo = typeIcons;

  constructor(
    private pipe: DatePipe,
    private router: Router
  ) { }

  ngOnInit(): void {
    this.data = this.pipe.transform(this.date, 'dd/MM/yyyy');
    this.hora = this.pipe.transform(this.date, 'HH:mm');
  }

  navigateToDetails(id: string) {
    this.router.navigate([`task/${id}`]);
  }

}
