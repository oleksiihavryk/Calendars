import { Component } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ModalService } from 'src/app/shared/services/modal.service';
import { UserService } from '../services/user.service';
import { Router } from '@angular/router';
import { User } from 'src/app/shared/domain/user';
import { AuthorizeService } from 'src/app/authentication/services/authorize.service';

@Component({
  selector: 'app-password',
  templateUrl: './password.component.html',
  styleUrls: ['./password.component.css']
})
export class PasswordComponent {
  public changePasswordErrorModalId: string = 'ChangePasswordErrorModalId';
  
  public isChangePasswordPanelOpen: boolean = false;
  public errorMessages: string[] = [];

  public oldPassword: FormControl = new FormControl('', [
    Validators.required
  ]);
  public newPassword: FormControl = new FormControl('', [
    Validators.required
  ]);
  public form: FormGroup = new FormGroup({
    oldPass: this.oldPassword,
    newPass: this.newPassword
  });

  constructor(
    private modal: ModalService,
    private userService: UserService,
    private router: Router,
    private authorize: AuthorizeService) { }

  public toggleChangePasswordPanel(panel: HTMLElement) {
    this.isChangePasswordPanelOpen = !this.isChangePasswordPanelOpen;
    
    $(panel).stop().slideToggle(300);
  }
  public changePasswords(panel: HTMLElement): void {
    if (this.form.valid === false) 
      throw new Error('Form cannot be invalid on this stage of password changing.');

    const {id, username, email} = this.authorize.userData;

    if (id === undefined || username === undefined)
      throw new Error('Incorrect userdata! User id and username cannot be undefined on this stage of password changing.')

    var user = new User(id, username, email, this.oldPassword.value, this.newPassword.value);
    const obs = this.userService.update(user);
    obs.subscribe({
      next: () => {
        this.router.navigateByUrl('/logout');
      },
      error: (err) => {
        this.errorMessages = err.error.messages;
        this.modal.toggleModal(this.changePasswordErrorModalId);
        this.reinitializeForm();
        this.toggleChangePasswordPanel(panel);
      },
    });
  }

  private reinitializeForm():void {
    this.oldPassword = new FormControl('', [
      Validators.required
    ]);
    this.newPassword = new FormControl('', [
      Validators.required
    ]);
    this.form = new FormGroup({
      oldPass: this.oldPassword,
      newPass: this.newPassword
    });
  }
}
