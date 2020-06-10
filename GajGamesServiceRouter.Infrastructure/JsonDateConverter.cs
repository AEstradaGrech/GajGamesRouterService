using System;
using Newtonsoft.Json.Converters;

namespace GajGamesServiceRouter.Infrastructure
{
    public class JsonDateConverter : IsoDateTimeConverter
    {
        public JsonDateConverter(string format)
        {
            DateTimeFormat = format;
        }
    }
}
