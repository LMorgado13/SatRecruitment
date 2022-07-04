using Sat.Recruitment.Api.Validaciones;
using System.ComponentModel.DataAnnotations;

namespace Sat.Recruitment.Api.DTOs
{
    public class ClsCreateUsersDTO
    {
        [PrimeraLetraMayuscula]
        [StringLength(maximumLength: 20)]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public string Name { get; set; }
        [EmailAddress]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public string Email { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [StringLength(maximumLength: 250)]
        public string Address { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [Phone]
        public string Phone { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public string UserType { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public decimal Money { get; set; }

        public ClsCreateUsersDTO()
        {

            Name = "";
            Email = "";
            Address = "";
            Phone = "";
            UserType = "";
            Money = 0;
        }
    }
}
