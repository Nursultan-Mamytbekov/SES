using Microsoft.Extensions.Configuration;

using SES.Models;
using SES.Services.Abstract;
using SES.Services.Soap.ServiceReference1;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using static SES.Services.Soap.ServiceReference1.InfocomServicePortTypeClient;

namespace SES.Services
{
    public class GetBankPinsService : IGetBankPinsService
    {
        private readonly IConfiguration _configuration;
        public GetBankPinsService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task<bankPinServiceResponse> GetPinAsync(BankPinRequestModel model)
        {
            string remoteAddress = _configuration["SecurityServerAddress"];
            InfocomServicePortTypeClient client = new InfocomServicePortTypeClient(EndpointConfiguration.InfocomServicePort, remoteAddress);
            var request = CreateHeaders();

            request.bankPinService = new bankPinService
            {
                request = new bankPinServiceRequest
                {
                    pin = model.Pin,
                    series = model.Series,
                    number = model.Number,
                    secret = _configuration["BankPinService:Secret"],
                    clientid = _configuration["BankPinService:ClientId"]
                }
            };

            var response = await client.bankPinServiceAsync(
                request.client,
                request.service,
                request.userId,
                request.id,
                request.issue,
                request.protocolVersion,
                request.bankPinService
            );

            return response.bankPinServiceResponse;
        }

        private bankPinServiceRequest1 CreateHeaders()
        {
            return new bankPinServiceRequest1
            {
                service = new XRoadServiceIdentifierType
                {
                    xRoadInstance = "central-server",
                    memberClass = "GOV",
                    memberCode = "70000005",
                    subsystemCode = "passport-service",
                    serviceCode = "bankPinService",
                    serviceVersion = _configuration["BankPinService:ServiceVersion"],
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
