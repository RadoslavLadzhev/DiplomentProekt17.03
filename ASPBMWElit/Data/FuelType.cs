namespace ASPBMWElit.Data
{
    public class FuelType
    {
        public int Id { get; set; } //PK
        public string Name { get; set; }
        public DateTime DateRegistered { get; set; }
        public ICollection<Car> Cars { get; set; }
        //m:1
    }
}
