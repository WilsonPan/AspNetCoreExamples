using System;
using System.Threading.Tasks;
using gRPC.Services;
using Grpc.Net.Client;

namespace gRPC.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            var channel = GrpcChannel.ForAddress("https://localhost:5001");
            var client = new SmsSender.SmsSenderClient(channel);
            var result = client.SendSms(new SmsRequest
            {
                Tel = "15993939393",
                Content = "发送短信内容"
            });

            Console.WriteLine(System.Text.Json.JsonSerializer.Serialize(result));
        }
    }
}