import { Component, OnInit, Input } from '@angular/core';
// tslint:disable-next-line:import-blacklist
import { Subject } from 'rxjs';
import { BsModalRef } from 'ngx-bootstrap';
import { ResponseModalModel } from '../../models/response-modal.model';

@Component({ template: '' })
export class ModalBaseComponent implements OnInit {
  public params: any;
  public active = false;
  public onClose: Subject<any>;
  public disabled = false;
  public model = {};

  constructor(public _bsModalRef: BsModalRef) {}

  ngOnInit() {
    this.onClose = new Subject<any>();
  }

  public showModal(): void {
    this.active = true;
  }

  public onConfirm(): void {
    this.return(true, this.model);
  }

  public onCancel(): void {
    this.return(false);
  }

  public hideModal(): void {
    this.return(false);
  }

  public return(ok: boolean, value?: any) {
    const responseModalModel = {
      isOk: ok,
      data: value
    } as ResponseModalModel;

    this.onClose.next(responseModalModel);
    this.active = false;
    this._bsModalRef.hide();
  }
}
