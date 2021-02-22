import { DialogController } from 'aurelia-dialog';
import { autoinject } from "aurelia-framework";

@autoinject()
export class Prompt {
  public message: string;
  public title: string;
  public hideCloseButton: boolean;

  constructor(private readonly controller: DialogController) {
    this.controller = controller;
    controller.settings.lock = true;
  }

  activate(msg: any) {
    this.message = msg.message;
    this.title = msg.title;
    this.hideCloseButton = msg.hideCloseButton;
  }
}
