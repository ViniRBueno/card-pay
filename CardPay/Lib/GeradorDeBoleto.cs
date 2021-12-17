using BoletoNetCoreWk;
using CardPay.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CardPay.Lib
{
    public class GeradorDeBoleto
    {
        public ZBoleto _boleto;
        public GeradorDeBoleto(ZBoleto boleto)
        {
            this._boleto = boleto;
        }

        public string GerarBoleto()
        {
            string nossoNumero = "453";
            string numeroDocumento = "BB943A";
            string codigoDeBarras = _boleto.CodigoBarras;
            string linhaDigitavel = _boleto.LinhaDigitavel;
            var contaBancaria = new ContaBancaria
            {
                Agencia = "1358",
                DigitoAgencia = "8",
                Conta = "9965842",
                DigitoConta = "1",
                CarteiraPadrao = "09",
                TipoCarteiraPadrao = TipoCarteira.CarteiraCobrancaSimples,
                TipoFormaCadastramento = TipoFormaCadastramento.ComRegistro,
                TipoImpressaoBoleto = TipoImpressaoBoleto.Empresa
            };
            IBanco _banco = Banco.Instancia(Bancos.Bradesco);
            _banco.Cedente = GerarCedente("151988", "", contaBancaria);
            _banco.FormataCedente();

            var endereco = new Endereco
            {
                LogradouroEndereco = "-",
                LogradouroNumero = "-",
                Bairro = "-",
                Cidade = "-",
                UF = "-",
                CEP = "-"
            };

            var sacado = new Sacado
            {
                CPFCNPJ = _boleto.CpfCliente,
                Nome = _boleto.NomeCliente,
                Observacoes = "Parcela empréstimo",
                Endereco = endereco,
            };
            CodigoBarra barra = new CodigoBarra();
            barra.LinhaDigitavel = linhaDigitavel;

            barra.CodigoBanco = "237";
            barra.LinhaDigitavel = linhaDigitavel;

            BoletoNetCoreWk.Boleto boleto = new BoletoNetCoreWk.Boleto(_banco)
            {
                DataVencimento = _boleto.DataVencimento,
                ValorTitulo = (decimal)_boleto.Valor,
                NossoNumero = nossoNumero,
                DescricaoOcorrencia= _boleto.Descricao,
                MensagemInstrucoesCaixa = _boleto.Descricao,
                EspecieDocumento = TipoEspecieDocumento.DM,
                Sacado = sacado,
                DataEmissao = _boleto.DataEmissao,
                DataProcessamento = DateTime.Now,
                NumeroDocumento = numeroDocumento,
                MensagemArquivoRemessa = "Mensagem para o arquivo remessa",
            };

            boleto.CodigoBarra.LinhaDigitavel = linhaDigitavel;
            boleto.CodigoBarra.CampoLivre = codigoDeBarras.Substring(0, 25);
            BoletoBancario boletob = new BoletoBancario();
            boletob.Boleto = boleto;
            var teste22 = boletob.MontaHtml();
            return teste22;
        }

        static Cedente GerarCedente(string codigoCedente, string digitoCodigoCedente, ContaBancaria contaBancaria)
        {
            return new Cedente
            {
                CPFCNPJ = "86.875.666/0001-09",
                Nome = "COMUNIBRAN LTDA",
                Codigo = codigoCedente,
                CodigoDV = digitoCodigoCedente,
                Endereco = new BoletoNetCoreWk.Endereco
                {
                    LogradouroEndereco = "Rua Imperatriz Leopoldina",
                    LogradouroNumero = "5",
                    LogradouroComplemento = "Cj 12",
                    Bairro = "Vila leopoldina",
                    Cidade = "São Paulo",
                    UF = "SP",
                    CEP = "02675-031"
                },
                ContaBancaria = contaBancaria
            };
        }
    }
}
