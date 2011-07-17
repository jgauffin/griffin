using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Griffin.Core.Data.SimpleMapper;
using Xunit;

namespace Griffin.Core.Tests.Data.Mappings
{
    public class EntityMappingTest
    {
        private EntityMapper<TestUser> _mapper = new EntityMapper<TestUser>();

        [Fact]
        public void TestPrimaryKey()
        {
            _mapper.PrimaryKey(model => model.Id, "Id");
            Assert.Equal("Id", _mapper.PrimaryKeys.First());
            Assert.Equal("Id", _mapper.GetColumnName("Id"));

            var record = new DataRecord();
            record.Fields.Add(new Tuple<string, object>("Id", 10));
            var user = new TestUser();
            _mapper.Map(user, record);
            Assert.Equal(10, user.Id);
        }

        [Fact]
        public void TestSimpleConverterForNull()
        {
            var record = new DataRecord(new Tuple<string, object>("Id", null));
            _mapper.Column(model => model.Id, "Id", ctx => int.Parse(ctx.ColumnValue.As<string>() ?? "0"));

            var user = new TestUser();
            _mapper.Map(user, record);
            Assert.Equal(0, user.Id);
        }

        [Fact]
        public void TestSimpleConverterForIntToString()
        {
            var record = new DataRecord(new Tuple<string, object>("Name", 42));
            _mapper.Column(model => model.Name, "Name", ctx => ctx.ColumnValue.As<string>());

            var user = new TestUser();
            _mapper.Map(user, record);
            Assert.Equal("42", user.Name);
        }

        [Fact]
        public void Test()
        {}

    }
}
