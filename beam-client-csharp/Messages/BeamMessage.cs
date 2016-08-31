using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace beam_client_csharp.Messages
{
    public class BeamMessage
    {
        public string type { get; set; }
        public virtual int id { get; set; }

        public BeamMessage()
        {
            type = "unknown";
        }
    }
}
