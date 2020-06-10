using System;
using GajGamesServiceRouter.Infrastructure.Enums;
using Newtonsoft.Json;

namespace GajGamesServiceRouter.Infrastructure.Dtos
{
    public class ImageDto
    {
        public string ImgBase64 { get; set; }
        public string ImgName { get; set; }
        [JsonConverter(typeof(JsonDateConverter), "dd-MM-yyyy")]
        public DateTime CreationDate { get; set; }
        public ImgCategory Category { get; set; }
        public Guid UserGuid { get; set; }
    }
}
