import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { CreateJobReq } from 'src/app/models/job/dtos/create-job-req';
import { GetJobResp } from 'src/app/models/job/dtos/get-job-resp';
import { Job } from 'src/app/models/job/job.model';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class JobService {
  private readonly jobApi: string;

  constructor(
    private readonly httpClient: HttpClient
  ) { 
    this.jobApi =  new URL('/api/job', environment.apiUrl).href;
  }

  createJob(job: CreateJobReq) {
    return this.httpClient.post(this.jobApi, job);
  }

  getJobById(id: number) {
    return this.httpClient.get<Job>(this.jobApi, {
      params: {jobId: id}
    });
  }

  getJobs() {
    return this.httpClient.get<GetJobResp>(this.jobApi);
  }

  updateJob(job: Job) {
    return this.httpClient.put(this.jobApi, job);
  }

  deleteJob(id: number) {
    return this.httpClient.delete(this.jobApi, {
      params: {jobId: id}
    })
  }
}
