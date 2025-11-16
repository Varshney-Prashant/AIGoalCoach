import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { GoalService } from '../services/goal-service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-goal-detail',
  templateUrl: './goal-detail.component.html',
  styleUrls: ['./goal-detail.component.scss']
})

export class GoalDetailComponent implements OnInit {
  goal: any;
  isDataLoaded = false;

  constructor(
    private route: ActivatedRoute,
    private goalService: GoalService,
    private toastr: ToastrService
  ) {}

  ngOnInit() {
    const goalId = this.route.snapshot.paramMap.get('id');
    if (goalId) this.loadGoal(goalId);
  }

  loadGoal(id: string) {
    this.goalService.getGoalDetailById(id).subscribe(res => {
      this.goal = res;
      this.isDataLoaded = true;
    });
  }

  toggleTask(id: string, markAsComplete: boolean) {
    const completeGoalTaskRequest = { goalTaskId: id, markAsComplete: !markAsComplete };
    this.goalService.markGoalTaskAsCompleted(completeGoalTaskRequest).subscribe({
      next: (result) => {
        if (result) {
          this.loadGoal(this.goal.id);
          this.toastr.success('Goal Task was Updated', 'Success');
        } else {
          this.toastr.error('Error updating the task', 'Error');
        }
      },
      error: () => this.toastr.error('Error updating the task', 'Error')
    });
  }
}
