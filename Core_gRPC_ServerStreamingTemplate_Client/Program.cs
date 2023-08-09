using System;
using System.Threading.Tasks;
 


using Grpc.Net.Client;
namespace Core_gRPC_ServerStreamingTemplate_Client
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var channel = GrpcChannel.ForAddress("https://localhost:5001");
            var messageClient = new Message.MessageClient(channel);
            Console.WriteLine("Client Running.");
            var messageResponse = messageClient.GetMessage(new MessageRequest { Message = Console.ReadLine() });

            await Task.Run(async () =>
            {
                while (await messageResponse.ResponseStream.MoveNext(new System.Threading.CancellationToken()))
                    Console.WriteLine($"Recieved Message From Server. : {messageResponse.ResponseStream.Current.Message}");
            });
        }
    }
}
