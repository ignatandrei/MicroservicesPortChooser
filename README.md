# MicroservicesPortChooser
If you have multiple microservices and do not know how to choose a port, generate same static port number every time for the name of the application

## Demo 

https://microservicesportchooser.azurewebsites.net/

## Swagger / OpenAPI
https://microservicesportchooser.azurewebsites.net/swagger/index.html

## How to deploy on your  microservice platform

Download your release :
### Windows x64: 

Download https://github.com/ignatandrei/MicroservicesPortChooser/releases/latest/download/releaseWin.zip/

and start the application with the following command:  

MicroservicesPortChooser --urls http://0.0.0.0:24079

Read more at https://docs.microsoft.com/en-us/aspnet/core/fundamentals/servers/kestrel/endpoints?view=aspnetcore-5.0

### IIS x64: 

Download https://github.com/ignatandrei/MicroservicesPortChooser/releases/latest/download/releaseIISWin.zip/

Install ASP.NET Core Module from https://docs.microsoft.com/en-us/aspnet/core/host-and-deploy/aspnet-core-module?view=aspnetcore-5.0

Read more https://docs.microsoft.com/en-us/aspnet/core/host-and-deploy/iis/?view=aspnetcore-5.0

### Linux x64: 

Download https://github.com/ignatandrei/MicroservicesPortChooser/releases/latest/download/releaseLin.zip/
NGINX : read https://docs.microsoft.com/en-us/aspnet/core/host-and-deploy/linux-nginx?view=aspnetcore-5.0
APACHE: https://docs.microsoft.com/en-us/aspnet/core/host-and-deploy/linux-apache?view=aspnetcore-5.0


## How to use in your application

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

IIS x64: https://github.com/ignatandrei/MicroservicesPortChooser/releases/latest/download/releaseIISWin.zip/

Linux x64: https://github.com/ignatandrei/MicroservicesPortChooser/releases/latest/download/releaseLin.zip/

Versioning: https://microservicesportchooser.azurewebsites.net/ams

Source Code: https://github.com/ignatandrei/MicroservicesPortChooser/

Nuget : <a href='https://www.nuget.org/packages/MicroservicesPortChooser/' target='_blank'>
![Nuget](https://img.shields.io/nuget/v/MicroservicesPortChooser)
</a>


PR Welcomed!
