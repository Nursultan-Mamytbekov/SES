using Microsoft.Extensions.Configuration;

using SES.Services.Abstract;
using SES.Services.Soap.ServiceReference;

using System;
using System.Threading.Tasks;

using static SES.Services.Soap.ServiceReference.PersonalDataUsageServiceClient;

namespace SES.Services
{
    public class GetPensionInfoWithSumService : IGetPensionInfoWithSumService
    {
        private readonly IConfiguration _configuration;
        
        public GetPensionInfoWithSumService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<PensionInfoWithSumResponse> GetPensionInfoWithSumAsync(string pin)
        {
            string remoteAddress = _configuration["SecurityServerAddress"];
            PersonalDataUsageServiceClient client = new PersonalDataUsageServiceClient(EndpointConfiguration.BasicHttpBinding_IPersonalDataUsageService, remoteAddress);
            
            var request = CreateHeaders();


            request.GetPensionInfoWithSum = new PensionInfoWithSumRequestDto
            {
                Pin = pin
            };

            var response = await client.GetPensionInfoWithSumAsync(
                request.client,
                request.id,
                request.issue,
                request.protocolVersion,
                request.service,
                request.userId,
                request.GetPensionInfoWithSum
            );

            return response.GetPensionInfoWithSumResponse;

        }

        private XRoadRequestOf_PensionInfoWithSumRequestDto CreateHeaders()
        {
            return new XRoadRequestOf_PensionInfoWithSumRequestDto
            {
                service = new XRoadServiceIdentifierType
                {
                    xRoadInstance = "central-server",
                    memberClass = "GOV",
                    memberCode = "70000003",
                    subsystemCode = "sf-personal-data",
                    serviceCode = "GetPensionInfoWithSum",
                    objectType = XRoadObjectType.SERVICE
                },
                client = new XRoadClientIdentifierType
                {
                    xRoadInstance = "central-server",
                    memberClass = _configuration["ClientOptions:MemberClass"],
                    memberCode = _configuration["ClientOptions:MemberCode"],
                    subsystemCode = _configuration["ClientOptions:SubsystemCode"],
                    objectType = XRoadObjectType.SUBSYSTEM
                },
                protocolVersion = "4.0",
                id = Guid.NewGuid().ToString(),
                userId = _configuration["ClientOptions:UserId"]
            };
        }
    }
}
