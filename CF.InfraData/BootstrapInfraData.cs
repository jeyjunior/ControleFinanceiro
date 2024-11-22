using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleInjector;
using JJ.UW.Core;
using JJ.UW.Data;
using JJ.UW.Data.Interfaces;
using JJ.UW.Data.Enumerador;
using JJ.UW.Data.Extensoes;
using CF.Domain.Interfaces;
using CF.InfraData.Repositories;
using CF.Domain.Entities;

namespace CF.InfraData
{
    public class BootstrapInfraData
    {
        private static eConexao Conexao { get; set; } = eConexao.SQLite;

        public static Container Container { get; private set; }

        public static void Iniciar(Container container)
        {
            Container = container;

            Config.DefinirConexao(Conexao);

            Container.Register<IUnitOfWork, UnitOfWork>(Lifestyle.Singleton);
            Container.Register<ICFCategoriaRepository, CFCategoriaRepository>(Lifestyle.Singleton);
            Container.Register<ICFRegistroFinanceiroRepository, CFRegistroFinanceiroRepository>(Lifestyle.Singleton);
            Container.Register<ICFStatusRepository, CFStatusRepository>(Lifestyle.Singleton);
            Container.Register<ICFTerceiroRepository, CFTerceiroRepository>(Lifestyle.Singleton);
            Container.Register<ICFTipoDePagamentoRepository, CFTipoDePagamentoRepository>(Lifestyle.Singleton);
            Container.Register<ICFTipoOperacaoRepository, CFTipoOperacaoRepository>(Lifestyle.Singleton);
            Container.Register<ICFTipoTransacaoFinanceiraRepository, CFTipoTransacaoFinanceiraRepository>(Lifestyle.Singleton);

            IniciarEstrutura();
        }

        private static void IniciarEstrutura()
        {
            var uow = Container.GetInstance<IUnitOfWork>();

            CriarTabelas(uow);
            InserirInformacoesIniciais(uow);
        }

        private static void CriarTabelas(IUnitOfWork uow)
        {
            bool tipoPagamento = false;
            bool categoria = false;
            bool terceiro = false;
            bool cfRegistroFinanceiro = false;

            try
            {
                tipoPagamento = uow.Connection.VerificarTabelaExistente<CFTipoDePagamento>();
                categoria = uow.Connection.VerificarTabelaExistente<CFCategoria>();
                terceiro = uow.Connection.VerificarTabelaExistente<CFTerceiro>();
                cfRegistroFinanceiro = uow.Connection.VerificarTabelaExistente<CFRegistroFinanceiro>();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            if (tipoPagamento == true && categoria == true && terceiro == true && cfRegistroFinanceiro == true)
                return;

            try
            {
                uow.Begin();

                if (tipoPagamento == false)
                    uow.Connection.CriarTabela<CFTipoDePagamento>(uow.Transaction);

                if (categoria == false)
                    uow.Connection.CriarTabela<CFCategoria>(uow.Transaction);

                if (terceiro == false)
                    uow.Connection.CriarTabela<CFTerceiro>(uow.Transaction);

                if (cfRegistroFinanceiro == false)
                    uow.Connection.CriarTabela<CFRegistroFinanceiro>(uow.Transaction);

                uow.Commit();
            }
            catch (Exception)
            {
                uow.Rollback();
                throw new Exception("Falha ao criar as tabelas na base de dados.");
            }
        }

        private static void InserirInformacoesIniciais(IUnitOfWork uow)
        {
            var cFTipoDePagamentoRepository = Container.GetInstance<ICFTipoDePagamentoRepository>();

            try
            {
                var cfTipoDePagamentoCollection = cFTipoDePagamentoRepository.ObterLista().ToList();

                if (cfTipoDePagamentoCollection.Count() > 0)
                    return;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            try
            {
                uow.Begin();

                var tiposPagamentoInicial = cFTipoDePagamentoRepository.ObterTipoDePagamentoInicial().ToList();

                foreach (var tipoPagamento in tiposPagamentoInicial)
                    cFTipoDePagamentoRepository.Adicionar(new CFTipoDePagamento { Pagamento = tipoPagamento, Ativo = true });

                uow.Commit();
            }
            catch (Exception)
            {
                uow.Rollback();
                throw new Exception("Falha ao criar as tabelas na base de dados.");
            }
        }
    }
}
