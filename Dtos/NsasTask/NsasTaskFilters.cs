using NsTask.Api.Domain.Enums;

namespace NsTask.Api.Dtos.NsasTask
{
    public class NsasTaskFilters
    {
        public NsasTaskFilters(string? stringfilters = null, Statuses? status = null)
        {
            this.stringfilters = stringfilters;
            this.status = status;   
        }
        public NsasTaskFilters():this(string.Empty,null)
        {
        }

        public string? stringfilters { get; set; }
        public Statuses? status { get; set; }
    }
}
