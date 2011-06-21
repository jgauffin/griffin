using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Griffin.Core.Net.Buffers;
using Xunit;

namespace Griffin.Core.Tests.Net.Buffers
{
    public class BufferSliceReaderTest
    {
        private byte[] _sourceBuffer;
        private byte[] _buffer;
        private BufferSlice _slice;
        private BufferSliceReader _reader;
        private const int Offset = 1024;
        private const int Length = 1024;

        public BufferSliceReaderTest()
        {
            _sourceBuffer = Encoding.ASCII.GetBytes("Hello world!\r\nSays \"SomeONe not powerful\"");
            _buffer = new byte[65535];
            Buffer.BlockCopy(_sourceBuffer, 0, _buffer, Offset, _sourceBuffer.Length);
            _slice = new BufferSlice(_buffer, Offset, Length) { Count = _sourceBuffer.Length };

            _reader = new BufferSliceReader(_slice);
        }

        [Fact]
        public void TestCurrent()
        {
            Assert.Equal('H', _reader.Current);
        }

        [Fact]
        public void TestConsume()
        {
            _reader.Consume();
            Assert.Equal('e', _reader.Current);
        }

        [Fact]
        public void TestConsumeOne()
        {
            _reader.Consume('H');
            Assert.Equal('e', _reader.Current);
        }

        [Fact]
        public void TestConsumeNonExistant()
        {
            _reader.Consume('e');
            Assert.Equal('H', _reader.Current);
        }

        [Fact]
        public void TestConsumeMultiple()
        {
            _reader.Consume('e', 'H');
            Assert.Equal('l', _reader.Current);
        }

        [Fact]
        public void TestConsumeUntilNonExistant()
        {
            SetBuffer("Hit haopopens!");
            _reader.ConsumeUntil('q');
            Assert.Equal('H', _reader.Current);
        }

        [Fact]
        public void TestConsumeUntilValid()
        {
            _reader.ConsumeUntil('l');
            Assert.Equal('l', _reader.Current);
        }

        [Fact]
        public void TestConsumeWhiteSpaces()
        {
            var temp = "   \t  :WORLD";
            SetBuffer(temp);

            _reader.ConsumeWhiteSpaces();
            Assert.Equal(':', _reader.Current);
        }

        [Fact]
        public void TestConsumeWhiteSpacesAndExtra()
        {
            var temp = "   \t  :WORLD";
            SetBuffer(temp);

            _reader.ConsumeWhiteSpaces(':');
            Assert.Equal('W', _reader.Current);
        }

        private void SetBuffer(string temp)
        {
            var bytes = Encoding.ASCII.GetBytes(temp);
            Buffer.BlockCopy(bytes, 0, _buffer, Offset, bytes.Length);
            _slice.Count = bytes.Length;
        }


        [Fact]
        public void TestContainsValid()
        {
            Assert.True(_reader.Contains('!'));
            Assert.Equal('H', _reader.Current);
        }

        [Fact]
        public void TestContainsNot()
        {
            Assert.False(_reader.Contains('q'));
            Assert.Equal('H', _reader.Current);
        }

        [Fact]
        public void NotEof()
        {
            Assert.False(_reader.EndOfFile);
        }

        [Fact]
        public void NotEofButAlmostEnd()
        {
            _slice.CurrentOffset = _slice.EndOffset - 1;
            Assert.False(_reader.EndOfFile);
        }

        [Fact]
        public void Eof()
        {
            _slice.CurrentOffset = _slice.EndOffset;
            Assert.True(_reader.EndOfFile);
        }

        [Fact]
        public void EndGotNoMore()
        {
            _slice.CurrentOffset = _slice.EndOffset;
            Assert.False(_reader.HasMore);
        }

        [Fact]
        public void AlmostEndGotMore()
        {
            SetBuffer("Hello World!");
            _slice.CurrentOffset = _slice.EndOffset - 1;
            Assert.True(_reader.HasMore);
            Assert.Equal('!', _reader.Current);
            
        }

        [Fact]
        public void TestLength()
        {
            Assert.Equal(_sourceBuffer.Length, _reader.Length);
            
        }

        [Fact]
        public void TestRead()
        {
            Assert.Equal('H', _reader.Read());
            Assert.Equal('e', _reader.Read());
        }

        [Fact]
        public void TestReadAtEnd()
        {
            _slice.CurrentOffset = _slice.EndOffset;
            Assert.Equal(char.MinValue, _reader.Read());
        }

        [Fact]
        public void ReadLine()
        {
            Assert.Equal("Hello world!", _reader.ReadLine());
        }

        [Fact]
        public void ReadLineThatDoNotExist()
        {
            _reader.ReadLine();
            Assert.Equal(null, _reader.ReadLine());
        }

        [Fact]
        public void ReadOnlyQuoted()
        {
            var actual = @"""Only quoted""";
            SetBuffer(actual);
            Assert.Equal("Only quoted", _reader.ReadQuotedString());
            Assert.True(_reader.EndOfFile);
        }

        [Fact]
        public void ReadQuouted()
        {
            SetBuffer(@"""Only quoted"" with more");
            Assert.Equal("Only quoted", _reader.ReadQuotedString());
            Assert.Equal(_reader.Current, ' ');
        }

        [Fact]
        public void ReadToEnd()
        {
            Assert.Equal("Hello world!\r\nSays \"SomeONe not powerful\"", _reader.ReadToEnd());
        }

        [Fact]
        public void ReadToEndOrChar()
        {
            Assert.Equal("Hello", _reader.ReadToEnd(' '));
        }

        [Fact]
        public void ReadToEndOrNonExistantChar()
        {
            Assert.Equal("Hello world!\r\nSays \"SomeONe not powerful\"", _reader.ReadToEnd('q'));
        }

        [Fact]
        public void ReadToExistantCharStr()
        {
            Assert.Equal("Hello world!", _reader.ReadToEnd(" q!"));
        }




        [Fact]
        public void TestReadWord()
        {
            var actual = _reader.ReadWord();
            Assert.Equal("Hello", actual);
            Assert.Equal(' ', _reader.Current);
        }


        [Fact]
        public void TestReadUntilSpaceStr()
        {
            Assert.Equal("Hello", _reader.ReadUntil(" "));
            Assert.Equal(' ', _reader.Current);
        }

        [Fact]
        public void TestReadUntilSpaceChar()
        {
            Assert.Equal("Hello", _reader.ReadUntil(' '));
            Assert.Equal(' ', _reader.Current);

            
        }
    }
}
