//using NsTask.Api.Domain.Enums;

//namespace NsTask.Api.Domain.ValueObjects
//{
//    public class Status : ValueObject
//    {
//        public int Id { get; set; }
//        public string Name { get; set; }

//        public static Status Created => new(1, "Created", "جديد");
//        public static Status Modified => new(2, "Modified", "معدل");
//        public static Status Ready => new(3, "Ready", "جاهز");
//        public static Status Published => new(4, "Published", "منشور");
//        public static Status Started => new(5, "Started", "بدأ");
//        public static Status Finished => new(6, "Finished", "إنتهى");

//        public static Status From(string name)
//        {
//            var status = SupportedStatuses.FirstOrDefault(a => a.NameEn == name);

//            if (status == null)
//            {
//                throw new UnSupportedStatusException(name);
//            }

//            return status;
//        }
//        public static Status From(int id)
//        {
//            var status = SupportedStatuses.FirstOrDefault(a => a.Id == id);

//            if (status == null)
//            {
//                throw new UnSupportedStatusException(id.ToString());
//            }

//            return status;
//        }
//        public static Status From(Statuses status)
//        {
//            return From((int)status);
//        }

//        public static implicit operator string(Status status)
//        {
//            return status.ToString();
//        }
//        public static implicit operator Statuses(Status status)
//        {
//            return (Statuses)status.Id;
//        }
//        public static implicit operator Status(Statuses status)
//        {
//            return From(status);
//        }
//        public static explicit operator Status(string name)
//        {
//            return From(name);
//        }
//        public static explicit operator Status(int id)
//        {
//            return From(id);
//        }

//        protected override IEnumerable<object> GetEqualityComponents()
//        {
//            yield return NameEn;
//        }
//        public static IEnumerable<Status> SupportedStatuses
//        {
//            get
//            {
//                yield return Created;
//                yield return Modified;
//                yield return Ready;
//                yield return Published;
//                yield return Started;
//                yield return Finished;
//            }
//        }
//        public override string ToString()
//        {
//            return NameEn.ToString();
//        }
//    }
//}
//}
