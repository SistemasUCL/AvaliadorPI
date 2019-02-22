import { Component, OnInit } from '@angular/core';
import { ModalBaseComponent } from '../../../core/components/_base/modal-base.component';
import { BsModalRef } from 'ngx-bootstrap';

@Component({
  selector: 'app-modal-falta',
  templateUrl: './modal-falta.component.html',
  styleUrls: ['./modal-falta.component.scss']
})
export class ModalFaltaComponent extends ModalBaseComponent implements OnInit {
  constructor(public _bsModalRef: BsModalRef) {
    super(_bsModalRef);
  }

  ngOnInit() {
    super.ngOnInit();
  }

  public showModal() {
    super.showModal();
  }

  public onConfirm(): void {
    this.return(true, this.params.ausentes);
  }
}
