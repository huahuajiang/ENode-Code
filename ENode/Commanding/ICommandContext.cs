using ENode.Domain;
using ENode.Messageing;
using System.Threading.Tasks;

namespace ENode.Commanding
{
    public interface ICommandContext
    {
        void Add(IAggregateRoot aggregateRoot);

        Task AddAsync(IAggregateRoot aggregateRoot);

        Task<T> GetAsync<T>(object id, bool firstFromCache = true) where T : class, IAggregateRoot;

        void SetResult(string result);

        string GetResult();

        void SetApplicationMessage(IApplicationMessage applicationMessage);

        IApplicationMessage GetApplicationMessage();
    }
}
