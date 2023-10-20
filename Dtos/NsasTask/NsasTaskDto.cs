using NsTask.Api.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace NsTask.Api.Dtos.NsasTask
{
    public class NsasTaskDto
    {
        public int Id { get; set; }
        [MaxLength(100)]
        public string Title { get; set; }
        [MaxLength(500)]
        public string Description { get; set; }
        public DateTime DueDate { get; set; }
        public Statuses Status { get; set; }
    }
}
