using System.ComponentModel;

namespace CardPay.Enums
{
    public enum LoanResultEnum
    {
        [Description("Aguardando aprovação")]
        Waiting = 1,
        
        [Description("Recusada")]
        Negated = 2,

        [Description("Disponivel")]
        Avaliable = 3,
        
        [Description("Em Andamento")]
        InProgress = 4,

        [Description("Negado análise de fraude")]
        Fraud = 5
    }
}
