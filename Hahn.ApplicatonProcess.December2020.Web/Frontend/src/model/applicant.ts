export class Applicant{
    id: number;
    name: string;
    familyName: string;
    address:string;
    age: number;
    hired: boolean;
    countryOfOrigin: string;
    email: string;
    constructor(){
        this.hired = false;
    }
}
