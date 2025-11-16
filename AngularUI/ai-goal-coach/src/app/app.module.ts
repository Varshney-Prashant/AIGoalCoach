import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';
import { ToastrModule } from 'ngx-toastr';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { LoginComponent } from './auth/login/login.component';
import { GoalComponent } from './goal/goal.component';
import { MyGoalComponent } from './goal/my-goal/my-goal.component';
import { RefineGoalDailogComponent } from './goal/refine-goal-dailog/refine-goal-dailog.component';
import { TokenInterceptor } from './auth/login/interceptors/token.interceptor';
import { LoaderInterceptor } from './shared/loader.interceptor';
import { LoaderComponent } from './shared/loader.component';
import { GoalDetailComponent } from './goal/goal-details/goal-detail.component';

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    GoalComponent,
    MyGoalComponent,
    RefineGoalDailogComponent,
    LoaderComponent,
    GoalDetailComponent
  ],
  imports: [
    BrowserModule,
    FormsModule,
    AppRoutingModule,
    HttpClientModule,
    BrowserAnimationsModule,      
    ToastrModule.forRoot({
      positionClass: 'toast-bottom-right',
      preventDuplicates: true
    })
  ],
  providers: [
    {
      provide: HTTP_INTERCEPTORS,
      useClass: TokenInterceptor,
      multi: true
    },
    { provide: HTTP_INTERCEPTORS, useClass: LoaderInterceptor, multi: true }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
