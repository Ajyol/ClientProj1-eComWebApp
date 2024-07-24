export interface UserCreateDto {
    userName: string;
    firstName: string;
    lastName: string;
    email: string;
    dateOfBirth: Date;
    passwordHash: string;
  }
  
  export interface UserGetDto {
    id: number;
    userName: string;
    firstName: string;
    lastName: string;
    email: string;
    dateOfBirth: Date;
  }
  