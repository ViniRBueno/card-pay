using CardPay.Models;

namespace CardPay.Entities
{
    public partial class FamilyMember
    {
        public int id_member { get; set; }
        public int id_family { get; set; }
        public string cpf { get; set; }
        public string member_name { get; set; }
        public decimal salary { get; set; }
        public FamilyMember() { }
        public FamilyMember(FamilyMemberModel memberModel, int familyId)
        {
            id_family = familyId;
            cpf = memberModel.cpf;
            member_name = memberModel.member_name;
            salary = memberModel.salary;
        }
    }
}
