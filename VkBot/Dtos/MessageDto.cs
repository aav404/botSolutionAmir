using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VkBot.Dtos
{
    public class MessageDto
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("date")]
        public long Date { get; set; }

        [JsonProperty("out")]
        public long Out { get; set; }

        [JsonProperty("user_id")]
        public long UserId { get; set; }

        [JsonProperty("read_state")]
        public long ReadState { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("body")]
        public string Body { get; set; }

        [JsonProperty("random_id")]
        public long RandomId { get; set; }

        [JsonProperty("owner_ids")]
        public object[] OwnerIds { get; set; }

        [JsonProperty("from_id")]
        public long FromId { get; set; }
    }
}
