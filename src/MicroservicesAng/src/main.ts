import { enableProdMode } from '@angular/core';
import { platformBrowserDynamic } from '@angular/platform-browser-dynamic';

import { AppModule } from './app/app.module';
import { environment } from './environments/environment';
import Tracker from '@openreplay/tracker';


if (environment.production) {
  enableProdMode();
}

platformBrowserDynamic().bootstrapModule(AppModule)
.then(() => {
  const tracker = new Tracker({
    projectKey: "kbcFbNn6mWwBgtMGIn2q",  
  });
    tracker.start();
})
  .catch(err => console.error(err));
