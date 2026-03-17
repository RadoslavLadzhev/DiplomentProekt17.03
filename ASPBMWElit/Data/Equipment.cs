using System.ComponentModel.DataAnnotations.Schema;

namespace ASPBMWElit.Data
{
    public class Equipment
    {
        public int Id { get; set; } //PK
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
      //  [Column(TypeName = "decimal(10,2)")]
        public double Price { get; set; }
        public int EquipmentTypeID { get; set; }//Fk
        public ICollection<Car> Cars { get; set; }
        //m;1
        //1;m
        public EquipmentType EquipmentTypes { get; set; }

    }
}
