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


            //TODO Criar notificacao 2024

            return Task.CompletedTask;
        }
    }
}
