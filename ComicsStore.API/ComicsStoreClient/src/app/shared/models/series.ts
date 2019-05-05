import { IBaseRecord } from './baseRecord';

export interface ISeries extends IBaseRecord {
  seriesNumber: number;
  seriesLanguage: string;
}
