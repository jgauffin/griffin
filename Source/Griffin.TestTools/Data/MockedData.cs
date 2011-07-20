using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Moq;

namespace Griffin.TestTools.Data
{
    public static class MockedData
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="objectsToEmulate"></param>
        /// <returns></returns>
        /// <example>
        /// 
        /// </example>
        public static IDataReader GetDataReader(this IEnumerable<object> objectsToEmulate)
        {
            Mock<IDataReader> mock = new Mock<IDataReader>();
            int count = -1;

// ReSharper disable PossibleMultipleEnumeration

            var itemCount = objectsToEmulate.Count();
            mock.Setup(x => x.Read())
                .Returns(count < itemCount - 1) // Returns true until there are no more items to read.
                .Callback(() => count++); // After each time 'Read' is called, it will advance to the next item in the list of objects.

            // Here you can optionally set up various values and/or strings to emulate your columns
            mock.Setup(x => x[0])
                .Returns(new Queue<object>(objectsToEmulate).Dequeue);

// ReSharper restore PossibleMultipleEnumeration

            return mock.Object;
        }
    }
}
