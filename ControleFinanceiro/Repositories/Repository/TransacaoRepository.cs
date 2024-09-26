using ControleFinanceiro.Domain.Entities;
using ControleFinanceiro.Repositories.Interfaces;
using Dapper.Contrib.Extensions;
using System.Data;

namespace ControleFinanceiro.Repositories.Repository
{
    public class TransacaoRepository : ITransacaoRepository
    {
        private readonly IDbConnection _dbConnection;

        public TransacaoRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<IEnumerable<Transacao>> ObterTodasTransacoesAsync()
        {
            return await _dbConnection.GetAllAsync<Transacao>();
        }

        public async Task<Transacao> ObterTransacaoPorIdAsync(Guid id)
        {
            return await _dbConnection.GetAsync<Transacao>(id);
        }

        public async Task AdicionarTransacaoAsync(Transacao transacao)
        {
            await _dbConnection.InsertAsync(transacao);
        }

        public async Task AtualizarTransacaoAsync(Transacao transacao)
        {
            await _dbConnection.UpdateAsync(transacao);
        }

        public async Task RemoverTransacaoAsync(Guid id)
        {
            var transacao = await ObterTransacaoPorIdAsync(id);
            if (transacao != null)
            {
                await _dbConnection.DeleteAsync(transacao);
            }
        }

        public async Task<decimal> ObterSaldoAsync()
        {
            var transacoes = await ObterTodasTransacoesAsync();
            decimal ganhos = 0, gastos = 0;

            foreach (var transacao in transacoes)
            {
                if (transacao.Valor > 0)
                    ganhos += transacao.Valor;
                else
                    gastos += transacao.Valor;
            }

            return ganhos + gastos;
        }
    }
}
