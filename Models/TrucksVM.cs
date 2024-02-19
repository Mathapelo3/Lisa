using System.ComponentModel.DataAnnotations;

namespace L.I.S.A.Models
{
    public class TrucksVM
    {
        [Key]
        public long Truck_Id { get; set; }
        public string Make { get; set; } = null!;
        public string Vin_Num { get; set; } = null!;
        public string Trailer1_Reg { get; set; } = null!;
        public string Trailer2_Reg { get; set; } = null!;
        public string Company { get; set; } = null!;
        public string Truck_Condition { get; set; } = null!;
        public string Truck_Status { get; set; } = null!;

        public byte[] Truck_Img { get; set; } = null!;


        [DataType(DataType.Upload)]
        public IFormFile TruckImageFile { get; set; }
    }
}
