using ControleFinanceiro.Domain.Entities;
using ControleFinanceiro.Repositories.Interfaces;
using ControleFinanceiro.Services.Interfaces;

namespace ControleFinanceiro.Services.Services
{
    public class TransacaoService : ITransacaoService
    {
        private readonly ITransacaoRepository _transacaoRepository;

        public TransacaoService(ITransacaoRepository transacaoRepository)
        {
            _transacaoRepository = transacaoRepository;
        }

        public async Task<IEnumerable<Transacao>> ObterTodasTransacoesAsync()
        {
            return await _transacaoRepository.ObterTodasTransacoesAsync();
        }

        public async Task<Transacao> ObterTransacaoPorIdAsync(Guid id)
        {
            return await _transacaoRepository.ObterTransacaoPorIdAsync(id);
        }

        public async Task AdicionarTransacaoAsync(Transacao transacao)
        {
            await _transacaoRepository.AdicionarTransacaoAsync(transacao);
        }

        public async Task<decimal> ObterSaldoAsync()
        {
            return await _transacaoRepository.ObterSaldoAsync();
        }
    }
}
