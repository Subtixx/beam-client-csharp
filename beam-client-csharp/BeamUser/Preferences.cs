namespace beam_client_csharp
{
    public class Preferences
    {
        public string chat_sounds_play { get; set; }
        public bool chat_sounds_html5 { get; set; }
        public bool chat_timestamps { get; set; }
        public bool chat_whispers { get; set; }
        public bool chat_chromakey { get; set; }
        public bool chat_lurkmode { get; set; }
        public ChannelNotifications channel_notifications { get; set; }
        public bool channel_mature_allowed { get; set; }
        public ChannelPlayer channel_player { get; set; }
        public bool chat_tagging { get; set; }
        public bool chat_colors { get; set; }
        public int chat_sounds_volume { get; set; }
    }
}