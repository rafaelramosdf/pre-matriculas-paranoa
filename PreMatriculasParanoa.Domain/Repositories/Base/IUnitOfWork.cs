using System;

namespace PreMatriculasParanoa.Domain.Repositories.Base
{
    public interface IUnitOfWork
    {
        /// <summary>
        /// Atenção! se este comando "Commit" estiver dentro do bloco de transação "UnitOfWork.BeginTransaction", o comando "Commit" salva os dados somente em memória, 
        /// caso não esteja em um bloco de transação, o comando efetiva as alterações direto no banco de dados.
        /// </summary>
        int Commit();

        /// <summary>
        /// Abre a Transação (obs.: dentro do bloco de transaction deve-se usar o comando "Commit()" e por último o "CommitTransaction"
        /// </summary>
        void BeginTransaction();

        /// <summary>
        /// Salva a Transação (obs.: Consolida e salva no banco de dados todas as alterações comitadas com o comando "Commit()".
        /// </summary>
        void CommitTransaction();

        /// <summary>
        /// Reverte as alterações de uma tranação
        /// </summary>
        void Rollback();
    }
}
