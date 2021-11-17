using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CardPay.Interfaces
{
    public interface IFamilyService
    {
        void CreateFamily(int id);
        void UpdateTotalSalary(int userId);
    }
}
