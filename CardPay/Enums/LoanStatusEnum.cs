using System.ComponentModel;

namespace CardPay.Enums
{
    public enum LoanStatusEnum
    {
        [Description("Empréstimo Criado")]
        Created = 1,
        
        [Description("Empréstimo ativo para o cliente e sendo pagos")]
        Active = 2,

        [Description("Empréstimo rejeitado pelo operador")]
        Rejected = 3,

        [Description("Empréstimo já finalizado")]
        Inactive = 4,

        [Description("Empréstimo rejeitado por fraude")]
        Fraud = 5
    }
}
