using FisioSolution.Models;

namespace FisioSolution.Data;
public interface IPatientRepository
{

    void AddPatient(Patient patient);
    void SaveChanges();
    Patient GetPatient(string dni);
    Dictionary<string, Patient> GetAllPatients();
    public void RemovePatient(Patient patient);

}