import { Component, Inject, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { Job } from 'src/app/models/job/job.model';
import { DialogService } from 'src/app/services/dialog.service';
import { JobService } from 'src/app/services/model-services/job.service';
import { DialogComponent } from '../dialog.component';

@Component({
  selector: 'app-job-detail-dialog',
  templateUrl: './job-detail-dialog.component.html',
  styleUrls: ['./job-detail-dialog.component.scss']
})
export class JobDetailDialogComponent extends DialogComponent implements OnInit {

  constructor(
    override dialogRef: MatDialogRef<JobDetailDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public job: Job,
    private readonly jobService: JobService,
    private readonly dialog: DialogService
  ) { 
    super();
  }

  ngOnInit(): void {
  }
}
