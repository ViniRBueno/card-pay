using System.ComponentModel;

namespace CardPay.Enums
{
    public enum LoanStatusEnum
    {
        [Description("Empréstimo Criado")]
        Created = 1,
        
        [Description("Empréstimo Aprovado pelo operador")]
        Approved = 2,

        [Description("Empréstimo ativo para o cliente e sendo pagos")]
        Active = 3,

        [Description("Empréstimo rejeitado pelo operador")]
        Rejected = 4,

        [Description("Empréstimo já finalizado")]
        Inactive = 5
    }
}
