namespace ASPBMWElit.Data
{
    public class EquipmentType
    {
        public int Id { get; set; }//PK
        public string Name { get; set; }
        /* interior,exterior,farove,digitalno*/
        //1:M
        public Equipment Equipments { get; set; }
    }
}
