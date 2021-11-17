using CardPay.Entities;
using CardPay.Interfaces;
using CardPay.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace CardPay.Services
{
    public class FamilyService : IFamilyService
    {
        CardPayContext _context;
        public FamilyService()
        {
            var contextOptions = new DbContextOptionsBuilder<CardPayContext>()
                .UseSqlServer(@"Server=localhost;Database=DB_CARD_PAY;Integrated Security=True")
                .Options;

            _context = new CardPayContext(contextOptions);
        }

        private Family GetFamilyByUserId(int userId) => _context.families.Where(f => f.id_user == userId).FirstOrDefault();

        public bool CreateFamilyMember(FamilyMemberModel memberModel, int userId)
        {
            var familyId = this.GetFamilyByUserId(userId).id_family;
            var familyMember = new FamilyMember(memberModel, familyId);

            CreateRegister(familyMember);

            return true;
        }

        public void CreateFamily(int userId)
        {
            var family = new Family(userId);
            CreateRegister(family);
        }

        public void UpdateTotalSalary(int userId)
        {
            var userSalary = _context.users.Where(u => u.id_user == userId).FirstOrDefault().salary;
            var family = GetFamilyByUserId(userId);
            var familyMembers = _context.familyMembers.Where(fm => fm.id_family == family.id_family).ToList();
            decimal totalSalary = userSalary;

            foreach (var member in familyMembers)
            {
                totalSalary += member.salary;
            }

            family.total_salary = totalSalary;

            CreateRegister(family);
        }

        private void CreateRegister<T>(T entity) where T : class
        {
            _context.Set<T>().Add(entity);
            _context.SaveChanges();
        }
    }
}
