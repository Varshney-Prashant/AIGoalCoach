import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './auth/login/login.component';
import { GoalDetailComponent } from './goal/goal-details/goal-detail.component';
import { MyGoalComponent } from './goal/my-goal/my-goal.component';

const routes: Routes = [
  { path: "", redirectTo: "login", pathMatch: "full" },
  { path: "login", component: LoginComponent },
  { path: "my-goals", component: MyGoalComponent },
  { path: "goal/:id", component: GoalDetailComponent },
];


@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
