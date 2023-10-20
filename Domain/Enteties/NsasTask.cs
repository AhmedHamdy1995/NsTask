using NsTask.Api.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace NsTask.Api.Domain.Enteties
{
    public class NsasTask
    {
        public NsasTask(int id, string title, string description, DateTime dueDate, Statuses status)
        {
            Id = id;
            Title = title;
            Description = description;
            DueDate = dueDate;
            Status = status;
        }

        [Key]
        public int Id { get; set; }
        [MaxLength(100)]
        public string Title { get; set; }
        [MaxLength(500)]
        public string Description { get; set; }
        public DateTime DueDate { get; set; }
        public Statuses Status { get; set; }
    }
}
