﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using JJ.UW.Core.Interfaces;
using JJ.UW.Data.Interfaces;
using JJ.UW.Data.Extensoes;

namespace CF.InfraData.Repositories
{
    public abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected IUnitOfWork unitOfWork = null;

        public Repository(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public int Adicionar(TEntity entity)
        {
            return unitOfWork.Connection.Adicionar(entity, unitOfWork.Transaction);
        }

        public int Atualizar(TEntity entity)
        {
            return unitOfWork.Connection.Atualizar(entity, unitOfWork.Transaction);
        }

        public bool CriarTabela(string query)
        {
            return unitOfWork.Connection.CriarTabelas(query, unitOfWork.Transaction);
        }

        public int Deletar(object id)
        {
            return unitOfWork.Connection.Deletar<TEntity>(id, unitOfWork.Transaction);
        }

        public void Dispose()
        {
            unitOfWork.Dispose();
            GC.SuppressFinalize(this);
        }

        public int ExecutarQuery(string query)
        {
            return unitOfWork.Connection.ExecutarQuery(query, unitOfWork.Transaction);
        }

        public TEntity Obter(int id)
        {
            return unitOfWork.Connection.Obter<TEntity>(id);
        }

        public IEnumerable<TEntity> ObterLista(string condition = "", object parameters = null)
        {
            return unitOfWork.Connection.ObterLista<TEntity>(condition, parameters);
        }
    }
}