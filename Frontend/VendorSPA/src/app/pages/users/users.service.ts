import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { UsersParams } from './users.model';

@Injectable({
  providedIn: 'root'
})
export class UsersService {

  baseUrl = environment.apiUrl;
  constructor(private http: HttpClient) { }

  getAll(parms: UsersParams): Observable<any[]> {
    let params = new HttpParams();
    params = params.append('pageNumber', parms.pageNumber.toString());
    params = params.append('pageSize', parms.pageSize.toString());
    return this.http.get<any[]>(this.baseUrl + 'users/GetUsers', {
      observe: 'body',
      params,
    });
  }


  addNewFile(fileData: any) {
    const formData = new FormData();
    Object.keys(fileData).forEach((key) => {
      formData.append(key, fileData[key]);
    });
    return this.http.post<any>(this.baseUrl + 'invoice', formData);
  }

  updateInvoice(id: any, fileData: any) {
    const formData = new FormData();
    Object.keys(fileData).forEach((key) => {
      formData.append(key, fileData[key]);
    });
    return this.http.put<any>(this.baseUrl + 'invoice/update/' + id, formData);
  }


  createUser(user: any) {
    return this.http.post<any>(this.baseUrl + 'users/create', user);
  }

  updateUser(id: string, user: any) {
    return this.http.put<any>(this.baseUrl + 'users/Update/' + id, user);
  }

  getUser(id: any) {
    return this.http.get<any>(this.baseUrl + 'users/GetUser/' + id);
  }


  deleteInvoice(id: any) {
    return this.http.delete(this.baseUrl + 'invoice/delete/' + id);
  }


}
