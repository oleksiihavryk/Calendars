import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { AuthorizeService } from 'src/app/authentication/services/authorize.service';
import { ModalService } from 'src/app/shared/services/modal.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent {
  public alreadyLoggedInModalId: string = 'AlreadyLoggedInModalId';

  constructor(
    private modal: ModalService,
    private auth: AuthorizeService,
    private router: Router) { }

  public start() {
    if (this.auth.isLogged) 
        this.modal.toggleModal(this.alreadyLoggedInModalId);
    else this.router.navigateByUrl('/login');

    return false;
  }
}
