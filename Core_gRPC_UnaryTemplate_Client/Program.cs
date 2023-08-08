 
using Grpc.Net.Client;
using System;
using System.Threading.Tasks;

namespace Core_gRPC_UnaryTemplate_Client
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            while (true)
            {
                var channel = GrpcChannel.ForAddress("https://localhost:5001");
                var messageClient = new message.messageClient(channel);
                Console.WriteLine("Please Enter Message .");
                var messageResponse = await messageClient.GetMessageAsync(new MessageRequest { Message = Console.ReadLine() });
                Console.WriteLine($"Response  : {messageResponse.Message}");
            }
        }
    }
 
}
