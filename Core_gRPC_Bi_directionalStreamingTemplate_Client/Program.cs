using System;
using System.Threading;
using System.Threading.Tasks;

using Grpc.Net.Client;


namespace Core_gRPC_Bi_directionalStreamingTemplate_Client
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var channel = GrpcChannel.ForAddress("https://localhost:5001");
            var messageClient = new Message.MessageClient(channel);
            Console.WriteLine("Client Running.");
            var getMessage = messageClient.GetMessage();

            Task request = Task.Run(async () =>
            {
                int count = 0;
                while (++count <= 10)
                {
                    await getMessage.RequestStream.WriteAsync(new MessageRequest { Message = $"Sending message  {count}" });
                    await Task.Delay(1000);
                }
            });

            CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
            Task response = Task.Run(async () =>
            {
                while (await getMessage.ResponseStream.MoveNext(cancellationTokenSource.Token))
                    Console.WriteLine(getMessage.ResponseStream.Current.Message);
            });
            await request;
            //RequestStream is done.  
            await getMessage.RequestStream.CompleteAsync();
            await response;
        }
    }
}
