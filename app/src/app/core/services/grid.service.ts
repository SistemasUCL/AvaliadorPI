import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { OrderByDirectionEnum } from '../enums/order-by-direction.enum';
import { BaseModel } from '../models/base.model';
import { BaseRequestService } from './base-request.service';

@Injectable()
export class GridService extends BaseRequestService {
  constructor(protected http: HttpClient) {
    super(http);
  }

  public search(
    route: string,
    search: string,
    page: number,
    pageSize: number,
    orderBy: string,
    direction: OrderByDirectionEnum
  ): Promise<any> {
    return super
      .get(
        `${route}?search=${search}&page=${page}&pageSize=${pageSize}&orderBy=${orderBy}&direction=${direction}`
      )
      .then((data) => data);
  }
}
