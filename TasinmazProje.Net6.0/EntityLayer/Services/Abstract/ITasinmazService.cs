using EntityLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLayer.Services.Abstract
{
    public interface ITasinmazService
    {
        Task<IEnumerable<Tasinmaz>> GetAll();
        Task<IEnumerable<Il>> GetAllCities();
        Task<IEnumerable<Ilce>> GetAllDistricts(int id);
        Task<IEnumerable<Mahalle>> GetAllNeighbourhood(int id);
        Task Add(Tasinmaz tasinmaz);
        Task Delete(int id);
        Task<Tasinmaz> GetById(int id);
        Task Update(Tasinmaz tasinmaz);
    }
}
