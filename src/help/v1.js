
function sleep(ms) {
    return new Promise(resolve => setTimeout(resolve, ms));
  }

const { chromium } = require('playwright');
const  fs  = require('fs');
(async () => { 


var nrSceenshots = 0;
const prefix='./generated/v1/';
    
const browser = await chromium.launch({ headless: true});

const context = await browser.newContext({
    recordVideo: {
      dir: "./recordings"
    }});

const page = await context.newPage()
await page.setViewportSize({ width: 1500, height: 889 })

await page.goto('https://microservicesportchooser.azurewebsites.net/static/mpcv1')
await page.screenshot({ path: prefix+ (++nrSceenshots)+"page.png"});

const input = await page.$('input[name="appName"]');
await input.hover();
// await page.click('input[name="appName"]')
// await page.type('input[name="appName"]', 'MyMicroserviceName')
var parentInput = await input.$('xpath=..');
await parentInput.screenshot({ path: prefix+ (++nrSceenshots)+"microservice_name.png"});
await input.type('MyMicroserviceName')

await page.screenshot({ path: prefix+ (++nrSceenshots)+"page.png"});

const buttonDeter = await page.$("//button[contains(., 'Static')]");
await buttonDeter.hover();
await buttonDeter.screenshot( { path: prefix+ (++nrSceenshots)+"generate_port.png"});
await buttonDeter.click();
// const element1 = await page.$('.mat-typography > app-root > app-primary-navig > .mat-drawer-container > .mat-drawer-content')
await sleep(5000);
await page.screenshot({ path: prefix+ (++nrSceenshots)+'page.png' })

 await context.close();
 
 const videoFileName = await page.video().path()
console.log(videoFileName);
fs.renameSync( videoFileName, prefix+'video.webm' );
await browser.close();
 

})();