using Desafio_BackEnd.Domain.Events;
using MassTransit;

namespace Desafio_BackEnd.Workers.MotorcycleWorker
{
    public class MotorcycleCreatedConsumer : IConsumer<MotorcycleCreatedEvent>
    {
        public Task Consume(ConsumeContext<MotorcycleCreatedEvent> context)
        {
            var message = context.Message;
            Console.WriteLine($"Motorcycle Created: {message.Id}, {message.Model}," +
                $" {message.FabricationYear}");

            if (message.FabricationYear == 2024)
            {
                Console.WriteLine($"Registration Notification Send");
                context.Publish<MotorcycleNotification>(new
                {
                    message.Id,
                    message.FabricationYear
                });
            }

            return Task.CompletedTask;
        }
    }
}