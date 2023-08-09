using Grpc.Core;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core_gRPC_ClientStreamingTemplate_Server
{
    public class MessageService : Message.MessageBase
    {
        private readonly ILogger<MessageService> _logger;
        public MessageService(ILogger<MessageService> logger)
        {
            _logger = logger;
        }

        public override async Task<MessageResponse> GetMessage(IAsyncStreamReader<MessageRequest> requestStream, ServerCallContext context)
        {
            await Task.Run(async () =>
            {
                while (await requestStream.MoveNext())
                {

                    Console.WriteLine($"Recieved Message From Client.");
                    Console.WriteLine("Message  : "+ requestStream.Current.Message);
                    
                     
                }
            });

            return new MessageResponse { Message = "Request OK /  Return Ok..." };
        }
    }
}
