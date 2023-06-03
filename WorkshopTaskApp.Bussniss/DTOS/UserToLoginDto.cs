using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkshopTaskApp.Bussniss.DTOS
{
    public class UserToLoginDto
    {
        [DisplayName("User Name")]
        [Required(ErrorMessage = "*")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "*")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
