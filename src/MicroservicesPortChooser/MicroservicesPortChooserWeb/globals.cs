global using Microsoft.AspNetCore.Hosting;
global using Microsoft.AspNetCore.Http;
global using Microsoft.Extensions.Configuration;
global using Microsoft.Extensions.Hosting;
global using Microsoft.Extensions.Logging;
global using System;
global using System.Collections.Generic;
global using System.Linq;
global using System.Threading.Tasks;
global using MSPCWebExtension;
global using MicroservicesPortChooserBL;
global using Microsoft.AspNetCore.Mvc;
global using MSPC_Interfaces;
global using Microsoft.AspNetCore.Mvc.ApiExplorer;
global using Microsoft.Extensions.DependencyInjection;
global using Microsoft.Extensions.Options;
global using Microsoft.OpenApi.Models;
global using Swashbuckle.AspNetCore.SwaggerGen;
global using Microsoft.AspNetCore.Builder;
global using AMSWebAPI;
//global using appSettingsEditor;
global using HealthChecks.UI.Client;
global using Hellang.Middleware.ProblemDetails;
global using Microsoft.AspNetCore.Diagnostics.HealthChecks;
global using Microsoft.AspNetCore.HttpOverrides;
global using MSPC_DAL;
global using NetCore2BlocklyNew;
global using MicroservicesPortChooserWeb;
global using Microsoft.EntityFrameworkCore;
global using Generated;
global using AMS_Base;
[assembly: AMS_Base.VersionReleased(Name = "FutureRelease", ISODateTime = "9999-04-16", recordData = AMS_Base.RecordData.Merges)]
[assembly: VersionReleased(Name = "CRUD", ISODateTime = "2022-04-18", recordData = RecordData.Merges)]
[assembly: VersionReleased(Name = "Net6", ISODateTime = "2022-04-15", recordData = RecordData.Merges)]
