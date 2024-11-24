using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JJ.UW.Data.Interfaces;
using CF.Domain.Entities;
using CF.Domain.Interfaces;

namespace CF.InfraData.Repositories
{
    public class CFCategoriaRepository : Repository<CFCategoria>, ICFCategoriaRepository
    {
        public CFCategoriaRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public IEnumerable<string> ObterCategoriaInicial()
        {
            var tipoDePagamentoCollection = new List<string>()
            {
                "Educação",
                "Farmácia",
                "Impostos",
                "Lavanderia",
                "Lazer",
                "Mercado",
                "Restaurante",
                "Saúde",
                "Tecnologia",
                "Transporte",
            };

            return tipoDePagamentoCollection.OrderBy(i => i).ToList();
        }
    }
}
