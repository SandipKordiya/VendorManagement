import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';

@Component({
  selector: 'app-pagination',
  templateUrl: './pagination.component.html',
  styleUrls: ['./pagination.component.scss'],
})
export class PaginationComponent implements OnInit {
  @Input() totalCount;
  @Input() pageIndex;
  @Input() pageSize;
  @Output() onChangeHandler: EventEmitter<any> = new EventEmitter();
  constructor() {}

  ngOnInit() {}
  handlePageChange(e) {
    this.onChangeHandler.emit(e);
  }
}
