using Grpc.Net.Client;
using System;
using System.Threading.Tasks;

namespace Core_gRPC_FullTemplate_Client
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var channel = GrpcChannel.ForAddress("https://localhost:5001");
            var greetClient = new Greeter.GreeterClient(channel);
            HelloReply response = await greetClient.SayHelloAsync(new HelloRequest { Name = "ismail" });
            Console.WriteLine($"recieved message : {response.Message}");
            Console.ReadKey();
        }
    }
}
