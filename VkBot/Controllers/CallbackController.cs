using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using VkBot.Dtos;
using VkNet.Abstractions;
using VkNet.Model;
using VkNet.Model.RequestParams;
using VkNet.Utils;

namespace VkBot.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CallbackController : ControllerBase
    {
        /// <summary>
        /// Конфигурация приложения
        /// </summary>
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        /// <summary>
        /// Апи Вк
        /// </summary>
        private readonly IVkApi _vkApi;

        public CallbackController(IVkApi vkApi, IConfiguration configuration, IMapper mapper)
        {
            _vkApi = vkApi;
            _configuration = configuration;
            _mapper = mapper;
        }

        [HttpPost]
        public IActionResult Callback([FromBody] UpdatesDto updates)
        {
            // Тип события
            switch (updates.Type)
            {
                // Ключ-подтверждение
                case "confirmation":
                    {
                        return Ok(_configuration["Config:Confirmation"]);
                    }

                // Новое сообщение
                case "message_new":
                    {
                        var keyboard = new VkNet.Model.Keyboard.KeyboardBuilder("text"); 
                        keyboard.AddButton("Тестовая кнопка", "test_1"); 
                        keyboard.SetInline(false);

                        _vkApi.Messages.Send(new MessagesSendParams
                        {
                            UserId = updates.Object.User_id,
                            RandomId = Extensions.GenerateRandomId(),
                            PeerId = updates.GroupId,
                            Message = "тест бота",
                            Keyboard = keyboard.Build(),
                        });

                        break;
                    }
            }

            return Ok("ok");
        }

        [HttpGet]
        public IActionResult GetLogs()
        {
            return Ok("Работает");
        }
    }
}
