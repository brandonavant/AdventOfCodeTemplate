using System.Threading.Tasks;
using AdventOfCodeTemplate.Base.Models;

namespace AdventOfCodeTemplate.Base.Services
{
    public interface IInputService
    {
        Task<DayInput> GetInput(int year, int day);
    }
}
