import { Component, EventEmitter, Input, OnChanges, Output } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { GoalDetail } from '../models/goal-detail.model';
import { GoalTask } from '../models/goal-task.model';
import { RefineGoalRequest } from '../models/refine-goal-request.model';
import { GoalService } from '../services/goal-service';

@Component({
  selector: 'app-refine-goal-dailog',
  templateUrl: './refine-goal-dailog.component.html',
  styleUrls: ['./refine-goal-dailog.component.scss']
})
export class RefineGoalDailogComponent implements OnChanges {
  @Input() show = false;
  @Output() close = new EventEmitter<boolean>();

  rawInput = '';
  actionableGoal = '';
  tasks: string[] = [];
  refined = false;

  constructor(
    private goalService: GoalService,
    private toastrService: ToastrService
  ){

  }

  ngOnChanges() {
    if (this.show) {
      document.body.classList.add('modal-open');
    } else {
      document.body.classList.remove('modal-open');
    }
  }  

  refineGoal(){
    var refineGoalRequest = new RefineGoalRequest({
      rawGoal: this.rawInput
    })
    this.goalService.getRefinedGoalResponse(refineGoalRequest).subscribe({
      next: (result) => {
        if (result.isGoalRefined){
          this.actionableGoal = result.actionableGoal;
          this.tasks = result.goalTasks;
          this.refined = true;
        }
        else{
          this.toastrService.warning(`${result.message}`, 'Warning');
        }
      },
      error: () => this.toastrService.error('An error ocurred', 'Error')
    })
  }

  saveGoalAndTasks(){
    let goalTasks = new Array<GoalTask>();
    this.tasks.forEach(task => {
      let goalTask = new GoalTask({
        description: task,
        isCompleted: false
      });

      goalTasks.push(goalTask);
    });

    const goalDetail = new GoalDetail({
      actionableGoal: this.actionableGoal,
      rawInput: this.rawInput,
      goalTasks: goalTasks
    });

    this.goalService.saveGoalAndTasks(goalDetail).subscribe(data => {
      if(data.id){
        this.toastrService.success('Goal and Task created successfully', 'Success');
      }
      else{
        this.toastrService.error('An error ocurred while creating goal or tasks', 'Error');
      }
    })
  }

  closeModal(){
    this.show = false;
    this.refined = false;
    this.rawInput = "";
    this.actionableGoal = "";
    this.tasks = [];
    this.close.emit(false);
  }
}
