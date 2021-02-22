import { ValidationRules } from "aurelia-validation";
import { Applicant } from "model/applicant";

export class ApplicantValidation{
    public static setAddApplicantValidationRule(applicant: Applicant): void {
        ValidationRules.ensure((u: Applicant) => u.id)
          .required()
          .withMessageKey("IdRequired")
          .ensure((u: Applicant) => u.email)
          .required()
          .withMessageKey("EmailRequired")
          .then()
          .email()
          .withMessageKey("EmailFormat")
          .ensure((u: Applicant) => u.age)
          .required()
          .withMessageKey("AgeRequired")
          .then()
          .between(20, 60)
          .withMessageKey("AgeRange")
    
          .on(applicant);
      }
}