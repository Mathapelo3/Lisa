namespace L.I.S.A.Models
{
    public class CaseVM
    {
        public long Casing_Id { get; set; }
        public string CaseNumber { get; set; }
        public string Casing_Type { get; set; } = null!;
        public string Casing_Desc { get; set; } = null!;
        public DateTime Start_Date { get; set; }
        public DateTime End_Date { get; set; }
        public string Status { get; set; } = null!;
        public string Resolution { get; set; } = null!;
        public string First_name { get; set; }
        public string Last_name { get; set;}
    }
}
