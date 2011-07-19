using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Griffin.Core.Data.SimpleMapper;
using Xunit;

namespace Griffin.Core.Tests.Data.Mappings
{
    public class PropertyMappingTests
    {
        [Fact]
        public void TestStringMappingGet() 
        {
            var mapping = new ColumnPropertyMapping<TestUser, string>("UserName", model => model.Name);
            var user = new TestUser {Name = "Arne"};
            Assert.Equal("Arne", mapping.GetValue(user));
        }

        [Fact]
        public void TestStringMappingNullGet()
        {
            var mapping = new ColumnPropertyMapping<TestUser, string>("UserName", model => model.Name);
            var user = new TestUser();
            Assert.Equal(null, mapping.GetValue(user));
        }

        [Fact]
        public void TestStringMappingNullSet()
        {
            var mapping = new ColumnPropertyMapping<TestUser, string>("UserName", model => model.Name);
            var user = new TestUser();
            Assert.Equal(null, mapping.GetValue(user));
        }

        [Fact]
        public void TestStringMappingSet()
        {
            var mapping = new ColumnPropertyMapping<TestUser, string>("UserName", model => model.Name);
            var user = new TestUser();
            mapping.SetValue(user, "Pelle");
            Assert.Equal("Pelle", user.Name);
        }

        [Fact]
        public void InvalidMapping()
        {
            var mapping = new ColumnPropertyMapping<TestUser, DateTime>("CreatedAt", model => model.CreatedAt);
            var user = new TestUser();
            Assert.Throws<MappingException>(() => mapping.SetValue(user, 22));
        }

        [Fact]
        public void AutoToStringConversion()
        {
            var mapping = new ColumnPropertyMapping<TestUser, string>("UserName", model => model.Name);
            var user = new TestUser();

            var ctx = new ValueContext<TestUser> {ColumnValue = 22, Entity = user};
            var value = mapping.ConvertValue(ctx);
            Assert.Equal("22", value);
            mapping.SetValue(user, value);
        }

        [Fact]
        public void StringToDateConversion()
        {
            var mapping = new ColumnPropertyMapping<TestUser, DateTime>("CreatedAt", model => model.CreatedAt, ctx => ctx.ToDateTime());
            var user = new TestUser();

            var context = new ValueContext<TestUser> { ColumnValue = "2009-11-01", Entity = user };
            var value = mapping.ConvertValue(context);
            Assert.Equal(new DateTime(2009,11,01), value);
        }

    }
}
