using NsTask.Api.Domain.Enteties;

namespace NsTask.Api.Bl.Interfaces
{
    public interface INsasTaskRepository
    {
        IEnumerable<NsasTask> GetNsasTasks();
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
