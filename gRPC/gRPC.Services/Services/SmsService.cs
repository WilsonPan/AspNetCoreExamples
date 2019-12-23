using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Grpc.Core;
using Microsoft.Extensions.Logging;

namespace gRPC.Services
{
    public class SmsService : SmsSender.SmsSenderBase
    {
        private readonly ILogger<SmsService> _logger;
        public SmsService(ILogger<SmsService> logger)
        {
            _logger = logger;
        }

        public override Task<SmsResponse> SendSms(SmsRequest request, ServerCallContext context)
        {
            return Task.FromResult(new SmsResponse
            {
                Code = 1,
                Message = "发送成功"
            });
        }
    }
}
