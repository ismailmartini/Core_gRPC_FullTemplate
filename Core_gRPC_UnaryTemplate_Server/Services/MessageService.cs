using Grpc.Core;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core_gRPC_UnaryTemplate_Server
{
    public class MessageService : message.messageBase
    {
        private readonly ILogger<MessageService> _logger;
        public MessageService(ILogger<MessageService> logger)
        {
            _logger = logger;
        }

        public async override Task<MessageResponse> GetMessage(MessageRequest request, ServerCallContext context)
        {
            Console.WriteLine($"Recieve Message.");
            Console.WriteLine("Message : ");
            Console.WriteLine(request.Message);

            return new MessageResponse
            {
                Message = "Request Ok ..."
            };
        }


    }
}
