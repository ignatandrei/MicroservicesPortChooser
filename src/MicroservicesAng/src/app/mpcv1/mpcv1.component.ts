import { Component, OnInit, AfterViewInit } from '@angular/core';
import { MPCService } from '../services/mpcv1.service';
import { ShepherdService } from 'angular-shepherd';
@Component({
  selector: 'app-mpcv1',
  templateUrl: './mpcv1.component.html',
  styleUrls: ['./mpcv1.component.css'],
})
export class MPCv1Component implements OnInit, AfterViewInit {
  appName: string = '';
  pcName: string = 'localhost';
  fullName: string = '';
  constructor(
    private MPCService: MPCService,
    private shepherdService: ShepherdService
  ) {}

  ngOnInit(): void {}
  constructFullName(port: string): string {
    this.fullName = 'http://' + (this.pcName || 'localhost') + ':' + port;
    return this.fullName;
  }

  onDeterministic(): void {
    this.MPCService.deterministic(this.appName).subscribe((v) =>
      window.alert(this.constructFullName(v))
    );
  }

  onNonDeterministic(): void {
    this.MPCService.nonDeterministic(this.appName).subscribe((v) =>
      window.alert(this.constructFullName(v))
    );
  }

  ngAfterViewInit() {
    var buttons = [
      {
        classes: 'shepherd-button-secondary',
        text: 'Exit',
        type: 'cancel',
      },
      {
        classes: 'shepherd-button-primary',
        text: 'Back',
        type: 'back',
      },
      {
        classes: 'shepherd-button-primary',
        text: 'Next',
        type: 'next',
      },
    ];
    this.shepherdService.steps = [];
    if (this.shepherdService.tourObject) {
      this.shepherdService.tourObject.steps = [];
    }


    this.shepherdService.defaultStepOptions = {
      cancelIcon: {
        enabled: true,
      },
      modal: true,
      classes: 'custom-class-name-1 custom-class-name-2',
      highlightClass: 'highlight',
      scrollTo: false,
      title: 'Welcome to Port Chooser v1!',
    };
    this.shepherdService.modal = true;
    this.shepherdService.confirmCancel = false;
    this.shepherdService.requiredElements = [
      {
        selector: '.first-element-help',
        message: 'No found intro.',
        title: 'MPCV1',
      },
      {
        selector: '.formAppName-help',
        message: 'waiting for app name',
        title: 'MPCV1',
      },
    ];
    //     this.shepherdService.steps = [];
    // this.shepherdService.tourObject.steps = [];
    this.shepherdService.modal = true;
    this.shepherdService.addSteps([
      {
        id: 'intro',
        attachTo: {
          element: '.first-element-help',
          on: 'bottom',
        },
        text: [
          'Click next for tour or exit if you know how to use already the app',
        ],
        buttons: buttons,
      },

      {
        id: 'nameApp',
        attachTo: {
          element: '.formAppName-help',
          on: 'right',
        },
        text: ['Please input here the name of the microservice'],
        buttons: buttons,
      },
      {
        id: 'generatePort',
        attachTo: {
          element: '.generatePort-help',
          on: 'bottom',
        },
        text: ['And click here to generate static port from name'],
        buttons: buttons,
      },
    ]);
    // this.shepherdService.addSteps(defaultSteps);
  }
  onHelp(): void {
    this.shepherdService.show('intro');
  }
}
