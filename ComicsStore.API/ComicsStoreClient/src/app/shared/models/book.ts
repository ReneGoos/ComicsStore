import { IBaseRecord } from './baseRecord';

export interface IBook extends IBaseRecord {
  'bookType': string;
  'active': string;
  'firstYear': number;
  'thisYear': number;
  'firstPrint': string;
}
