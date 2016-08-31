using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using beam_client_csharp.Messages;

namespace beam_client_csharp.EventHandlers
{
    static class BeamMethodHandler
    {
        public static void HandleMethod(BeamMethodMessage message)
        {
            switch (message.method)
            {
                default:
                    throw new NotImplementedException(message.method);

                case "msg":
                    Console.WriteLine("Received Chat message: {0}", message.arguments[0]);
                    break;
            }
        }
    }
}
