using Microsoft.EntityFrameworkCore;
using NSE.Core.DomainObjects;
using System.Linq;
using System.Threading.Tasks;

namespace NSE.Core.Mediator
{
    public static class MediatorExtension
    {
        public static async Task PublicarEventos<T>( this IMediatorHandler mediator, T ctx ) where T : DbContext
        {
            var domainEntities = ctx.ChangeTracker
                .Entries<Entity>()
                .Where(x => x.Entity.Notificacoes != null && x.Entity.Notificacoes.Any());

            var domainEvents = domainEntities
                .SelectMany(x => x.Entity.Notificacoes)
                .ToList();

            domainEntities.ToList()
                .ForEach(entity => entity.Entity.LimparEventos());

            var tasks = domainEvents
                .Select(async ( domainEvent ) =>
                {
                    await mediator.PublicarEventos(domainEvent);
                });

            await Task.WhenAll(tasks);
        }
    }
}

