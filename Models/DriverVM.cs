using System.ComponentModel.DataAnnotations;

namespace L.I.S.A.Models
{
    public class DriverVM
    {
        [Key]
        public int Driver_Id { get; set; }
        public string First_Name { get; set; }
        public string Last_Name { get; set; }
        public string Birth_Country { get; set; }
        public DateTime Emp_Date { get; set; }
        public string Emp_Num { get; set; }
        public string Contact_Num { get; set; }
        public string Email { get; set; }

    }
}
