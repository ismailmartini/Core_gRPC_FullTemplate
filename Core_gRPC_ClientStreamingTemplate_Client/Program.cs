using Grpc.Net.Client;
using System;
using System.Threading.Tasks;

namespace Core_gRPC_ClientStreamingTemplate_Client
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var channel = GrpcChannel.ForAddress("https://localhost:5001");
            var messageClient = new Message.MessageClient(channel);
            Console.WriteLine("Client Running");
            var request = messageClient.GetMessage();

            await Task.Run(async () =>
            {
                int count = 0;
                while (++count <= 10)
                {
                    await request.RequestStream.WriteAsync(new MessageRequest { Message = $"Sending Message {count}" });
                    await Task.Delay(1000);
                }
            });

            //RequestStream success
            await request.RequestStream.CompleteAsync();

            var response = await request;
            Console.WriteLine($"Response : {response.Message}");
        }
    }
}
