import {Aurelia} from 'aurelia-framework';
import * as environment from '../config/environment.json';
import {PLATFORM} from 'aurelia-pal';
import Backend from "i18next-xhr-backend";
import { ValidationMessageProvider } from 'aurelia-validation';
import { I18N } from 'aurelia-i18n';

export function configure(aurelia: Aurelia) {
  aurelia.use
    .standardConfiguration()
    .feature(PLATFORM.moduleName('resources/index'));

  aurelia.use.developmentLogging(environment.debug ? 'debug' : 'warn');

  aurelia.use
    .standardConfiguration()
    .plugin(PLATFORM.moduleName('aurelia-validation'))
    .plugin(PLATFORM.moduleName('aurelia-table'))
    .developmentLogging();

  aurelia.use.plugin(PLATFORM.moduleName('aurelia-i18n'), (instance) => {
    // register backend plugin
    instance.i18next.use(Backend);
    return instance.setup({
      backend: {                                  // <-- configure backend settings
        loadPath: "/locales/{{lng}}/{{ns}}.json", // <-- XHR settings for where to get the files from
      },
      lng: "en",
      attributes: ["t", "i18n"],
      fallbackLng: "en",
      debug: false,
    });
  });
  aurelia.use.plugin(PLATFORM.moduleName('aurelia-validation'));
  aurelia.use.plugin(PLATFORM.moduleName('aurelia-dialog'));

  ValidationMessageProvider.prototype.getMessage = function (key) {
    const i18n = aurelia.container.get(I18N);
    const translation = i18n.tr(`errorMessages.${key}`);
    return this.parser.parse(translation);
  };
  
  if (environment.testing) {
    aurelia.use.plugin(PLATFORM.moduleName('aurelia-testing'));
  }

  aurelia.start().then(() => aurelia.setRoot(PLATFORM.moduleName('app')));
}
