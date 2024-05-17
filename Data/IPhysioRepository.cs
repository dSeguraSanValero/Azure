using FisioSolution.Models;

namespace FisioSolution.Data;
public interface IPhysioRepository
{
    void AddPhysio(Physio physio);
    void SaveChanges();
    Physio GetPhysio(int stringRegisterNumber);
    Dictionary<string, Physio> GetAllPhysios();
}