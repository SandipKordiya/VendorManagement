import { Component, OnInit } from '@angular/core';
import { UsersParams } from '../../users.model';
import { UsersService } from '../../users.service';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'app-users',
  templateUrl: './users.component.html',
  styleUrls: ['./users.component.scss']
})
export class UsersComponent implements OnInit {
  breadCrumbItems: Array<{}>;
  attestators: any[];
  parms = new UsersParams();
  public user = {
    name: 'Izzat Nadiri',
    age: 26
  }
  isLoading = true;
  constructor(private service: UsersService, private modalService: NgbModal) { }

  ngOnInit() {
    this.breadCrumbItems = [
      { label: 'Vendors' },
      { label: 'List', active: true },
    ];
    this.getList();
  }

  getList() {
    this.service.getAll(this.parms).subscribe(
      (res: any) => {
        console.log(res);
        this.attestators = res;
        this.isLoading = false;
      },
      (error) => {
        // this.alertify.error(error.error.message || error.message);
        this.isLoading = false;
      }
    );
  }

  handlePageChange(e) {
    this.parms.pageNumber = e;
    this.getList();
  }

 

}
