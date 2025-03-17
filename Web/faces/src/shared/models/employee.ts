import { JobFunction } from "./job-function";
import { Phone } from "./phone";

export interface Employee {

    id: number;

    firstName: string;

    lastName: string;

    fullName: string;

    email: string;

    docNumber: string;

    birthDate: Date;

    age: number;

    isAgeOfMajority: boolean;

    jobFunction: JobFunction;

    manager: Employee | null;

    phones: Phone[];

    password?:string;
}

