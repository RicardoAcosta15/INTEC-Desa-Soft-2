using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUD_Cédulas.Models
{
    public class User
    {
        public int Id { get; set; }
        public long IdNumber { get; set; }
        public string ProfilePicture { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string BirthPlace { get; set; }
        public DateTime DOB { get; set; }
        public string Country { get; set; }
        public char Sex { get; set; }
        public string BloodType { get; set; }
        public string Occupation { get; set; }
        public string CivilState { get; set; }
        public DateTime ExpDate { get; set; }
        public DateTime CreatedDate { get; set; }

    }
}
