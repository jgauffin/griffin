using System.IO;
using System.Linq;
using System.Text;

namespace Griffin.Networking.Buffers
{
    public static class StreamReaderExtensions
    {
        public static void ConsumeWhiteSpaces(this StreamReader reader)
        {
            while (char.IsWhiteSpace((char) reader.Peek()))
                reader.Read();
        }

        public static string ReadUntil(this StreamReader reader, params char[] chars)
        {
            //reader.BaseStream.
            Encoding encoding = Encoding.UTF8;
            var ch = (char) reader.Peek();
            while (!chars.Contains(ch))
            {
                reader.Read();
            }

            return null;
        }
    }
}