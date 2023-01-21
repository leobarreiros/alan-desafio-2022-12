using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public struct ValueResult<T>
    {
        public bool Failed { get; private set; }
        public string Error { get; private set; }
        public string Tag { get; private set; }
        public T Value { get; private set; }

        public static ValueResult<T> Success()
        {
            return default(ValueResult<T>);
        }

        public static ValueResult<T> Success(T value)
        {
            var result = default(ValueResult<T>);
            result.Value = value;
            return result;
        }

        public static ValueResult<T> Faliure(string error, string tag = "")
        {
            var result = default(ValueResult<T>);
            result.Failed = true;
            result.Tag = tag;
            result.Error = error;
            return result;
        }
    }

    public struct ValueResult
    {
        public bool Failed { get; private set; }
        public string Error { get; private set; }
        public string Tag { get; private set; }

        public static ValueResult Success()
        {
            return default(ValueResult);
        }

        public static ValueResult Faliure(string error, string tag = "")
        {
            var result = default(ValueResult);
            result.Failed = true;
            result.Tag = tag;
            result.Error = error;
            return result;
        }
    }
}
