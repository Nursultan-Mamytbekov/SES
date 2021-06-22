using SES.Models;
using SES.Services.Soap.ServiceReference;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SES.Services
{
    public class PermissionsService
    {
        public async Task<RequestForPermissionResponse> SendRequestForPermission(RequestPermissionModel model)
        {
            PersonalDataUsageServiceClient client = new PersonalDataUsageServiceClient();
            var request = GetRequestForPermissionHeaders();
            request.InitializeRequestForPermission = new RequestForPermissionModel()
            {               
                Pin = model.Pin,
                PhoneNumber = model.PhoneNumber,
                LastName = model.LastName,
                FirstName = model.FirstName,
                Patronymic = model.Patronymic,
                EndDate = model.EndDate,
                BirthDate = model.BirthDate,
                OrganizationId = Guid.Parse(model.OrganizationId),
                PassportAddress = model.PassportAddress,
                FactAddress = model.FactAddress,
                PassportNumberAndSeries = model.PassportNumber,
                PassportIssuedDate = model.PassportIssuedDate,
                PassportIssuedBy = model.PassportIssuedBy,
                Email = model.Email,
            };

            var response = await client.InitializeRequestForPermissionAsync(
                request.client,
                request.id,
                request.issue,
                request.protocolVersion,
                request.service,
                request.userId,
                request.InitializeRequestForPermission
            );

            return response.InitializeRequestForPermissionResponse;
        }

        public async Task<SendCodeForPermissionResponse> SendCodeForPermission(CodeForPermissionModel model)
        {
            PersonalDataUsageServiceClient client = new PersonalDataUsageServiceClient();
            var request = GetCodeForPermissionHeaders();
            request.SendConfirmationCodeForPermission = new PermissionCode()
            {
                RequestId = Guid.Parse(model.RequestId),
                Code = model.Code,
            };

            var response = await client.SendConfirmationCodeForPermissionAsync(
                request.client,
                request.id,
                request.issue,
                request.protocolVersion,
                request.service,
                request.userId,
                request.SendConfirmationCodeForPermission
            );

            return response.SendConfirmationCodeForPermissionResponse;
        }

        private XRoadRequestOf_RequestForPermissionModel GetRequestForPermissionHeaders()
        {
            return new XRoadRequestOf_RequestForPermissionModel
            {
                service = new XRoadServiceIdentifierType
                {
                    xRoadInstance = "central-server",
                    memberClass = "GOV",
                    memberCode = "70000003",
                    subsystemCode = "sf-personal-data",
                    serviceCode = "InitializeRequestForPermission",
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

        private XRoadRequestOf_PermissionCode GetCodeForPermissionHeaders()
        {
            return new XRoadRequestOf_PermissionCode
            {
                service = new XRoadServiceIdentifierType
                {
                    xRoadInstance = "central-server",
                    memberClass = "GOV",
                    memberCode = "70000003",
                    subsystemCode = "sf-personal-data",
                    serviceCode = "SendConfirmationCodeForPermission",
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
