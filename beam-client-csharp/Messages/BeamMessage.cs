namespace beam_client_csharp.Messages
{
    public class BeamMessage
    {
        public BeamMessage()
        {
            type = "unknown";
        }

        public string type { get; set; }
        public virtual int id { get; set; }
    }
}