import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Department } from 'src/app/models/department/department.model';
import { CreateDepartmentReq } from 'src/app/models/department/dtos/create-department-req';
import { GetDepartmentResp } from 'src/app/models/department/dtos/get-department-resp';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class DepartmentService {
  private readonly departmentApi: string;

  constructor(
    private readonly httpClient: HttpClient
  ) { 
    this.departmentApi =  new URL('/api/department', environment.apiUrl).href;
  }

  createDepartment(department: CreateDepartmentReq) {
    return this.httpClient.post(this.departmentApi, department);
  }

  getDepartmentById(id: number) {
    return this.httpClient.get<Department>(this.departmentApi + '/department', {
      params: {departmentId: id}
    });
  }

  getDepartments() {
    return this.httpClient.get<GetDepartmentResp>(this.departmentApi);
  }

  updateDepartment(department: Department) {
    return this.httpClient.put(this.departmentApi, department);
  }

  deleteDepartment(id: number) {
    return this.httpClient.delete(this.departmentApi, {
      params: {departmentId: id}
    })
  }
}
