import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { UsersRoutingModule } from './users-routing.module';
import { SharedModule } from 'src/app/shared/shared.module';
import { UiModule } from "../../shared/ui/ui.module";
import { UsersComponent } from './components/users/users.component';
import { UsersService } from './users.service';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { AddUserComponent } from './components/add-user/add-user.component';
import { NgbAlertModule } from '@ng-bootstrap/ng-bootstrap';
import { TableLoaderModule } from '../components/table-loader/table-loader.module';

@NgModule({
    declarations: [UsersComponent, AddUserComponent],
    exports: [
        UsersComponent,
        AddUserComponent
    ],
    providers: [
        UsersService
    ],
    imports: [
        CommonModule,
        UsersRoutingModule,
        FormsModule,
        ReactiveFormsModule,
        SharedModule,
        NgbAlertModule,
        TableLoaderModule
    ]
})
export class UsersModule { }
