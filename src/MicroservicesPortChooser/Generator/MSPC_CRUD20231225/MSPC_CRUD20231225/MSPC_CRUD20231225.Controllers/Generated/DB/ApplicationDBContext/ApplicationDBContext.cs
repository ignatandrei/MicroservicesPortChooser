﻿//modified 2023.12.15
//3.this was autogenerated by a tool. Do not modify! Use partial
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Generated;

public partial class RegisterApplicationDBContext : IRegisterContext
{
    public Type AddServices(IServiceCollection services, ConfigurationManager configuration){

        var cnString = configuration.GetConnectionString("ApplicationDBContext");
        //var cnString = Environment.GetEnvironmentVariable("AZURE_SQL_CONNECTIONSTRING");
        if (string.IsNullOrWhiteSpace(cnString))
        {
            throw new ArgumentException("please add  connection string ApplicationDBContext into appsettings.json ");
        }

        services.AddDbContext<ApplicationDBContext>(options =>
              {
              //options.UseSqlServer("Data Source=.;Initial Catalog=TestData;UId=sa;pwd=<YourStrong@Passw0rd>;TrustServerCertificate=true;");
              options.UseSqlServer(cnString);
              }
          );
                  services.AddTransient<ISearchDataMSPC_Register, SearchDataMSPC_Register>();
                  return typeof(ApplicationDBContext);
    }
    
    [ModuleInitializer]
    public static void AddMe()
    {
        Generated.UtilsControllers.registerContexts.Add(new RegisterApplicationDBContext());
    }

    
}

