using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Griffin.Core.Tests.Data
{
    public class DataRecord : IDataRecord
    {
        private List<Tuple<string,object>>  _fields = new List<Tuple<string, object>>();

        public DataRecord()
        {
            
        }

        public DataRecord(params Tuple<string,object>[] items)
        {
            _fields = new List<Tuple<string, object>>(items);
        }

        public string GetName(int i)
        {
            return _fields[i].Item1;
        }

        public string GetDataTypeName(int i)
        {
            return _fields[i].Item2.GetType().Name;
        }

        public Type GetFieldType(int i)
        {
            return _fields[i].Item2.GetType();
        }

        public object GetValue(int i)
        {
            return _fields[i].Item2;
        }

        public int GetValues(object[] values)
        {
            for (int i = 0; i < _fields.Count; i++)
            {
                values[i] = _fields[i].Item2;
            }

            return _fields.Count;
        }

        public int GetOrdinal(string name)
        {
            for (int i = 0; i < _fields.Count; i++)
            {
                var field = _fields[i];
                if (field.Item1.Equals(name))
                    return i;
            }

            return -1;
        }

        public bool GetBoolean(int i)
        {
            throw new NotImplementedException();
        }

        public byte GetByte(int i)
        {
            throw new NotImplementedException();
        }

        public long GetBytes(int i, long fieldOffset, byte[] buffer, int bufferoffset, int length)
        {
            throw new NotImplementedException();
        }

        public char GetChar(int i)
        {
            throw new NotImplementedException();
        }

        public long GetChars(int i, long fieldoffset, char[] buffer, int bufferoffset, int length)
        {
            throw new NotImplementedException();
        }

        public Guid GetGuid(int i)
        {
            throw new NotImplementedException();
        }

        public short GetInt16(int i)
        {
            throw new NotImplementedException();
        }

        public int GetInt32(int i)
        {
            throw new NotImplementedException();
        }

        public long GetInt64(int i)
        {
            throw new NotImplementedException();
        }

        public float GetFloat(int i)
        {
            throw new NotImplementedException();
        }

        public double GetDouble(int i)
        {
            throw new NotImplementedException();
        }

        public string GetString(int i)
        {
            throw new NotImplementedException();
        }

        public decimal GetDecimal(int i)
        {
            throw new NotImplementedException();
        }

        public DateTime GetDateTime(int i)
        {
            throw new NotImplementedException();
        }

        public IDataReader GetData(int i)
        {
            throw new NotImplementedException();
        }

        public bool IsDBNull(int i)
        {
            return _fields[i].Item2 == null || _fields[i].Item2 == DBNull.Value;
        }

        public int FieldCount { get { return _fields.Count; } }

        public List<Tuple<string, object>> Fields
        {
            get { return _fields; }
        }

        object IDataRecord.this[int i]
        {
            get { return _fields[i].Item2; }
        }

        object IDataRecord.this[string name]
        {
            get { return _fields.First(item => item.Item1 == name).Item2;}
        }
    }
}
