using NsTask.Api.Domain.Enteties;
using NsTask.Api.Dtos.NsasTask;
using System.Linq.Expressions;

namespace NsTask.Api.Bl.Interfaces
{
    public interface INsasTaskRepository
    {
        IEnumerable<NsasTask> GetNsasTasks();
        IQueryable<NsasTask> FilterTasks(NsasTaskFilters? filters = null);
        NsasTask GetNsasTask(int Id);
        NsasTask GetNsasTask(string Name);
        bool CheckNsasTaskExists(int Id);
        bool CheckNsasTaskExists(string Name);
        bool CreateNsasTask(NsasTask NsasTask);
        bool UpdateNsasTask(NsasTask NsasTask);
        bool DeleteNsasTask(NsasTask NsasTask);
        bool Save();
    }
}
