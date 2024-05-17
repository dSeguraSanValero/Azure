namespace FisioSolution.Models;
public class Treatment
{
   public int TreatmentId { get; set; }
   public int PhysioId { get; set; }
   public int PatientId { get; set; }
   public string? TreatmentCause { get; set; }
   public DateTime TreatmentDate { get; set; }
   public static int TreatmentIdSeed { get; set; }
   public Patient Patient { get; set; }
   public Physio Physio { get; set; }
   public bool MoreSessionsNeeded { get; set; }

   public Treatment() {
      
   }

   public Treatment(int physioId, int patientId, string treatmentCause, DateTime treatmentDate, bool moreSessionsNeeded ) 
   {
      TreatmentId = TreatmentIdSeed++;
      PhysioId = physioId;
      PatientId = patientId;
      TreatmentCause = treatmentCause;
      TreatmentDate = treatmentDate;
      MoreSessionsNeeded = moreSessionsNeeded; 
   }
}