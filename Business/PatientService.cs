using FisioSolution.Data;
using FisioSolution.Models;

namespace FisioSolution.Business;

public class PatientService : IPatientService
{
    private readonly IPatientRepository _repository;

    public PatientService(IPatientRepository repository)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    public void RegisterPatient(string name, string dni, string password, DateTime birthDate, decimal weight, decimal height, bool insurance)
    {
        try 
        {
            Patient patient = new(name, dni, password, birthDate, weight, height, insurance);
            _repository.AddPatient(patient);
            _repository.SaveChanges();
        } 
        catch (Exception e)
        {
            throw new Exception("Ha ocurrido un error al registrar el usuario", e);
        }
    }

    public bool CheckPatientExist(string dni)
    {
        try
        {
            foreach (var patient in _repository.GetAllPatients().Values)
            {
                if (patient.Dni.Equals(dni))
                {
                    return true;
                }
            }

            return false;
        }
        catch (Exception e)
        {
            throw new Exception("Ha ocurrido un error al comprobar el usuario", e);
        }
    }

    public bool CheckLoginPatient(string dni, string pasword)
    {
        try
        {
            foreach (var patient in _repository.GetAllPatients().Values)
            {
                if (patient.Dni.Equals(dni, StringComparison.OrdinalIgnoreCase) &&
                    patient.Password.Equals(pasword))
                {
                    return true;
                }
            }

            return false;
        }
        catch (Exception e)
        {
            throw new Exception("Ha ocurrido un error al comprobar el usuario", e);
        }
    }

    public Patient GetPatientByDni(string dni)
    {
        try
        {
            foreach (var patient in _repository.GetAllPatients().Values)
            {
                if (patient.Dni.Equals(dni))
                {
                    return patient;
                }
            }

            return null;
        }
        catch (Exception e)
        {
            throw new Exception("Ha ocurrido un error al obtener el usuario", e);
        }
    }

    public void UpdatePatientTreatments(string dni, List<Treatment> treatments)
    {
        try
        {
            Patient patient = GetPatientByDni(dni);

            if (patient != null)
            {

                patient.MyTreatments = treatments;
                _repository.SaveChanges();
            }
            else
            {
                throw new Exception("No se encontró ningún paciente con el DNI proporcionado.");
            }
        }
        catch (Exception e)
        {
            throw new Exception("Ha ocurrido un error al actualizar los tratamientos del paciente.", e);
        }
    }

    public void DeletePatient(Patient patient)
    {
        try
        {
            _repository.RemovePatient(patient);
            _repository.SaveChanges();
        }
        catch (Exception e)
        {
            throw new Exception("Ha ocurrido un error al borrar el usuario", e);
        }
    }

    public void UpdatePatientData(Patient patient, string newName, string newPassword, decimal newWeight, decimal newHeight, bool newInsurance)
    {
        try
        {
            if (patient != null)
            {
                patient.Name = newName;
                patient.Password = newPassword;
                patient.Weight = newWeight;
                patient.Height = newHeight;
                patient.Insurance = newInsurance;

                _repository.SaveChanges();
            }
            else
            {
                throw new ArgumentNullException(nameof(patient), "El paciente proporcionado es nulo.");
            }
        }
        catch (Exception e)
        {
            throw new Exception("Ha ocurrido un error al actualizar los datos del paciente.", e);
        }
    }


}