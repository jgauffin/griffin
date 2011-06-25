using Griffin.Networking.Buffers;

namespace Griffin.Networking.Protocols.Http.Implementation.Headers.Parsers
{
    [ParserFor(AuthorizationHeader.NAME)]
    internal class AuthorizationParser : IHeaderParser
    {
        #region IHeaderParser Members

        public IHeader Parse(string name, ITextParser reader)
        {
            var header = new AuthorizationHeader();
            reader.ConsumeWhiteSpaces();
            header.Scheme = reader.ReadWord();
            reader.ConsumeWhiteSpaces();
            header.Data = reader.ReadToEnd();
            return header;
        }

        #endregion
    }
}