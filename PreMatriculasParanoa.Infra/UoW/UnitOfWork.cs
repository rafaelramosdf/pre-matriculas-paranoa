using Microsoft.EntityFrameworkCore.Storage;
using PreMatriculasParanoa.Infra.Context;
using PreMatriculasParanoa.Domain.Repositories.Base;

namespace PreMatriculasParanoa.Infra.UoW
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly EntityContext _entityContext;
        private IDbContextTransaction _transaction;

        public UnitOfWork(EntityContext entityContext)
        {
            _entityContext = entityContext;
        }

        /// <summary>
        /// Atenção! se este comando "Commit" estiver dentro do bloco de transação "UnitOfWork.BeginTransaction", o comando "Commit" salva os dados somente em memória, 
        /// caso não esteja em um bloco de transação, o comando efetiva as alterações direto no banco de dados.
        /// </summary>
        public int Commit()
        {
            return _entityContext.SaveChanges();
        }

        /// <summary>
        /// Abre a Transação (obs.: dentro do bloco de "transaction" deve-se usar o comando "Commit()" e por último o "CommitTransaction"
        /// </summary>
        public void BeginTransaction()
        {
            _transaction = _transaction ?? _entityContext.Database.BeginTransaction();
        }

        /// <summary>
        /// Salva a Transação (obs.: Consolida e salva no banco de dados todas as alterações comitadas com o comando "Commit()").
        /// </summary>
        public void CommitTransaction()
        {
            _transaction.Commit();
        }

        /// <summary>
        /// Reverte as alterações de uma tranação
        /// </summary>
        public void Rollback()
        {
            _transaction.Rollback();
        }
    }
}
