using System;
using Newtonsoft.Json;

namespace GajGamesServiceRouter.Infrastructure.Dtos
{
    public class ErrorDetail
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }

        public override string ToString()
        {
            return JsonConvert.ToString(this);
        }
    }
}
