import { I18N } from "aurelia-i18n";
import { autoinject } from "aurelia-framework";

@autoinject()
export class Home {
  constructor(private readonly i18n: I18N,) {

  }
  public useLanguageChange(language: string) {
    this.i18n.setLocale(language).then(() => {
      console.log("language changed");
    });
  }

}
