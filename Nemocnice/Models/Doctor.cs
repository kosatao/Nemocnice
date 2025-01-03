namespace Nemocnice.Models
{
    public class Doctor
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }    
        public int Specialization {  get; set; }
        
        public Hospitalization Hospitalization { get; set; }
    }
}
