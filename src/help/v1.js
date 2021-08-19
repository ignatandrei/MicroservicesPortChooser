const { chromium } = require('playwright');
(async () => { 
    
const browser = await chromium.launch({ headless: false });
const page = await browser.newPage()
await page.goto('https://microservicesportchooser.azurewebsites.net/static/mpcv1')

await page.setViewportSize({ width: 1500, height: 889 })

await page.click('input[name="appName"]')
await page.type('input[name="appName"]', 'MyMicroserviceName')

const buttonDeter = await page.$("//button[contains(., 'Static')]");
await buttonDeter.click();
// const element1 = await page.$('.mat-typography > app-root > app-primary-navig > .mat-drawer-container > .mat-drawer-content')
await page.screenshot({ path: 'staticV1.png' })

// await browser.close();

})();