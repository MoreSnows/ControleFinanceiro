using ControleFinanceiro.Domain.Entities;

namespace ControleFinanceiro.Services.Interfaces
{
    public interface ITransacaoService
    {
        Task<IEnumerable<Transacao>> ObterTodasTransacoesAsync();
        Task<Transacao> ObterTransacaoPorIdAsync(Guid id);
        Task AdicionarTransacaoAsync(Transacao transacao);
        Task<decimal> ObterSaldoAsync();
    }
}
