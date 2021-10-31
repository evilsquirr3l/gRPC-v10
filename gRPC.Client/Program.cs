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
            var httpHandler = new HttpClientHandler();
            // Return `true` to allow certificates that are untrusted/invalid
            httpHandler.ServerCertificateCustomValidationCallback =
                HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;
            
            using var channel = GrpcChannel.ForAddress("http://localhost:5000", new GrpcChannelOptions()
            {
                HttpHandler = httpHandler
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
    }
}