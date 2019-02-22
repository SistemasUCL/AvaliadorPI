import {
  Component,
  OnChanges,
  Input,
  SimpleChanges,
  OnInit,
  Output,
  EventEmitter
} from '@angular/core';

@Component({
  selector: 'app-card',
  templateUrl: './card.component.html',
  styleUrls: ['./card.component.scss']
})
export class CardComponent implements OnInit {
  @Input() avaliacao: any;
  @Input() index: 0;

  @Output() setRating = new EventEmitter<any>();

  constructor() {}

  ngOnInit(): void {}

  // ngOnChanges(changes: SimpleChanges) {
  //   this.avaliacao = changes.avaliacao
  //     ? changes.avaliacao.currentValue
  //     : changes.avaliacao;
  //   this.index = changes.index.currentValue;
  // }

  rating(avaliacao: any) {
    this.setRating.emit(avaliacao);
  }
}
