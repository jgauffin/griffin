using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Griffin.Core.Converters;
using Xunit;

namespace Griffin.Core.Tests
{

    public class ConverterTester 
    {
        [Fact]
        public void ConvertFromStringToDateUsingDefaultConverter()
        {
            var date = "2011-01-02".ConvertTo<DateTime>();
            Assert.Equal(new DateTime(2011, 01, 02), date);
        }

        [Fact]
        public void ConvertFromStringToDateTime()
        {
            Converter.Register(new StringToDateTimeConverter());
            var date = "2011-01-02".ConvertTo<DateTime>();
            Assert.Equal(new DateTime(2011,01,02), date);
        }

        [Fact]
        public void TestStringToInt()
        {
            Assert.Equal(10, "10".ConvertTo<int>());
        }
    }
}
