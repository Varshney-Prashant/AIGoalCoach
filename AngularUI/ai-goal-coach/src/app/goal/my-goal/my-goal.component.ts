import { Component } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { GoalService } from '../services/goal-service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-my-goal',
  templateUrl: './my-goal.component.html',
  styleUrls: ['./my-goal.component.scss']
})
export class MyGoalComponent {

  goals: any[] = [];
  loading = true;
  showModal = false;

  constructor(
    private goalService: GoalService,
    private toastrService: ToastrService,
    private router: Router
  ) {}

  ngOnInit() {
    this.loadGoals();
  }

  loadGoals() {
    this.goalService.getMyGoals().subscribe((res: any) => {
      this.goals = res;
      this.loading = false;
    });
  }

  navigateToGoal(goal: any) {
    this.router.navigate(['/goal', goal.id]);
  }

  openModal() {
    this.showModal = true;
  }

  closeModal(saved: boolean) {
    this.showModal = false;
    if (saved) this.loadGoals();
  }
}