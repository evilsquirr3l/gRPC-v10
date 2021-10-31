using System;
using System.Net.Http;
using System.Threading.Tasks;
using Grpc.Net.Client;

namespace gRPC.Client
{
    class Program
    {
        static async Task Main(string[] args)
        {
            using var channel = GrpcChannel.ForAddress("http://localhost:5000", new GrpcChannelOptions()
            {
                HttpHandler = GetHttpClientHandler()
            });
            
            // создаем клиента
            var client = new Tasks.TasksClient(channel);
            Console.Write("Took your variant: ");
            var message = Convert.ToInt32(Console.ReadLine());
            
            // обмениваемся сообщениями с сервером
            var reply = await client.SendRequestAsync(new Request {RequestMessage = message});
            Console.WriteLine(reply.ReplyMessage);
            Console.ReadKey();
        }

        private static HttpClientHandler GetHttpClientHandler()
        {
            var httpHandler = new HttpClientHandler();
            // Return `true` to allow certificates that are untrusted/invalid
            httpHandler.ServerCertificateCustomValidationCallback =
                HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;
            return httpHandler;
        }
    }
}