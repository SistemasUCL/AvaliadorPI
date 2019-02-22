import {
  Component,
  Input,
  OnChanges,
  SimpleChanges,
  EventEmitter,
  Output
} from '@angular/core';

@Component({
  selector: 'app-star-rating',
  templateUrl: './star-rating.component.html',
  styleUrls: ['./star-rating.component.scss']
})
export class StarRatingComponent implements OnChanges {
  @Input() avaliacao = {};

  @Output() setRating = new EventEmitter<any>();

  starList: boolean[] = [false, false, false, false, false];

  constructor() {}

  ngOnChanges(changes: SimpleChanges): void {
    this.avaliacao = changes.avaliacao.currentValue;
    this.setStar(this.avaliacao['nota']);
  }

  public setStar(data: number) {
    this.avaliacao['nota'] = data;
    data--;
    for (let i = 0; i <= 4; i++) {
      this.starList[i] = i <= data;
    }

    this.setRating.emit(this.avaliacao);
  }
}
