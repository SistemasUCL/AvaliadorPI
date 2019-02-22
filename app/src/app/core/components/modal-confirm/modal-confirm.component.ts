import { Component, OnInit } from '@angular/core';
// tslint:disable-next-line:import-blacklist
import { Subject } from 'rxjs';
import { BsModalRef } from 'ngx-bootstrap';
import { ModalBaseComponent } from '../_base/modal-base.component';

@Component({
  selector: 'app-modal-confirm',
  templateUrl: './modal-confirm.component.html',
  styleUrls: ['./modal-confirm.component.scss']
})
export class ModalConfirmComponent extends ModalBaseComponent
  implements OnInit {
  constructor(public _bsModalRef: BsModalRef) {
    super(_bsModalRef);
  }

  ngOnInit() {
    super.ngOnInit();
  }
}
