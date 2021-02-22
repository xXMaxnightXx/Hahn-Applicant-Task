import { DialogService } from "aurelia-dialog";
import { autoinject, bindable } from "aurelia-framework";
import { I18N } from "aurelia-i18n";
import { Prompt } from '../shared/prompt/prompt';



import {
  ValidationRules,
  ValidationController,
  validateTrigger,
  ValidationMessageProvider,
} from "aurelia-validation";
import { Applicant } from "../model/applicant";
import { BootstrapFormRenderer } from "../validation/bootstrap-form-renderer";
import { ApplicantValidation } from "validation/applicantValidation";
import { ApplicantApi } from "api/applicantApi";
import { privateDecrypt } from "crypto";

@autoinject()
export class ApplicantCreate {
  public heading = this.i18n.tr("Application.AppHeader");
  public applicant: Applicant;
  
  public isValid: boolean = true;
  public buttonSave: string;
  public buttonReset: string;

  constructor(
    private readonly i18n: I18N,
    private readonly validation: ValidationController,
    private readonly dialog: DialogService,
    private readonly applicantApi: ApplicantApi) {
    this.validation.validateTrigger = validateTrigger.change;
    this.validation.addRenderer(new BootstrapFormRenderer());

    
    this.applicant = new Applicant();
    ApplicantValidation.setAddApplicantValidationRule(this.applicant);
    this.buttonReset = this.i18n.tr("Button.Reset");
    this.buttonSave = this.i18n.tr("Button.Save");
  }



  public validateAndSubmit() {
    this.validation.validate().then((result) => {
      this.applicantApi.addApplicant(this.applicant).then(result=>{
        this.dialog.open({ viewModel: Prompt, model: { title: this.i18n.tr("Application.ApplicantTitle"), message: this.i18n.tr("Application.ApplicantCreated"), hideCloseButton: true }, lock: true }).then(result => {
          this.resetFields();          
        });
        

      }).catch((err) => {        
        var messages = JSON.parse(err.response).data === undefined ? [JSON.parse(err.response)] : JSON.parse(err.response).data.validationMessage;
        var message = "";
        messages.map((msg, index)=>{          
          message = message + "<li>" + msg + " </ li>";
        })
        message = message + "</ul>";        
        this.dialog.open({ viewModel: Prompt, model: { title: this.i18n.tr("Application.ValidationTitle"), message: message, hideCloseButton: true }, lock: true })
      })
    });
  }


  get canReset(): boolean {
    if (
      this.applicant.id ||
      this.applicant.name ||
      this.applicant.familyName ||
      this.applicant.address ||
      this.applicant.countryOfOrigin ||
      this.applicant.email ||
      this.applicant.age ||
      this.applicant.hired == true) {
      return true;
    }
    return false;
  }


  get canSend(): boolean {

    this.validation.validate().then((result) => {
      this.isValid = result.valid;
    })
    
    return this.isValid;

  }


  public reset(): void {

    this.dialog.open({ viewModel: Prompt, model: { title: this.i18n.tr("Application.ConfirmationTitle"), message: this.i18n.tr("Application.ResetConfirmationMessage") }, lock: true }).whenClosed(response => {
      if (!response.wasCancelled) {
        this.resetFields();
      }
    });
  }

  private resetFields(): void
  {
    this.applicant.id = undefined;
    this.applicant.name = "";
    this.applicant.address = "";
    this.applicant.countryOfOrigin = "";
    this.applicant.email = "";
    this.applicant.familyName = "";
    this.applicant.age = undefined;
    this.applicant.hired = false;
    this.validation.reset();
  }
}
