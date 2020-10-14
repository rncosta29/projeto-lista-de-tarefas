export default class TaskModel {
  public id: string;
  public type: number;
  public title: string;
  public description: string;
  public when: string;
  public done: boolean;
  public created: Date;
  public userId: string;
}
