using System.IO;
using System.Reflection;

namespace beam_client_csharp.Tests
{
    public class TestHelpers
    {
        // Get directory of test DLL
        public static string ExecutionString = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

        public static string GetResource(string manifest)
        {
            var assembly = Assembly.GetExecutingAssembly();
            using (var stream = assembly.GetManifestResourceStream(manifest))
                if (stream != null)
                    using (var reader = new StreamReader(stream))
                    {
                        return reader.ReadToEnd();
                    }

            return "";
        }
    }
}