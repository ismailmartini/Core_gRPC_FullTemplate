using Grpc.Core;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core_gRPC_Bi_directionalStreamingTemplate_Server
{
    public class MessageService : Message.MessageBase
    {
        private readonly ILogger<MessageService> _logger;
        public MessageService(ILogger<MessageService> logger)
        {
            _logger = logger;
        }

        public async override Task GetMessage(IAsyncStreamReader<MessageRequest> requestStream, IServerStreamWriter<MessageResponse> responseStream, ServerCallContext context)
        {
            Task response = Task.Run(async () =>
            {
                int count = 0;
                while (++count <= 10)
                {
                    await responseStream.WriteAsync(new MessageResponse { Message = $"{count}. Request ok process was done.." });
                    await Task.Delay(1000);
                }
            });

            Task request = Task.Run(async () =>
            {
                while (await requestStream.MoveNext())
                {
                 

                    Console.WriteLine($"Recieved Message From Client.");
                    Console.WriteLine("Message  : " + requestStream.Current.Message);
                }
            });

            await response;
            await request;
        }
    }
}
