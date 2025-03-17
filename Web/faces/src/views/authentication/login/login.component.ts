import { CommonModule } from '@angular/common';
import { Component, OnInit, signal } from '@angular/core';
import { FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { InputTextModule } from 'primeng/inputtext';
import { FloatLabelModule } from 'primeng/floatlabel';
import { ButtonModule } from 'primeng/button';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome'
import { faRightToBracket } from '@fortawesome/free-solid-svg-icons'
import { AuthenticationApiService, FacesCredential } from '../../../api-services/authentication-api.service';
import { LoginService } from '../../../shared/services/login.service';
import { LoadingComponent } from '../../../shared/components/loading/loading.component';

@Component({
  selector: 'login',
  standalone: true,
  templateUrl: './login.component.html',
  styleUrl: './login.component.css',
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    InputTextModule,
    FloatLabelModule,
    ButtonModule,
    FontAwesomeModule,
    LoadingComponent
  ],
  providers:[]
})
export class LoginComponent implements OnInit{

  signInIcon = faRightToBracket;

  constructor(private loginService: LoginService, private fb: FormBuilder) { 

    this.form = this.fb.group({
      document: this.fb.control('',Validators.required),
      password: this.fb.control('', Validators.required)
    })
  

  }

  loading = signal(false);

  form!: FormGroup;

  ngOnInit(): void {
    
  }


  async login(credentials : FacesCredential) : Promise<void>{

    await this.loginService.login(credentials,this.loading);

  }
  
  
}
