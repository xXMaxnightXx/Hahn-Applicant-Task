import { HttpClient } from "aurelia-http-client";
import { inject } from "aurelia-framework";
import api from '../config/api';
import { Applicant } from '../model/applicant';

@inject(HttpClient)
export class ApplicantApi {
  private http: HttpClient;

  constructor(http: HttpClient) {
    http.configure(x => x.withBaseUrl(api.dev + '/applicant/'));
    this.http = http;
  }
  
  
  public addApplicant(applicant: Applicant): Promise<any> {
    var promise = new Promise((resolve, reject) => {
      this.http
        .post('', applicant)
        .then(data => {          
          resolve(data);
        }).catch(err => reject(err));
    });
    return promise;
  }

  public getApplicants() : Promise<any>{
    let promise = new Promise((resolve, reject) => {
      this.http
        .get('')
        .then(data => {         
          resolve(data)
        }).catch(err => reject(err));
    });
    return promise;
  }
}
