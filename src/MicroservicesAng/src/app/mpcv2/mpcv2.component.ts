import { AfterViewInit, Component, OnInit } from '@angular/core';
import { ShepherdService } from 'angular-shepherd';
import { MPCService } from '../services/mpcv1.service';

@Component({
  selector: 'app-mpcv2',
  templateUrl: './mpcv2.component.html',
  styleUrls: ['./mpcv2.component.css'],
})
export class Mpcv2Component implements OnInit, AfterViewInit {
  appName: string = '';
  pcName: string = 'localhost';
  tag: string = '';
  fullName: string = '';
  constructor(
    private MPCService: MPCService,
    private shepherdService: ShepherdService
  ) {}
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
    this.shepherdService.modal = true;
    this.shepherdService.defaultStepOptions = {
      cancelIcon: {
        enabled: true,
      },
      modal: true,
      classes: 'custom-class-name-1 custom-class-name-2',
      highlightClass: 'highlight',
      scrollTo: false,
      title: 'Welcome to Port Chooser v2!',
    };

    this.shepherdService.confirmCancel = false;
    this.shepherdService.requiredElements = [
      {
        selector: '.first-element-help',
        message: 'No found intro.',
        title: 'MPCV2',
      },
      {
        selector: '.formAppName-help',
        message: 'waiting for app name',
        title: 'MPCV2',
      },
    ];
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
        id: 'nameTag',
        attachTo: {
          element: '.formTag-help',
          on: 'right',
        },
        text: ['Please input here the tag of the microservice -can be a different folder'],
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
  ngOnInit(): void {}
  constructFullName(port: string): string {
    this.fullName = 'http://' + (this.pcName || 'localhost') + ':' + port;
    return this.fullName;
  }
  onHelp(): void {
    this.shepherdService.show('intro');
  }
  onDeterministic(): void {
    this.MPCService.deterministicV2(this.appName, this.tag).subscribe((v) =>
      window.alert(this.constructFullName(v))
    );
  }
}
