using System.ComponentModel.DataAnnotations;

namespace MODELS
{
    public class LoggedUser
    {
        [Required]
        public string name { get; set; }

        [Required]
        public string role { get; set; }
    }
}