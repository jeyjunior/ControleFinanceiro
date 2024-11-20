using System;
using System.Collections.Generic;
using System.Linq;
using JJ.UW.Data.Interfaces;
using CF.Domain.Entities;
using CF.Domain.Interfaces;

namespace CF.InfraData.Repositories
{
    public class CFTipoDePagamentoRepository : Repository<CFTipoDePagamento>, ICFTipoDePagamentoRepository
    {
        public CFTipoDePagamentoRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public IEnumerable<string> ObterTipoDePagamentoInicial()
        {
            var tipoDePagamentoCollection = new List<string>()
            {
                "Boleto",
                "Carteira Digital",
                "Cartão Pré-pago",
                "Cheque",
                "Consórcio",
                "Crédito",
                "Criptomoedas",
                "Débito",
                "Débito Automático",
                "Dinheiro",
                "Financiamento",
                "Link de Pagamento",
                "Pagamento por QR Code",
                "PIX",
                "Transferência Bancária",
                "Vale-Alimentação",
                "Vale-Refeição"
            };

            return tipoDePagamentoCollection.OrderBy(i => i).ToList();
        }
    }
}
