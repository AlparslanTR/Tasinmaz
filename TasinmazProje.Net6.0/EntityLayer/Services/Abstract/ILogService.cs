using EntityLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLayer.Services.Abstract
{
    public interface ILogService
    {
        Task<IEnumerable<Log>> GetAll();
        Task Add(Log log);
    }
}
