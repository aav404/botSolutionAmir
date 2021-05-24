using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace VkBot.Dtos
{
    [DataContract]
    public class MessageDto
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("date")]
        public long Date { get; set; }

        [JsonProperty("out")]
        public long Out { get; set; }

        [JsonProperty("user_id")]
        public long User_id { get; set; }

        [JsonProperty("read_state")]
        public long Read_State { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("body")]
        public string Body { get; set; }

        [JsonProperty("owner_ids")]
        public object[] Owner_Ids { get; set; }
    }
}
