using Microsoft.EntityFrameworkCore;
using CursoEFCore.Data;
using System;
using CursoEFCore.domain;
using CursoEFCore.ValueObjects;
using Microsoft.Extensions.Logging.Console;

internal class NewBaseType
{
    private static void ConsultarPedidoCarregamentoAdiantado()
    {
        using var db = new CursoEFCore.Data.ApplicationContext();
        var pedidos = db.Pedidos.Include(p => p.Itens).ThenInclude(p => p.Produto).ToList();

        Console.WriteLine(pedidos.Count);
    }
}

internal class Program : NewBaseType
{
    private static void Main(string[] args)
    {
        /*
        using var db = new CursoEFCore.Data.ApplicationContext();

        db.Database.Migrate();*/

        //InserirDados();
        //InserirDadosEmMassa();
        //ConsultarDados();
        //CadastrarPedido();
        //ConsultarPedidoCarregamentoAdiantado();
        //AtualizarDados();
        RemoverDados();
        
    }

    public static void RemoverDados()
    {
        using var db = new CursoEFCore.Data.ApplicationContext();
        //var cliente = db.Clientes.Find(5); -- Localiza o registro interagindo com o bando duas vezes
        var cliente = new Cliente { Id = 4}; //É passado como paramêtro sem consultar, deletando em apenas uma interação

        //db.Clientes.Remove(cliente);
        //db.Remove(cliente);
        db.Entry(cliente).State = EntityState.Deleted;

        db.SaveChanges();
    }

    public static void AtualizarDados()
    {
        using var db = new CursoEFCore.Data.ApplicationContext();

        //Exemplo ambiente conectado
        //var cliente = db.Clientes.Find(3);
        //cliente.Nome = "Vinícius Eduardo";


        //Ambiente desconectado (Para um front que interaje com uma API)
        var cliente = new Cliente
        {
            Id = 3
        };

        var clienteDesconectado = new
        {
            Nome = "Vinicius Eduardo",
            Telefone = "11953337469"
        };

        db.Attach(cliente);
        db.Entry(cliente).CurrentValues.SetValues(clienteDesconectado);

        //db.Clientes.Update(cliente); -- Má prática, altera "tudo", deixar sem este comando quando vamos atualizar um ou poucos registros
        db.SaveChanges();
    }

    private static void CadastrarPedido()
    {
        using var db = new CursoEFCore.Data.ApplicationContext();

        var cliente = db.Clientes.FirstOrDefault();
        var produto = db.Produtos.FirstOrDefault();

        var pedido = new Pedido
        {
            ClienteId = cliente.Id,
            IniciadoEm = DateTime.Now,
            FinalizadoEm = DateTime.Now,
            Observacao = "Pedido teste",
            Status = StatusPedido.Analise,
            TipoFrete = TipoFrete.SemFrete,
                Itens = new List<PedidoItem>
                {
                    new PedidoItem
                    {
                        ProdutoId = produto.Id,
                        Desconto = 0,
                        Quantidade = 1,
                        Valor = 10,
                    }
                }
        };
        db.Pedidos.Add(pedido);
        db.SaveChanges();
    }

    private static void ConsultarDados()
    {

        
        using var db = new CursoEFCore.Data.ApplicationContext();
        
        //var consultaPorSintaxe = (from c in db.Clientes where c.Id > 0 select c).ToList();
        var consultaPorMetodo = db.Clientes.AsNoTracking().Where(p=>p.Id > 0).OrderBy(p=>p.Id).ToList();


        foreach(var cliente in consultaPorMetodo)
        {
            Console.WriteLine($"Consultando Cliente: {cliente.Id}");
            //db.Clientes.Find(cliente.Id);
            db.Clientes.FirstOrDefault(p=>p.Id==cliente.Id);
        }
    }

    private static void InserirDadosEmMassa()
    {
        
        var produto = new Produto
        {
            Descricao = "Produto Teste",
            CodigoDeBarras = "123456789121",
            Valor = 10m,
            TipoProduto = TipoProduto.MercadoriaParaRevenda,
            Ativo = true
        };

        var cliente = new Cliente{
            Nome = "Vinícius Eduardo",
            CEP = "09172260",
            Cidade = "Santo André",
            Estado = "SP",
            Telefone = "11953337469"
        };

        var listaClientes = new[]
        {
            new Cliente
            {
                Nome = "Vicente",
                CEP = "09172270",
                Cidade = "São Bernardo",
                Estado = "SP",
                Telefone = "11946262473"
            },
            new Cliente
            {
                Nome = "Rafael",
                CEP = "09172250",
                Cidade = "São Caetano",
                Estado = "SP",
                Telefone = "11953337489"
            }
        };

        var db = new CursoEFCore.Data.ApplicationContext();
        //db.AddRange(produto, cliente);
        db.Set<Cliente>().AddRange(listaClientes);
        //db.Clientes.AddRange(listaClientes);

        var registros = db.SaveChanges();
        Console.WriteLine($"Registros: {registros}");
    }

    private static void InserirDados()
    {
        var produto = new Produto
        {
            Descricao = "Produto teste",
            CodigoDeBarras = "1234567891231",
            Valor = 10m,
            TipoProduto = TipoProduto.MercadoriaParaRevenda,
            Ativo = true,
        };

        using var db = new CursoEFCore.Data.ApplicationContext();
        //db.Produtos.Add(produto);
        //db.Set<Produto>().Add(produto);
        //db.Entry(produto).State = EntityState.Added;
        db.Add(produto);

        var registros = db.SaveChanges();

        Console.WriteLine($"Total de registros: {registros}");

    }
}