using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.Extensions.NETCore.Setup;
using Amazon.IdentityManagement;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.DI
{
    public static class AwsServiceCollectionExtensions
    {
        public static void AddAwsServices(this IServiceCollection services, AWSOptions awsConfig)
        {
            services.AddDefaultAWSOptions(awsConfig);
            services.AddAWSService<IAmazonDynamoDB>();
            services.AddAWSService<IAmazonIdentityManagementService>();
            services.AddScoped<IDynamoDBContext, DynamoDBContext>();            
        }
    }
}
