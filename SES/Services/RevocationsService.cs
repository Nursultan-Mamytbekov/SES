using SES.Services.Soap.ServiceReference;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SES.Services
{
    public class RevocationsService
    {
        public async Task<XRoadResponseOf_ArrayOfPermissionResponse> GetPermissions(string pin)
        {
            PersonalDataUsageServiceClient client = new PersonalDataUsageServiceClient();
            var request = CreateHeaders();

            request.GetPermissions = new PermissionRequest
            {
                Pin = pin,
                BeginDate = new DateTime(2020, 01, 01),
                EndDate = new DateTime(9999, 01, 01)
            };

            var response = await client.GetPermissionsAsync(
                request.client,
                request.id,
                request.issue,
                request.protocolVersion,
                request.service,
                request.userId,
                request.GetPermissions
            );

            return response;
        }

        private XRoadRequestOf_PermissionRequest CreateHeaders()
        {
            return new XRoadRequestOf_PermissionRequest
            {
                service = new XRoadServiceIdentifierType
                {
                    xRoadInstance = "central-server",
                    memberClass = "GOV",
                    memberCode = "70000003",
                    subsystemCode = "sf-personal-data",
                    serviceCode = "GetPermissions",
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
