namespace ASPBMWElit.Data
{
    public enum TypeAuto
    {
        Sedan,
        Coupe,
        Cabriolet,
        SUV,
        Hatchback,
        Wagon
    }
    public class Car
    {
        public int Id { get; set; } //PK
        public int CatalogNumber { get; set; }
        public string Model { get; set; }
        public int EquipmentId { get; set; }//FK
        public string? Description { get; set; }
        public TypeAuto CarType { get; set; }
        public int FuelTypeId { get; set; }
        public int HorsePower { get; set; }
        public double Acceleration { get; set; }
        public string ImageUrl { get; set; }
        public double Price { get; set; }
        public int CreatedAt { get; set; }
        //1:M
        public ICollection<Inquiring> Inquirings { get; set; }

        public FuelType FuelTypes { get; set; }

        public Equipment Equipments { get; set; }
    }
}
