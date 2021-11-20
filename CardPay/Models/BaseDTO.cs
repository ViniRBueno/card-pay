using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CardPay.Models
{
    public class BaseDTO<T>
    {
        public static BaseDTO<string> Error(string message)
        {
            return new BaseDTO<string>(false, message, null);
        }
        public static BaseDTO<T> Success(string message, T data)
        {
            return new BaseDTO<T>(true, message, data);
        }
        public BaseDTO(bool success, string message, T data)
        {
            this.success = success;
            this.message = message;
            this.data = data;
        }

        public bool success { get; set; }
        public string message { get; set; }
        public T data { get; set; }
    }
}
