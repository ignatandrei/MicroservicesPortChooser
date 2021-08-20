const { chromium } = require('playwright');
(async () => { 
    
const browser = await chromium.launch({ headless: false });
const page = await browser.newPage()
await page.goto('https://microservicesportchooser.azurewebsites.net/static/mpcv1')

await page.setViewportSize({ width: 1500, height: 889 })

const input = await page.$('input[name="appName"]');
// await page.click('input[name="appName"]')
// await page.type('input[name="appName"]', 'MyMicroserviceName')
var parentInput = await input.$('xpath=..');
await parentInput.screenshot({ path: "microservice_name.png"});
await input.type('MyMicroserviceName')

const buttonDeter = await page.$("//button[contains(., 'Static')]");
await buttonDeter.screenshot( { path: "generate_port.png"});
await buttonDeter.click();
// const element1 = await page.$('.mat-typography > app-root > app-primary-navig > .mat-drawer-container > .mat-drawer-content')
await page.screenshot({ path: 'staticV1.png' })

 await browser.close();

})();