using Microsoft.Extensions.Configuration;

using SES.Services.Abstract;
using SES.Services.Soap.ServiceReference;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using static SES.Services.Soap.ServiceReference.PersonalDataUsageServiceClient;

namespace SES.Services
{
    public class PersonalAccountInfoWithSumService : IPersonalAccountInfoWithSumService
    {
        private readonly IConfiguration _configuration;
        public PersonalAccountInfoWithSumService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task<PersonalAccountInfoWithSumResponse> GetPersonalAccountInfoAsync(string pin)
        {
            string remoteAddress = _configuration["SecurityServerAddress"];
            PersonalDataUsageServiceClient client = new PersonalDataUsageServiceClient(EndpointConfiguration.BasicHttpBinding_IPersonalDataUsageService, remoteAddress);

            var request = CreateHeaders();

            request.GetPersonalAccountInfoWithSumInfo = new PersonalAccountInfoWithSumRequest
            {
                Pin = pin
            };

            var response = await client.GetPersonalAccountInfoWithSumInfoAsync(
                request.client,
                request.id,
                request.issue,
                request.protocolVersion,
                request.service,
                request.userId,
                request.GetPersonalAccountInfoWithSumInfo
            );

            return response.GetPersonalAccountInfoWithSumInfoResponse;

        }

        private XRoadRequestOf_PersonalAccountInfoWithSumRequest CreateHeaders()
        {
            return new XRoadRequestOf_PersonalAccountInfoWithSumRequest
            {
                service = new XRoadServiceIdentifierType
                {
                    xRoadInstance = "central-server",
                    memberClass = "GOV",
                    memberCode = "70000003",
                    subsystemCode = "sf-personal-data",
                    serviceCode = "GetPersonalAccountInfoWithSumInfo",
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
