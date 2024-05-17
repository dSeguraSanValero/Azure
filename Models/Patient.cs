namespace FisioSolution.Models;

public class Patient
{
   public int PatientId { get; set; }
   public string? Name { get; set; }
   public string? Dni { get; set; }
   public string? Password { get; set; }
   public DateTime BirthDate { get; set; }
   public decimal Weight { get; set; }
   public decimal Height { get; set; }
   public bool Insurance { get; set; }
   public List<Treatment> MyTreatments { get; set; }
   public List<Physio> AssignedPhysios { get; set; } = new List<Physio>();
   public static int PatientIdSeed { get; set; }

   public Patient()
   {
      MyTreatments = new List<Treatment>();
   }

   public Patient(string name, string dni, string password, DateTime birthDate, decimal weight, decimal height, bool insurance) 
   {
      PatientId = PatientIdSeed++;
      Name = name;
      Dni = dni;
      Password = password;
      BirthDate = birthDate;
      Weight = weight;
      Height = height;
      Insurance = insurance;
      MyTreatments = new List<Treatment>();
      AssignedPhysios = new List<Physio>();
   }

}