using Grpc.Core;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Core_gRPC_ServerStreamingTemplate_Server.Message;

namespace Core_gRPC_ServerStreamingTemplate_Server
{
    public class MessageService :MessageBase
    {
        public override async Task GetMessage(MessageRequest request, IServerStreamWriter<MessageResponse> responseStream, ServerCallContext context)
        {
            Console.WriteLine($"Recieved Message From Client.");
            Console.WriteLine("Message  : ");
            Console.WriteLine(request.Message);

            await Task.Run(async () =>
            {
                int count = 0;
                while (++count <= 10)
                {
                    await responseStream.WriteAsync(new MessageResponse { Message = $"Send Ex Message {count}" });
                    await Task.Delay(1000);
                }
            });
        }
    }
}
