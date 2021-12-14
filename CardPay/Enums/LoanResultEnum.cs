using System.ComponentModel;

namespace CardPay.Enums
{
    public enum LoanResultEnum
    {
        [Description("Disponivel")]
        Avaliable = 0,

        [Description("Aguardando aprovação")]
        Waiting = 1,

        [Description("Em Andamento")]
        InProgress = 2,

        [Description("Recusada")]
        Negated = 3,

        [Description("Finalizada")]
        Finished = 4,

        [Description("Negado análise de fraude")]
        Fraud = 5
    }
}
