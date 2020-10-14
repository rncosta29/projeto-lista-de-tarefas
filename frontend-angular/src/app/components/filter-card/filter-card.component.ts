import { Component, OnInit, Output, Input } from '@angular/core';

@Component({
  selector: 'app-filter-card',
  templateUrl: './filter-card.component.html',
  styleUrls: ['./filter-card.component.css']
})
export class FilterCardComponent implements OnInit {

  public filter = '../../../assets/filter.png';
  public test: string;

  @Input() title;
  @Input() filterActived;

  @Input() myFilter: boolean;

  constructor() { }

  bora() {
    console.log(this.myFilter);
  }

  ngOnInit(): void {
  }
}
