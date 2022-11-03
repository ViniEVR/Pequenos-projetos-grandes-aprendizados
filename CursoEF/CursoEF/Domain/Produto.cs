using CursoEFCore.ValueObjects;
using CursoEFCore.domain;

namespace CursoEFCore.domain
{
    public class Produto 
    {
        public int Id {get; set;}
        public string CodigoDeBarras {get; set;}
        public string Descricao {get; set;}
        public decimal Valor {get; set;}
        public TipoProduto TipoProduto  {get; set;}
        public bool Ativo {get; set;}
    }
}