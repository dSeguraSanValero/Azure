using FisioSolution.Data;
using FisioSolution.Models;


namespace FisioSolution.Business;
public class PhysioService : IPhysioService
{

    private readonly IPhysioRepository _repository;

    public PhysioService(IPhysioRepository repository)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    public void RegisterPhysio(string name, int registrationNumber, string password, bool availeable, TimeSpan horaApertura, TimeSpan horaCierre, decimal price)
    {
        try 
        {
            Physio physio = new(name, registrationNumber, password, availeable, horaApertura, horaCierre, price);
            _repository.AddPhysio(physio);
            _repository.SaveChanges();
        } 
        catch (Exception e)
        {
            throw new Exception("Ha ocurrido un error al registrar el usuario", e);
        }
    }

    public bool CheckPhysioExist(int registrationNumber)
    {
        try
        {
            foreach (var physio in _repository.GetAllPhysios().Values)
            {
                if (physio.RegistrationNumber.Equals(registrationNumber))
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

    public Physio GetPhysioByRegistrationNumber(int registrationNumber)
    {
        try
        {
            foreach (var physio in _repository.GetAllPhysios().Values)
            {
                if (physio.RegistrationNumber.Equals(registrationNumber))
                {
                    return physio;
                }
            }

            return null;
        }
        catch (Exception e)
        {
            throw new Exception("Ha ocurrido un error al obtener el usuario", e);
        }
    }

    public bool CheckLoginPhysio(int registrationNumber, string password)
    {
        try
        {
            foreach (var physio in _repository.GetAllPhysios().Values)
            {
                if (physio.RegistrationNumber.Equals(registrationNumber) &&
                    physio.Password.Equals(password))
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

    public void UpdatePhysioTreatments(Physio physio, List<Treatment> treatments)
    {
        try
        {
            physio.MyTreatments = treatments;
            _repository.SaveChanges();
        }
        catch (Exception e)
        {
            throw new Exception("Ha ocurrido un error al actualizar los tratamientos del fisioterapeuta.", e);
        }
    }

    public void PrintPhysioTreatments()
    {
        try
        {
            var allPhysios = _repository.GetAllPhysios();

            foreach (var physio in allPhysios.Values)
            {
                Console.WriteLine($"Nombre: {physio.Name}");
                Console.WriteLine($"Número de Colegiado: {physio.RegistrationNumber}");
                Console.WriteLine($"Disponibilidad: {(physio.Availeable ? "Disponible" : "No disponible")}");
                Console.WriteLine($"Horario de apertura: {physio.OpeningTime}");
                Console.WriteLine($"Horario de cierre: {physio.ClosingTime}");
                Console.WriteLine($"Precio por sesión: {physio.Price}");
                Console.WriteLine($"------------------------------------");
                Console.WriteLine("");
            }
        }
        catch (Exception e)
        {
            throw new Exception("Ha ocurrido un error al imprimir los tratamientos de los fisioterapeutas.", e);
        }
    }

}
