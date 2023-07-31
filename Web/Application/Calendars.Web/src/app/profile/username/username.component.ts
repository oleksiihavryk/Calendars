import { Component, Input } from '@angular/core';
import { UserService } from '../services/user.service';
import { AuthorizeService } from 'src/app/authentication/services/authorize.service';
import { User } from 'src/app/shared/domain/user';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ModalService } from 'src/app/shared/services/modal.service';

@Component({
  selector: 'app-username',
  templateUrl: './username.component.html',
  styleUrls: ['./username.component.css']
})
export class UsernameComponent {
  @Input() username: string = '';

  public changeUsernameErrorModalId: string = 'ChangeUsernameErrorModalId';
  public errorMessages: string[] = [];

  public isChangesOpen: boolean = false;
  public doneButtonDisabled: boolean = false;

  public input: FormControl = new FormControl(this.authorize.userData.username, [
    Validators.required
  ]);
  public form: FormGroup = new FormGroup({
    username: this.input
  });

  constructor(
    public authorize: AuthorizeService,
    private userService: UserService,
    private router: Router,
    private modal: ModalService) {}

  public openChanges(): void {
    this.isChangesOpen = true;
  }
  public closeChanges(): void {
    this.isChangesOpen = false;
  }
  public change() {
    if (this.form.valid === false) {
      throw new Error('You cannot change the nickname if this nickname isnt valid.')
    }

    this.doneButtonDisabled = true;

    const user = new User(
      this.authorize.userData.id ?? '', 
      this.input.value,
      this.authorize.userData.email);

    const obs = this.userService.update(user);
    obs.subscribe({
      next: (response) => {
        this.closeChanges();
        this.router.navigateByUrl('/logout');
      },
      error: (err) => {
        this.errorMessages = err.error.messages;
        this.modal.toggleModal(this.changeUsernameErrorModalId);
        this.input.setValue(this.authorize.userData.username);
        this.doneButtonDisabled = false;
        this.closeChanges();
      }
    });
  }
}
