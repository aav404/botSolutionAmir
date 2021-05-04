using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using VkBot.Dtos;
using VkNet.Model;

namespace VkBot.Controllers
{
    [Serializable]
    public class UpdatesDto
    {
        /// <summary>
        /// Тип события
        /// </summary>
        [JsonProperty("type")]
        public string Type { get; set; }

        /// <summary>
        /// Объект, инициировавший событие
        /// Структура объекта зависит от типа уведомления
        /// </summary>
        [JsonProperty("object")]
        public MessageDto Object { get; set; }

        /// <summary>
        /// ID сообщества, в котором произошло событие
        /// </summary>
        [JsonProperty("group_id")]
        public long GroupId { get; set; }

        /// <summary>
        /// Секретный ключ. Передается с каждым уведомлением от сервера
        /// </summary>
        [JsonProperty("secret")]
        public string Secret { get; set; }
    }
}