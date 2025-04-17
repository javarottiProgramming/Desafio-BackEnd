using Desafio_BackEnd.Domain.Events;
using MassTransit;

namespace Desafio_BackEnd.Workers.MotorcycleWorker
{
    public class MotorcycleNotificationConsumer : IConsumer<MotorcycleNotification>
    {
        public Task Consume(ConsumeContext<MotorcycleNotification> context)
        {
            var message = context.Message;
            Console.WriteLine($"{message.Id} Notification Received");

            return Task.CompletedTask;
        }
    }
}