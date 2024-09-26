using ControleFinanceiro.Domain.Entities;

namespace ControleFinanceiro.Repositories.Interfaces
{
    public interface ITransacaoRepository
    {
        Task<IEnumerable<Transacao>> ObterTodasTransacoesAsync();
        Task<Transacao> ObterTransacaoPorIdAsync(Guid id);
        Task AdicionarTransacaoAsync(Transacao transacao);
        Task AtualizarTransacaoAsync(Transacao transacao);
        Task RemoverTransacaoAsync(Guid id);
        Task<decimal> ObterSaldoAsync();
    }
}
