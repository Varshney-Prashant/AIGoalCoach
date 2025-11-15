import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { GoalDetail } from "../models/goal-detail.model";
import { Goal } from "../models/goal.model";
import { RefineGoalRequest } from "../models/refine-goal-request.model";
import { RefinedGoalResponse } from "../models/refined-goal-response.model";

@Injectable({ providedIn: 'root' })
export class GoalService {
  constructor(private http: HttpClient) {}

  private baseUrl= "https://localhost:7233/api/goals";

  getMyGoals(): Observable<Goal[]> {
    return this.http.get<Goal[]>(`${this.baseUrl}/my`);
  }

  getGoalDetailById(goalId: string): Observable<GoalDetail> {
    return this.http.get<GoalDetail>(`${this.baseUrl}/detail/${goalId}`);
  }

  saveGoalAndTasks(goalDetail: GoalDetail): Observable<GoalDetail> {
    return this.http.post<GoalDetail>(`${this.baseUrl}/save`, goalDetail);
  }

  markGoalTaskAsCompleted(goalTaskId: string): Observable<boolean> {
    return this.http.post<boolean>(`${this.baseUrl}/completeTask`, {goalTaskId});
  }

  getRefinedGoalResponse(refineGoalRequest: RefineGoalRequest): Observable<RefinedGoalResponse> {
    return this.http.post<RefinedGoalResponse>(`${this.baseUrl}/refineGoal`, refineGoalRequest);
  }
}