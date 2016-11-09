using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestGraphApi.TestData
{
    public abstract class AbstractTestData<T>
    {
        public T[] GetTestData(int amount)
        {
            return Generate().Take(amount).ToArray();
        }

        protected abstract IEnumerable<T> Generate();
    }
}