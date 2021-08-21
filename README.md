# MicroservicesPortChooser
If you have multiple microservices and do not know how to choose a port, generate same static port number every time for the name of the application

## Application Examples:

## Demo 

https://microservicesportchooser.azurewebsites.net/

## Swagger / OpenAPI
https://microservicesportchooser.azurewebsites.net/swagger/index.html

## How to use

### .NET Core
Make a HttpClient and call the service with the following url:
https://microservicesportchooser.azurewebsites.net/api/v1/PortChooser/GetDeterministicPortFrom/{serviceName}

Or add the Nuget package 
<a href='https://www.nuget.org/packages/MicroservicesPortChooser/' target='_blank'>
![Nuget](https://img.shields.io/nuget/v/MicroservicesPortChooser)
</a>

### Express

```javascript
const express = require('express');
const axios = require('axios');

const app = express();

app.get('/', function (req, res) {
  res.send('hello world');
})
//replace test with your service name
axios.get('https://microservicesportchooser.azurewebsites.net/api/v1/PortChooser/GetDeterministicPortFrom/test')
  .then(function (response) {
    app.listen(Number(response.data));
  })
  .catch(function (error) {
    // handle error
    console.log(error);
  });      
```


### Releases

Windows x64: https://github.com/ignatandrei/MicroservicesPortChooser/releases/latest/download/releaseWin.zip/

Linux x64: https://github.com/ignatandrei/MicroservicesPortChooser/releases/latest/download/releaseLin.zip/

Dotnet Tool: TBD

Versioning: https://microservicesportchooser.azurewebsites.net/ams

Source Code: https://github.com/ignatandrei/MicroservicesPortChooser/

Nuget : ![Nuget](https://img.shields.io/nuget/v/MicroservicesPortChooser)
PR Welcomed!
