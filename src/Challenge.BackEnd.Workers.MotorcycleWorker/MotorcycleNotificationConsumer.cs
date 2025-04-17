using Challenge.BackEnd.Core.Domain.Events;
using MassTransit;

namespace Challenge.BackEnd.Worker.MotorcycleWorker
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