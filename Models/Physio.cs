namespace FisioSolution.Models;
public class Physio
{
   public int PhysioId { get; set; }
   public string? Name { get; set; }
   public int RegistrationNumber { get; set; }
   public string? Password { get; set; }
   public bool Availeable { get; set; }
   public TimeSpan OpeningTime { get; set; }
   public TimeSpan ClosingTime { get; set; }
   public decimal Price { get; set; }
   public List<Treatment> MyTreatments { get; set; }
   public static int PhysioIdSeed { get; set; }


   public Physio()
   {
      MyTreatments = new List<Treatment>();
   }

   public Physio(string name, int registrationNumber, string password, bool availeable, TimeSpan openingTime, TimeSpan closingTime, decimal price) 
   {
      PhysioId = PhysioIdSeed++;
      Name = name;
      RegistrationNumber = registrationNumber;
      Password = password;
      Availeable = availeable;
      OpeningTime = openingTime;
      ClosingTime = closingTime;
      Price = price;
      MyTreatments = new List<Treatment>();
   }

}
