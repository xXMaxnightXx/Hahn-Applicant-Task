import { I18N } from "aurelia-i18n";
import { EventAggregator } from 'aurelia-event-aggregator'
import { autoinject, bindable, computedFrom } from 'aurelia-framework'
import { ApplicantApi } from 'api/applicantApi';


@autoinject()
export class ApplicantList {

  
  applicants: [] | any;

  @bindable
  public header: string = this.i18n.tr("ApplicantList.Heading");
  public columns: any; 
  public rows: []; 


  constructor(private readonly ea: EventAggregator, private readonly applicantService: ApplicantApi, private readonly i18n: I18N) {

    
    this.applicants = [];
    
    this.rows = [];
    
    this.columns = [
      { field: 'id', header: this.i18n.tr("ApplicantList.Id"), size: "60px", sortable: true },
      { field: 'name', header: this.i18n.tr("ApplicantList.Name"), sortable: true, size: "200px" },
      { field: 'familyName', header: this.i18n.tr("ApplicantList.FamilyName") },
      { field: 'age', header: this.i18n.tr("ApplicantList.Age"), size: "60px" },
      { field: 'email', header: this.i18n.tr("ApplicantList.Email"), size: "200px"},
      { field: 'countryOfOrigin', header: this.i18n.tr("ApplicantList.Country"), class: "text-center" }];

  
    this.created();
  }
  public refresh(): void {    
    this.applicantService.getApplicants()
      .then(data => {
        this.rows = JSON.parse(data.response).data.applicants;
        console.log(this.rows);
      })
      .catch(err => console.log(err));
  }
  created() {
    this.applicantService.getApplicants()
      .then(data => {        
        this.rows = JSON.parse(data.response).data.applicants;
        console.log(this.rows);
      })
      .catch(err => console.log(err));
  }

  
}
