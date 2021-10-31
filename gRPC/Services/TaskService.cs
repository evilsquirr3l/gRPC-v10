using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Grpc.Core;
using Microsoft.Extensions.Logging;

namespace gRPC
{
    public class TaskService : Tasks.TasksBase
    {
        private List<string> _list = new List<string>
        {
            "ФТП – получение списка файлов + передача файла с сервера на клиент.",
            @"Телнет. Создать программу, которая соединяется с указанным сервером
            по указанному порту и производит удаленное выполнение команд.",
            "Сетевая игра – «самый умный» дается вопросы и варианты ответа. Считается кто больше раз правильно ответил.",
            "Защищенная (шифрование XOR) передача сообщений по сети.",
            "Сетевая программа резервирования билетов.",
            "Выбор обеда из меню (результат заказа записывается в файл).",
            "Сетевой аукцион. Продается предмет участникам дается по 30 секунд, чтобы поднять ставку.",
            "Сетевая регистрация на порядок сдачи лабораторных работ.",
            "Сетевой выбор варианта лабораторной работы. После того, как кто-то выбрал себе задание оно не отображается другим.",
            "Тестирование по сети. Дается вопрос и варианты ответа."
        };

        private string GetTask(int index)
        {
            var task = _list[index];
            _list.RemoveAt(index);

            return task;
        }

        public override Task<Reply> SendRequest(Request request, ServerCallContext context)
        {
            var task = GetTask(request.RequestMessage);
            
            return Task.FromResult(new Reply
            {
                ReplyMessage = $"You took variant #{request.RequestMessage} and the task {task}"
            });
        }
    }
}