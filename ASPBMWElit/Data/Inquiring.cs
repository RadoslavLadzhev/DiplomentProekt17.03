namespace ASPBMWElit.Data
{
    public class Inquiring
    {
        public int? Id { get; set; }//PK
        public string? ClientId { get; set; }
        public Client Client { get; set; }
        public int CarId { get; set; }//Fk
        public string? Message  { get; set; }
        public DateTime InspectionDate { get; set; }
        public DateTime CreateAt { get; set; }
        //M:1
        public Car Cars { get; set; }
        
    }
}
