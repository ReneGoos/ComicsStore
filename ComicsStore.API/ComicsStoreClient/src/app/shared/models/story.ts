import { IBaseRecord } from './baseRecord';

export interface IStory extends IBaseRecord {
  storyType: string;
  storyNumber: number;
  pages: number;
  extraInfo: string;

}
