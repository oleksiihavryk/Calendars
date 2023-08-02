import { Component, Input } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { AuthorizeService } from 'src/app/authentication/services/authorize.service';
import { ModalService } from 'src/app/shared/services/modal.service';
import { UserService } from '../services/user.service';
import { Router } from '@angular/router';
import { User } from 'src/app/shared/domain/user';

@Component({
  selector: 'app-email',
  templateUrl: './email.component.html',
  styleUrls: ['./email.component.css']
})
export class EmailComponent {
  @Input() email: string | undefined;

  public changeEmailErrorModalId: string = 'ChangeEmailErrorModalId';
  public deleteEmailModalId: string = 'DeleteEmailModalId';
  
  public errorMessages: string[] = [];
  public isChangesOpen: boolean = false;
  public doneButtonDisabled: boolean = false;

  public input: FormControl = new FormControl(this.authorize.userData.email, [
    Validators.required,
    Validators.email
  ]);
  public form: FormGroup = new FormGroup({
    username: this.input
  });

  constructor(
    public authorize: AuthorizeService,
    private modal: ModalService,
    private userService: UserService,
    private router: Router) {}

  public invokeRemoveEmailModal() {
    this.modal.toggleModal(this.deleteEmailModalId);
  }
  public openChanges() {
    this.isChangesOpen = true;
  }
  public closeChanges() {
    this.isChangesOpen = false;
  }
  public createRemoveEmailHandler() {
    return () => {
      this.removeEmail();
    }
  }
  public removeEmail() {
    if (this.authorize.userData.email !== undefined) {
      this.doneButtonDisabled = true;
        
      const user = new User(
        this.authorize.userData.id ?? '', 
        this.authorize.userData.username ?? '',
        undefined);
          
      this.updateUser(user);
    }
  }
  public change() {
    if (this.form.valid === false) {
      throw new Error('You cannot change the email if this email isnt valid.')
    }

    this.doneButtonDisabled = true;

    const user = new User(
      this.authorize.userData.id ?? '', 
      this.authorize.userData.username ?? '',
      this.input.value);

    this.updateUser(user);
  }
  public isEmailAndInputHasDifferentValues(): boolean {
    return ((this.input.value !== null && this.input.value !== '') || 
      this.authorize.userData.email !== undefined) && 
      this.input.value !== this.authorize.userData.email
  }

  private updateUser(user: User) : void {
    const obs = this.userService.update(user);
    obs.subscribe({
      next: (response) => {
        this.closeChanges();
        this.router.navigateByUrl('/logout');
      },
      error: (err) => {
        this.errorMessages = err.error.messages;
        this.modal.toggleModal(this.changeEmailErrorModalId);
        this.input.setValue(this.authorize.userData.email);
        this.doneButtonDisabled = false;
        this.closeChanges();
      }
    });
  }
}
