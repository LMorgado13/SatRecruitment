using Sat.Recruitment.Api.Models;
using Sat.Recruitment.Api.Validaciones;
using System.ComponentModel.DataAnnotations;

namespace Sat.Recruitment.Api.Clases
{
    public class ClsUser : ClsResultInfo
    {

        public string Name { get; set; }
 
        public string Email { get; set; }
  
        public string Address { get; set; }
    
        public string Phone { get; set; }
      
        public string UserType { get; set; }
       
        public decimal Money { get; set; }
    }
}
