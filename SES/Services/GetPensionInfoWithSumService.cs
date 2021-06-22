using SES.Services.Abstract;
using SES.Services.Soap.ServiceReference;

using System;
using System.Threading.Tasks;

namespace SES.Services
{
    public class GetPensionInfoWithSumService : IGetPensionInfoWithSumService
    {
        public async Task<PensionInfoWithSumResponse> GetPensionInfoWithSumAsync(string pin)
        {
            PersonalDataUsageServiceClient client = new PersonalDataUsageServiceClient();
            
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
