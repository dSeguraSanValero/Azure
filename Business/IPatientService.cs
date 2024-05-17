using FisioSolution.Models;

namespace FisioSolution.Business;

public interface IPatientService
{
    public void RegisterPatient(string name, string dni, string password, DateTime birthDate, decimal weight, decimal height, bool insurance);
    public bool CheckPatientExist(string dni);
    public bool CheckLoginPatient(string dni, string pasword);
    public Patient GetPatientByDni(string dni);
    public void UpdatePatientTreatments(string dni, List<Treatment> treatments);
    public void DeletePatient(Patient patient);
    public void UpdatePatientData(Patient patient, string newName, string newPassword, decimal newWeight, decimal newHeight, bool newInsurance);
}