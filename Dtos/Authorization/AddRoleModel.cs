using System.ComponentModel.DataAnnotations;

namespace NsTask.Api.Dtos.Authorization
{
    public class AddRoleModel
    {
        public string UserId { get; set; }
        public string Role { get; set; }
    }
}
