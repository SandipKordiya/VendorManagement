import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, map } from 'rxjs';
import { environment } from 'src/environments/environment';
import { UsersParams } from '../users/users.model';

@Injectable({
  providedIn: 'root'
})
export class VendorService {

  baseUrl = environment.apiUrl;
  constructor(private http: HttpClient) { }

  getAll(id: string): Observable<any[]> {
    // let params = new HttpParams();
    // params = params.append('pageNumber', parms.pageNumber.toString());
    // params = params.append('pageSize', parms.pageSize.toString());
    return this.http.get<any[]>(this.baseUrl + 'invoice/GetInvoices/' + id);
  }

}
