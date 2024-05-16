import {Role} from "./role.model";

export interface User {
  sub: string; 
  name: string;
  email: string;
  role: string[];
}
