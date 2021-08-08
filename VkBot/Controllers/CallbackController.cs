using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using VkBot.BD;
using VkBot.Dtos;
using VkNet.Abstractions;
using VkNet.Model;
using VkNet.Model.Keyboard;
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

        private readonly Dictionary<string, string> questions = new Dictionary<string, string>
        {
            {
                "button1", "За ваши модификации дают бан?"
            },
            {
                "button2", "Как начать пользоваться D2MO?"
            },
            {
                "button3", "Как купить работу?"
            },
            {
                "adminConnectButton", "Связаться с администратором."
            },
        };

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
                        if (updates.Object.Body.ToLower() == "начать")
                        {
                            SendMenu(updates);

                            CommunicatingWithAdmin.communicatingWithAdminList.Remove(updates.Object.User_id);
                        }
                        else if (CommunicatingWithAdmin.communicatingWithAdminList.Contains(updates.Object.User_id))
                        {
                            break;
                        }
                        else
                        {
                            SendResponse(updates);
                        }

                        break;
                    }
            }

            return Ok("ok");
        }

        private void SendMenu(UpdatesDto updates)
        {
            var keyboard = new VkNet.Model.Keyboard.KeyboardBuilder("text");
            keyboard.SetInline(true);

            foreach (var question in questions)
            {
                keyboard.AddButton(question.Value, question.Key);
                keyboard.AddLine();
            }

            SendMessage(updates.Object.User_id, updates.GroupId, "Выбери вопрос", keyboard.Build());
        }

        private void SendResponse(UpdatesDto updates)
        {
            var keyboard = new VkNet.Model.Keyboard.KeyboardBuilder("text");
            keyboard.SetInline(true);

            switch (updates.Object.Body)
            {
                case "За ваши модификации дают бан?":
                    {
                        SendMessage(updates.Object.User_id, updates.GroupId, "Все моды сделаны посредством софта, которое предоставляют Valve, за это невозможно получить бан. Абсолютно по такому же принципу создатели сетов из Workshop подгружают свои работы в клиент игры. Мы не вмешиваемся в данные, которые отправляются на сервера Valve.");

                        break;
                    }
                case "Как начать пользоваться D2MO?":
                    {
                        SendMessage(updates.Object.User_id, updates.GroupId, @"1) Вам необходимо скачать программу из статьи: (https://vk.com/@amir4anmods-taverna).
  2) Следом перейти в обсуждения и установить по инструкций: (https://vk.com/topic-178507216_39493678).
  3) Установка модов указана здесь: (https://vk.com/topic-178507216_39493678).
  4) Решение всех проблем указано здесь: (https://vk.com/topic-178507216_39493681).");

                        break;
                    }
                case "Как купить работу?":
                    {
                        SendMessage(updates.Object.User_id, updates.GroupId, @"1) Вы можете оформить подписку Donuts и получить доступ к эксклюзивной коллекций работ нашего производства. Вся информация касаемо платформы Donuts представлена в этой статье: (https://vk.com/@donut-faq-dlya-donov). Подписка оформляется по этой ссылке: (https://vk.com/amir4anmods?source=description&w=donut_payment-178507216).
  2) Так же в товарах группы присутствуют работы, которые продаются поштучно.
  3) Заказать эксклюзивную сборку можно, связавшись с администратором группы.");

                        break;
                    }
                case "Связаться с администратором.":
                    {
                        CommunicatingWithAdmin.communicatingWithAdminList.Add(updates.Object.User_id);
                        PingAdmin(updates);
                        SendMessage(updates.Object.User_id, updates.GroupId, "Админ ответит в скором времени :)");

                        break;
                    }
                default:
                    {
                        SendMessage(updates.Object.User_id, updates.GroupId, "Нажми на одну из кнопок, чтобы получить ответ :)");
                        SendMenu(updates);
                        break;
                    }
            }

            //_vkApi.Messages.MarkAsImportantConversation(updates.GroupId, false);
        }

        private void PingAdmin(UpdatesDto updates)
        {
            _vkApi.Messages.Send(new MessagesSendParams
            {
                UserId = 24958821,
                RandomId = Extensions.GenerateRandomId(),
                PeerId = updates.GroupId,
                Message = "Тебя пингует какой то долбаёб, чекни группу",
                //Forward = new MessageForward()
                //{
                //    OwnerId = updates.Object.User_id,
                //    PeerId = updates.GroupId,
                //    ConversationMessageIds =
                //    new long[]
                //    { updates.Object.Id },
                //    MessageIds = new long[]
                //    { updates.Object.Id }
                //}
            });
        }

        private void SendMessage(long userId, long groupId, string message, MessageKeyboard keyboard = null)
        {
            _vkApi.Messages.Send(new MessagesSendParams
            {
                UserId = userId,
                RandomId = Extensions.GenerateRandomId(),
                PeerId = groupId,
                Message = message,
                Keyboard = keyboard,
            });
        }
    }
}
