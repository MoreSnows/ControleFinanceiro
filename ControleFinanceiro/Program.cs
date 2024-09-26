using ControleFinanceiro.Domain.Entities;
using ControleFinanceiro.Domain.Enum;
using ControleFinanceiro.Infrastructure.Database;
using ControleFinanceiro.Repositories.Repository;
using ControleFinanceiro.Services.Interfaces;
using ControleFinanceiro.Services.Services;
using Microsoft.Extensions.Logging;

class Program
{
    static async Task Main(string[] args)
    {

        string connectionString = @"Server=localhost\sqlexpress;Database=ControleFinancas;User Id=sa;Password=250575;TrustServerCertificate=True";
        var dbConnectionFactory = new DbConnectionFactory(connectionString);
        var transacaoRepository = new TransacaoRepository(dbConnectionFactory.GetConnection());
        var transacaoService = new TransacaoService(transacaoRepository);

        bool continuar = true;

        while (continuar)
        {
            Console.WriteLine("Escolha uma opção:");
            Console.WriteLine("1 - Adicionar Transação");
            Console.WriteLine("2 - Ver Saldo");
            Console.WriteLine("3 - Relatório de Transações");
            Console.WriteLine("4 - Definir Meta Financeira");
            Console.WriteLine("5 - Sair");

            var escolha = Console.ReadLine();
            continuar = await ProcessarEscolhaAsync(escolha, transacaoService);
        }
    }

    private static async Task<bool> ProcessarEscolhaAsync(string escolha, ITransacaoService transacaoService)
    {
        switch (escolha)
        {
            case "1":
                Console.Clear();
                await AdicionarTransacaoAsync(transacaoService);
                break;
            case "2":
                Console.Clear();
                await ExibirSaldoAsync(transacaoService);
                break;
            case "3":
                Console.Clear();
                await ExibirRelatorioAsync(transacaoService);
                break;
            case "4":
                Console.Clear();
                DefinirMetaFinanceira();
                break;
            case "5":
                Console.Clear();
                return false;
            default:
                Console.WriteLine("Escolha inválida. Tente novamente.");
                break;
        }
        return true;
    }

    private static async Task AdicionarTransacaoAsync(ITransacaoService transacaoService)
    {
        Console.WriteLine("Descrição:");
        var descricao = Console.ReadLine();

        decimal valor;
        while (true)
        {
            Console.WriteLine("Valor:");
            if (decimal.TryParse(Console.ReadLine(), out valor)) break;
            Console.WriteLine("Valor inválido. Tente novamente.");
        }

        Console.WriteLine("Escolha uma categoria:");
        foreach (var categoria in Enum.GetValues(typeof(Categorias)))
        {
            Console.WriteLine($"{(int)categoria} - {categoria}");
        }

        int categoriaId;
        while (true)
        {
            if (int.TryParse(Console.ReadLine(), out categoriaId) && Enum.IsDefined(typeof(Categorias), categoriaId))
            {
                // Categoria válida
                Console.WriteLine($"Você escolheu a categoria ID: {categoriaId}");
                break;
            }
            Console.WriteLine("Categoria inválida. Tente novamente.");
        }

        var transacao = new Transacao
        {
            Descricao = descricao,
            Valor = valor,
            Data = DateTime.Now,
            Categoria = categoriaId
        };
        Console.WriteLine($"transação id:{transacao.Id}");

        await transacaoService.AdicionarTransacaoAsync(transacao);
        Console.WriteLine("Transação adicionada com sucesso!");
    }

    private static async Task ExibirSaldoAsync(ITransacaoService transacaoService)
    {
        var saldo = await transacaoService.ObterSaldoAsync();
        Console.WriteLine($"Saldo atual: {saldo:C}");
    }

    private static async Task ExibirRelatorioAsync(ITransacaoService transacaoService)
    {
        var transacoes = await transacaoService.ObterTodasTransacoesAsync();
        foreach (var transacao in transacoes)
        {
            Console.WriteLine($"{transacao.Data:yyyy-MM-dd} - {transacao.Descricao}: {transacao.Valor:C}");
        }
    }

    private static void DefinirMetaFinanceira()
    {
        Console.WriteLine("Defina uma meta de economia:");
        if (decimal.TryParse(Console.ReadLine(), out var meta))
        {
            Console.WriteLine($"Meta de economia definida: {meta:C}");
        }
        else
        {
            Console.WriteLine("Meta inválida. Tente novamente.");
        }
    }
}