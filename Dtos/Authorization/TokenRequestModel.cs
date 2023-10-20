using System.ComponentModel.DataAnnotations;

namespace NsTask.Api.Dtos.Authorization
{
    public class TokenRequestModel
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
