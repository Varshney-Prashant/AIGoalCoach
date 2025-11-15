import { Component } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { GoalService } from '../services/goal-service';

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
    private toastrService: ToastrService
  ) {}

  ngOnInit() {
    this.loadGoals();
  }

  loadGoals() {
    this.goalService.getMyGoals().subscribe((res: any) => {
      this.goals = res.map((g: any) => ({ ...g, expanded: false }));
      this.loading = false;
    });
  }

  toggleGoal(goal: any) {
    goal.expanded = !goal.expanded;

    if (goal.expanded && !goal.tasks) {
      goal.loading = true;
      this.goalService.getGoalDetailById(goal.id).subscribe(res => {
        goal.tasks = res.goalTasks;
        goal.loading = false;
      });
    }
  }

  toggleTask(id: string) {
    this.goalService.markGoalTaskAsCompleted(id).subscribe({
      next: (result) => {
        if (result){
          this.toastrService.success('Task was completed', 'Success');
        }
        else {
          this.toastrService.error('There was an error in completeing the task', 'Error');
        }
      },
      error: () => this.toastrService.error('There was an error in completeing the task', 'Error')
    });
  }

  openModal() {
    this.showModal = true;
  }

  closeModal(saved: boolean) {
    this.showModal = false;
    if (saved) this.loadGoals();
  }
}
