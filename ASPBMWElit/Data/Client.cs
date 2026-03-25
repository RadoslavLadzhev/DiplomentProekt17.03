using Microsoft.AspNetCore.Identity;

namespace ASPBMWElit.Data
{
    public class Client : IdentityUser
    {
       // public int Id { get; set; }//PK
        public string FirstName { get; set; }
        public string LastName { get; set; }
        //public string Address { get; set; }
        //// 1 : M
        public ICollection<Inquiring> Inquirings { get; set; }

    }
}
