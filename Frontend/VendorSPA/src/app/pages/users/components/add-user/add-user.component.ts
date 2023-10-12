import { AfterViewInit, Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { first } from 'rxjs';
import { AuthenticationService } from 'src/app/core/services/auth.service';
import { UserProfileService } from 'src/app/core/services/user.service';
import { UsersService } from '../../users.service';

@Component({
  selector: 'app-add-user',
  templateUrl: './add-user.component.html',
  styleUrls: ['./add-user.component.scss']
})
export class AddUserComponent implements OnInit, AfterViewInit {
  breadCrumbItems: Array<{}>;

  signupForm: FormGroup;
  submitted = false;
  error = '';
  successmsg = false;
  userId: string;
  userData: any;
  btnTitle: string = "Create User";

  // set the currenr year
  year: number = new Date().getFullYear();

  // tslint:disable-next-line: max-line-length
  constructor(private formBuilder: FormBuilder, private route: ActivatedRoute,
    private router: Router, private modalService: NgbModal,
    private _service: UsersService) {

    this.route.params.subscribe((params) => {
      this.userId = params['id'];
      console.log(params);
      if (this.userId)
        this.btnTitle = "Edit User";
      this.getUserDetails();
    });

  }

  ngOnInit() {
    this.breadCrumbItems = [
      { label: 'Vendors' },
      { label: 'Create', active: true },
    ];

    this.signupForm = this.formBuilder.group({
      firstName: ['', Validators.required],
      lastName: [''],
      mobile: [''],
      username: ['', Validators.required],
      address: [''],
      city: [''],
      state: [''],
      companyCode: [''],
      botEmail: [''],
      description: [''],
      panNumber: [''],
      email: ['', [Validators.required, Validators.email]],
      password: ['', Validators.required],
    });
  }

  ngAfterViewInit() {
  }

  getUserDetails() {
    this._service.getUser(this.userId).subscribe({
      next: (data: any) => {
        this.userData = data;

        this.signupForm.patchValue({
          firstName: this.userData.firstName,
          lastName: this.userData.lastName,
          username: this.userData.userName,
          email: this.userData.email,
          password: this.userData.password,
          mobile: this.userData.phoneNumber,
          address: this.userData.address,
          panNumber: this.userData.panNumber,
          city: this.userData.city,
          state: this.userData.state,
          description: this.userData.description,
        });
      },
      error: (err) => {
        console.log(err);
      },
    });
  }


  close() {
    this.modalService.dismissAll();
  }

  // convenience getter for easy access to form fields
  get f() { return this.signupForm.controls; }

  /**
   * On submit form
   */
  onSubmit() {
    this.submitted = true;

    // stop here if form is invalid
    if (this.signupForm.invalid) {
      return;
    } else {
      
      if (this.userId != null || this.userId != undefined) {
        console.log("enter");
        
        this._service.updateUser(this.userId, this.signupForm.value)
          .pipe(first())
          .subscribe(
            data => {
              this.successmsg = true;
              if (this.successmsg) {
                this.router.navigate(['/users/users']);
              }
            },
            error => {
              this.error = error ? error : '';
            });
      } else {
        this._service.createUser(this.signupForm.value)
          .pipe(first())
          .subscribe(
            data => {
              this.successmsg = true;
              if (this.successmsg) {
                this.router.navigate(['/users/users']);
              }
            },
            error => {
              this.error = error ? error : '';
            });
      }


    }
  }
}

