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
using Windows.UI.Xaml.Documents;

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
            //InserirRegistrosParaTeste(uow);
        }

        private static void CriarTabelas(IUnitOfWork uow)
        {
            bool cfTipoPagamento = false;
            bool cfCategoria = false;
            bool cfTerceiro = false;
            bool cfRegistroFinanceiro = false;
            bool cfStatus = false;
            bool cfTipoOperacao = false;
            bool cfTipoTransacaoFinanceira = false;

            try
            {
                cfCategoria = uow.Connection.VerificarTabelaExistente<CFCategoria>();
                cfRegistroFinanceiro = uow.Connection.VerificarTabelaExistente<CFRegistroFinanceiro>();
                cfStatus = uow.Connection.VerificarTabelaExistente<CFStatus>();
                cfTerceiro = uow.Connection.VerificarTabelaExistente<CFTerceiro>();
                cfTipoPagamento = uow.Connection.VerificarTabelaExistente<CFTipoDePagamento>();
                cfTipoOperacao = uow.Connection.VerificarTabelaExistente<CFTipoOperacao>();
                cfTipoTransacaoFinanceira = uow.Connection.VerificarTabelaExistente<CFTipoTransacaoFinanceira>();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            if (cfTipoPagamento == true && cfCategoria == true && cfTerceiro == true && cfRegistroFinanceiro == true)
                return;

            try
            {
                uow.Begin();

                if (cfTipoPagamento == false)
                    uow.Connection.CriarTabela<CFTipoDePagamento>(uow.Transaction);

                if (cfCategoria == false)
                    uow.Connection.CriarTabela<CFCategoria>(uow.Transaction);

                if (cfTerceiro == false)
                    uow.Connection.CriarTabela<CFTerceiro>(uow.Transaction);

                if(cfStatus == false)
                    uow.Connection.CriarTabela<CFStatus>(uow.Transaction);

                if (cfTipoOperacao == false)
                    uow.Connection.CriarTabela<CFTipoOperacao>(uow.Transaction);

                if (cfTipoTransacaoFinanceira == false) 
                    uow.Connection.CriarTabela<CFTipoTransacaoFinanceira>(uow.Transaction);

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
            var cFCategoriaRepository = Container.GetInstance<ICFCategoriaRepository>();
            var cfStatusRepository = Container.GetInstance<ICFStatusRepository>();
            var cFTipoDePagamentoRepository = Container.GetInstance<ICFTipoDePagamentoRepository>();
            var cfTipoOperacaoRepository = Container.GetInstance<ICFTipoOperacaoRepository>();
            var cfTipoTransacaoFinanceiraRepository = Container.GetInstance<ICFTipoTransacaoFinanceiraRepository>();

            bool inserirTipoDePagamento = false;
            bool inserirCategoria = false;
            bool inserirStatus = false;
            bool inserirTipoOperacao = false;
            bool inserirTipoTransacao = false;

            try
            {
                var cfTipoDePagamentoCollection = cFTipoDePagamentoRepository.ObterLista().ToList();
                var cfCategoriaCollection = cFCategoriaRepository.ObterLista().ToList();
                var statusCollection = cfStatusRepository.ObterLista().ToList();
                var cfTipoOperacaoCollection = cfTipoOperacaoRepository.ObterLista().ToList();
                var cfTipoTransacaoFinanceiraCollection = cfTipoTransacaoFinanceiraRepository.ObterLista().ToList();

                if (cfTipoDePagamentoCollection.Count() <= 0)
                    inserirTipoDePagamento = true;

                if (cfCategoriaCollection.Count() <= 0)
                    inserirCategoria = true;

                if (statusCollection.Count() <= 0)
                    inserirStatus = true;

                if(cfTipoOperacaoCollection.Count() <= 0)
                    inserirTipoOperacao = true;

                if(cfTipoTransacaoFinanceiraCollection.Count() <= 0)
                    inserirTipoTransacao = true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            if (!inserirTipoDePagamento && !inserirCategoria && !inserirStatus && !inserirTipoOperacao && !inserirTipoTransacao)
                return;

            try
            {
                uow.Begin();

                if (inserirTipoDePagamento)
                {
                    var tiposPagamentoInicial = cFTipoDePagamentoRepository.ObterTipoDePagamentoInicial().ToList();

                    foreach (var tipoPagamento in tiposPagamentoInicial)
                        cFTipoDePagamentoRepository.Adicionar(new CFTipoDePagamento { Pagamento = tipoPagamento, Ativo = true });
                }

                if (inserirCategoria)
                {
                    var categoriaInicial = cFCategoriaRepository.ObterCategoriaInicial().ToList();

                    foreach (var categoria in categoriaInicial)
                        cFCategoriaRepository.Adicionar(new CFCategoria { Categoria = categoria });
                }

                if (inserirStatus)
                {
                    var statusInicial = cfStatusRepository.ObterStatusInicial().ToList();

                    foreach (var status in statusInicial)
                        cfStatusRepository.Adicionar(new CFStatus { Status = status.Status });
                }

                if (inserirTipoOperacao)
                {
                    var tipoOperacao = cfTipoOperacaoRepository.ObterTipoOperacaoInicial().ToList();

                    foreach (var operacao in tipoOperacao)
                        cfTipoOperacaoRepository.Adicionar(new CFTipoOperacao { Nome = operacao.Nome, Ativo=true });
                }

                if (inserirTipoTransacao)
                {
                    var tipoTransacao = cfTipoTransacaoFinanceiraRepository.ObterTipoTransacaoFinanceiraInicial().ToList();

                    foreach (var transacao in tipoTransacao)
                        cfTipoTransacaoFinanceiraRepository.Adicionar(new CFTipoTransacaoFinanceira { Transacao = transacao.Transacao }); 
                }

                uow.Commit();
            }
            catch (Exception)
            {
                uow.Rollback();
                throw new Exception("Falha ao criar as tabelas na base de dados.");
            }
        }

        private static void InserirRegistrosParaTeste(IUnitOfWork uow)
        {
            try
            {
                bool inserirTerceiro = false;
                var cfTerceiroRepository = Container.GetInstance<ICFTerceiroRepository>();
                var cfRegistroFinanceiroRepository = Container.GetInstance<ICFRegistroFinanceiroRepository>();

                inserirTerceiro = cfTerceiroRepository.ObterLista().ToList().Count() <= 0;

                uow.Begin();

                // TERCEIRO
                if (inserirTerceiro)
                {
                    cfTerceiroRepository.Adicionar(new CFTerceiro { Nome = "Enel Energia", FK_CFCategoria = 10, Observacoes = "Fornecedora de energia elétrica" });
                    cfTerceiroRepository.Adicionar(new CFTerceiro { Nome = "CPFL Energia", FK_CFCategoria = 10, Observacoes = "Fornecedora de energia elétrica" });
                    cfTerceiroRepository.Adicionar(new CFTerceiro { Nome = "Supermercado Carrefour", FK_CFCategoria = 6, Observacoes = "Rede de supermercados" });
                    cfTerceiroRepository.Adicionar(new CFTerceiro { Nome = "Hospital Santa Casa", FK_CFCategoria = 8, Observacoes = "Hospital de grande porte" });
                    cfTerceiroRepository.Adicionar(new CFTerceiro { Nome = "Academia BodyTech", FK_CFCategoria = 5, Observacoes = "Rede de academias de ginástica" });
                    cfTerceiroRepository.Adicionar(new CFTerceiro { Nome = "Restaurante Fogo de Chão", FK_CFCategoria = 7, Observacoes = "Restaurante especializado em carnes" });
                    cfTerceiroRepository.Adicionar(new CFTerceiro { Nome = "Petrobras", FK_CFCategoria = 6, Observacoes = "Petroleira brasileira" });
                    cfTerceiroRepository.Adicionar(new CFTerceiro { Nome = "OAB - Ordem dos Advogados do Brasil", FK_CFCategoria = 1, Observacoes = "Organização de classe de advogados" });
                    cfTerceiroRepository.Adicionar(new CFTerceiro { Nome = "Magazine Luiza", FK_CFCategoria = 6, Observacoes = "Varejo de produtos eletrônicos e móveis" });
                    cfTerceiroRepository.Adicionar(new CFTerceiro { Nome = "Ambev", FK_CFCategoria = 7, Observacoes = "Fabricante de bebidas e cervejas" });
                }

                // REGISTRO FINANCEIRO
                Random random = new Random();

                var statusList = new[] { 1, 2, 3 }; 
                var terceirosList = new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 }; 
                var tipoPagamentoList = new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17 }; 
                var tipoTransacaoList = new[] { 1, 2 }; 
                var tipoOperacaoList = new[] { 1, 2, 3 }; 

                for (int i = 0; i < 100; i++)
                {
                    var novoRegistro = new CFRegistroFinanceiro
                    {
                        DataPagamento = i % 2 == 0 ? (DateTime?)DateTime.Now.AddDays(-random.Next(1, 30)) : null, 
                        DataVencimento = DateTime.Now.AddDays(random.Next(1, 30)), 
                        Valor = Convert.ToDecimal( Math.Round(random.NextDouble() * 1000, 2)),
                        FK_CFStatus = statusList[random.Next(statusList.Length)], 
                        FK_CFTerceiro = terceirosList[random.Next(terceirosList.Length)], 
                        FK_CFTipoDePagamento = tipoPagamentoList[random.Next(tipoPagamentoList.Length)],
                        FK_CFTipoTransacaoFinanceira = tipoTransacaoList[random.Next(tipoTransacaoList.Length)],
                        FK_CFTipoOperacao = tipoOperacaoList[random.Next(tipoOperacaoList.Length)]
                    };

                    cfRegistroFinanceiroRepository.Adicionar(novoRegistro);
                }

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
