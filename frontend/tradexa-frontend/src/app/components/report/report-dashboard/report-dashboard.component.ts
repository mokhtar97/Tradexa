// src/app/components/report/report-dashboard/report-dashboard.component.ts

import { Component, OnInit } from '@angular/core';
import { ReportService } from 'src/app/services/report.service';

@Component({
  selector: 'app-report-dashboard',
  templateUrl: './report-dashboard.component.html',
})
export class ReportDashboardComponent implements OnInit {
  reportData: any[] = [];

  constructor(private reportService: ReportService) {}

  ngOnInit(): void {
    this.loadReport();
  }

  loadReport(): void {
    this.reportService.getDashboardData().subscribe((data) => {
      this.reportData = data; // Ensure the format matches <app-pie-chart> input
    });
  }
}
