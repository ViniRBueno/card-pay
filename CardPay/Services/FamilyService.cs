using CardPay.Entities;
using CardPay.Interfaces;
using CardPay.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
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

        public IEnumerable<FamilyMember> GetFamilyMembers(int userId)
        {
            var family = new List<FamilyMember>();
            var user = GetUser(userId);
            family.Add(new FamilyMember()
            {
                cpf = user.cpf,
                member_name = user.user_name,
                salary = user.salary
            });
            
            var familyId = GetFamilyByUserId(userId).id_family;
            var members = _context.familyMembers.Where(f => f.id_family == familyId).ToList();

            foreach (var member in members)
            {
                family.Add(member);
            }
            return family;
        }

        public FamilyMember CreateFamilyMember(FamilyMemberModel memberModel, int userId)
        {
            var familyId = GetFamilyByUserId(userId).id_family;
            var familyMember = new FamilyMember(memberModel, familyId);

            CreateRegister(familyMember);
            UpdateTotalSalary(userId);

            return familyMember;
        }

        public FamilyMember UpdateFamilyMember(FamilyMemberModel memberModel, int userId)
        {
            var familyId = GetFamilyByUserId(userId).id_family;
            var members = GetFamilyMembersByFamilyId(familyId);
            var updateMember = members.Where(m => m.id_member == memberModel.id).FirstOrDefault();

            if (updateMember == null)
                throw new System.Exception("Membro não encontrado na família!");

            updateMember.cpf = memberModel.cpf;
            updateMember.member_name = memberModel.member_name;
            updateMember.salary = memberModel.salary;

            UpdateRegister(updateMember);
            UpdateTotalSalary(userId);

            return updateMember;
        }

        public void CreateFamily(int userId)
        {
            var family = new Family(userId);
            CreateRegister(family);
        }

        public void UpdateTotalSalary(int userId)
        {
            var user = GetUser(userId);
            var family = GetFamilyByUserId(userId);
            var familyMembers = GetFamilyMembersByFamilyId(family.id_family);
            decimal totalSalary = user.salary;

            foreach (var member in familyMembers)
            {
                totalSalary += member.salary;
            }

            family.total_salary = totalSalary;

            UpdateRegister(family);
        }

        #region Private Methods
        private Family GetFamilyByUserId(int userId) => _context.families.Where(f => f.id_user == userId).FirstOrDefault();

        private User GetUser(int userId) => _context.users.Where(u => u.id_user == userId).FirstOrDefault();

        private IEnumerable<FamilyMember> GetFamilyMembersByFamilyId(int familyId) => _context.familyMembers.Where(m => m.id_family == familyId).ToList();

        private void CreateRegister<T>(T entity) where T : class
        {
            _context.Set<T>().Add(entity);
            _context.SaveChanges();
        }

        private void UpdateRegister<T>(T entity) where T : class
        {
            _context.Set<T>().Update(entity);
            _context.SaveChanges();
        }
        #endregion
    }
}
