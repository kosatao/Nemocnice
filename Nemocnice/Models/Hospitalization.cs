namespace Nemocnice.Models
{
    public class Hospitalization
    {
        public int Id { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public int Procedure { get; set; }

        public int DoctorId { get; set; }
        public int PatientId { get; set; }

        public Patient Patient { get; set; }
        public Doctor Doctor { get; set; }
    }
}
