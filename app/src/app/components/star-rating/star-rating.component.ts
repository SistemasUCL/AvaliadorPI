import { Component, Input, EventEmitter, Output } from '@angular/core';
import { ControlValueAccessor } from '@angular/forms';

import { AvaliacaoCriterioModel } from '../../shared/models/avaliacao-criterio.model';

@Component({
  selector: 'app-star-rating',
  templateUrl: './star-rating.component.html',
  styleUrls: ['./star-rating.component.scss']
})
export class StarRatingComponent implements ControlValueAccessor {
  onTouched: any;
  onChange: any;

  @Input() avaliacao = {} as AvaliacaoCriterioModel;

  starList: boolean[] = [false, false, false, false, false];

  constructor() {}

  writeValue(): void {}

  registerOnChange(fn: any): void {
    this.onChange = fn;
  }

  registerOnTouched(fn: any): void {
    this.onTouched = fn;
  }

  public setStar(data: number) {
    this.avaliacao.nota = data;
    data--;
    for (let i = 0; i <= 4; i++) {
      this.starList[i] = i <= data;
    }
  }
}
