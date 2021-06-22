using SES.Services.Abstract;
using SES.Services.Soap.ServiceReference;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SES.Services
{
    public class PersonalAccountInfoWithSumService : IPersonalAccountInfoWithSumService
    {
        public async Task<PersonalAccountInfoWithSumResponse> GetPersonalAccountInfoAsync(string pin)
        {
            PersonalDataUsageServiceClient client = new PersonalDataUsageServiceClient();

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
                    memberClass = "GOV",
                    memberCode = "70000003",
                    subsystemCode = "settlements-service",
                    objectType = XRoadObjectType.SUBSYSTEM
                },
                protocolVersion = "4.0",
                id = Guid.NewGuid().ToString(),
                userId = "fc12b8d1-dc09-49f1-95f7-911bb2f97cc0"
            };
        }
    }
}
