using Cygnus.Domain.Models;

namespace Cygnus.ConsoleApp.Tasks
{
    public interface ITask
    {
        Task ProcessAsync(EntityModel entity);
    }
}
