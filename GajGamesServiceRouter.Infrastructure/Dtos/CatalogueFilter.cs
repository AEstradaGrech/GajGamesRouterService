using System;
using GajGamesServiceRouter.Infrastructure.Enums;
using Newtonsoft.Json;

namespace GajGamesServiceRouter.Infrastructure.Dtos
{
    public class CatalogueFilter
    {
        public string Title { get; set; }
        public string Studio { get; set; }
        public int? Price { get; set; }
        public PEGI? PEGI { get; set; }
        public string Genre { get; set; }
        [JsonConverter(typeof(JsonDateConverter), "dd-MM-yyyy")]
        public DateTime? MinReleaseDate { get; set; }
        [JsonConverter(typeof(JsonDateConverter), "dd-MM-yyyy")]
        public DateTime? MaxReleaseDate { get; set; }

        public int SortingParameter { get; set; }
        public bool IsOrderByDescending { get; set; }
        public bool IsGreaterThanPrice { get; set; }
        public bool IsGreaterThanPEGI { get; set; }
        public int ChunkSize { get; set; }
        public int Skip { get; set; }
    }
}
