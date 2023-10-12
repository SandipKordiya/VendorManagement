import { Component, OnInit, TemplateRef } from '@angular/core';
import { Observable } from 'rxjs';
import { VendorService } from '../../vendor.service';
import { ActivatedRoute } from '@angular/router';
import { environment } from 'src/environments/environment';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { UsersService } from 'src/app/pages/users/users.service';

@Component({
  selector: 'app-vendor-files',
  templateUrl: './vendor-files.component.html',
  styleUrls: ['./vendor-files.component.scss']
})
export class VendorFilesComponent implements OnInit {

  // breadcrumb items
  breadCrumbItems: Array<{}>;
  userId: string;
  userName: string;
  // Table data
  ordersData: any[];

  invoiceFile?: File;
  submitted = false;

  filePath: string = environment.apiUrl + `invoice/download/`

  tables$: Observable<any[]>;
  total$: Observable<number>;

  vendorFileForm: FormGroup;
  isLoading = true;
  isEdit = false;
  invoiceId: number = 0;
  constructor(public service: VendorService,
    private formBuilder: FormBuilder, private _service: UsersService,
    private Route: ActivatedRoute, private modalService: NgbModal) {
    this.Route.params.subscribe((params) => {
      this.userId = params['userId'];
      this.userName = params['vendorName'];
    });


  }

  selectinvoiceFile(event: any): void {
    this.invoiceFile = event.target.files[0];
    // let reader = new FileReader();
    // reader.readAsDataURL(event.target.files[0]);
  }

  // convenience getter for easy access to form fields
  get f() { return this.vendorFileForm.controls; }

  ngOnInit(): void {
    this.breadCrumbItems = [{ label: 'Invoices' }, { label: 'list', active: true }];


    this.vendorFileForm = this.formBuilder.group({
      poNumber: ['', Validators.required],
      invoiceDate: ['', [Validators.required]],
      invoiceNumber: ['', Validators.required],
      isSentEmail: [false, Validators.required],
    });

    /**
     * fetch data
     */
    this.getList();


  }


  /**
   * fetches the table value
   */
  getList() {
    this.service.getAll(this.userId).subscribe(
      (res: any) => {
        console.log(res);
        this.isLoading = false;
        this.ordersData = res;
      },
      (error) => {
        // this.alertify.error(error.error.message || error.message);
        this.isLoading = false;
      }
    );
  }

  deleteInvoice(id: any) {
    this._service.deleteInvoice(id).subscribe(
      (res: any) => {
        console.log(res);
        this.isLoading = false;
        this.getList();
      },
      (error) => {
        // this.alertify.error(error.error.message || error.message);
        this.isLoading = false;
      }
    );
  }

  close() {
    this.modalService.dismissAll();
  }

  openModal(template: TemplateRef<any>) {
    this.modalService.open(template, { centered: true });
  }

  updateInvoice(item: any, template: TemplateRef<any>) {
    console.log(item);
    this.isEdit = true;
    this.invoiceId = item.id;
    this.vendorFileForm.patchValue({
      poNumber: item.poNumber,
      invoiceDate: item.invoiceDate,
      invoiceNumber: item.invoiceNumber,
      isSentEmail: item.isSentEmail
    });
    this.modalService.open(template, { centered: true });
  }

  /**
   * On submit form
   */
  onSubmit() {
    this.submitted = true;

    // stop here if form is invalid
    if (this.vendorFileForm.invalid) {
      return;
    } else {

      if (this.isEdit) {
        this._service.updateInvoice(this.invoiceId, {
          ...this.vendorFileForm.value,
          userId: this.userId,
          invoiceFile: this.invoiceFile
        })
          .subscribe(
            data => {
              // this.router.navigate(['/account/login']);
              this.modalService.dismissAll();
              this.vendorFileForm.reset();
              this.invoiceId = 0;
              this.getList();
            },
            error => {
              // this.error = error ? error : '';
            });
      } else {
        this._service.addNewFile({
          ...this.vendorFileForm.value,
          userId: this.userId,
          invoiceFile: this.invoiceFile
        })
          .subscribe(
            data => {
              // this.router.navigate(['/account/login']);
              this.modalService.dismissAll();
              this.vendorFileForm.reset();
              this.getList();
            },
            error => {
              // this.error = error ? error : '';
            });
      }

    }
  }
}
