using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookiesAuthApp.WEB.Common
{
    public class Response<T>
    {
        public string? StatusCode { get; set; }
        public string? Message { get; set; }
        public T? Data { get; set; }
    }
}
