using NsTask.Api.Bl.Interfaces;
using NsTask.Api.Data;
using NsTask.Api.Domain.Enteties;

namespace NsTask.Api.Bl.Services
{
    public class NsasTaskRepository : INsasTaskRepository
    {
        private readonly ApplicationDbContext db;

        public NsasTaskRepository(ApplicationDbContext db)
        {
            this.db = db;
        }

        public bool CheckNsasTaskExists(int Id)
        {
            return db.nsasTasks.Any(bo => bo.Id == Id);
        }

        public bool CheckNsasTaskExists(string Title)
        {
            return db.nsasTasks.Any(bo => bo.Title.ToLower().Equals(Title.ToLower()));
        }

        public bool CreateNsasTask(NsasTask NsasTask)
        {
            db.nsasTasks.Add(NsasTask);
            return Save();
        }

        public bool DeleteNsasTask(NsasTask NsasTask)
        {
            db.nsasTasks.Remove(NsasTask);
            return Save();
        }

        public NsasTask GetNsasTask(int Id)
        {
            return db.nsasTasks.Find(Id);
        }

        public NsasTask GetNsasTask(string Title)
        {
            return db.nsasTasks.FirstOrDefault(b => b.Title.ToLower().Equals(Title.ToLower()));
        }

        public IEnumerable<NsasTask> GetNsasTasks()
        {
            return db.nsasTasks.ToList();
        }

        public bool Save()
        {
            return db.SaveChanges() >= 0 ? true : false;
        }

        public bool UpdateNsasTask(NsasTask NsasTask)
        {
            db.nsasTasks.Update(NsasTask);
            return Save();
        }
    }
}
