import { makeObservable, observable } from 'mobx';

export class UserStore {
  constructor() {
    makeObservable(this);
    
    this.userID = null;
    this.userRole = null;
  }
}

const userStore = new UserStore();

export default userStore;