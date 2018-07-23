using Solution.Services.WebServices.Ext.Models.Clientes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace Solution.Services.WebServices.Ext.Controllers
{
    public class Cliente
    {
        public int Codigo { get; set; }
        public string Nome { get; set; }
        public string cpf_cnpj { get; set; }
        public DateTime DataCadastro { get; set; }
        public List<Produto> Produtos { get; set; }
    }

    public class Produto
    {
        public int Codigo { get; set; }
        public string Nome { get; set; }
    }

    public static class ProdutoRepository
    {

    }


    public static class ClienteRepository
    {
        private static List<Cliente> _clientes;

        public static List<Cliente> Clientes
        {
            get
            {
                if (_clientes == null)
                    Init();

                return _clientes;
            }
            set
            {
                if (_clientes == null)
                    Init();

                _clientes = value;
            }
        }

        private static void Init()
        {
            _clientes = new List<Cliente>();
            Clientes.Add(new Cliente
            {
                Codigo = 1,
                Nome = "Vinicius",
                cpf_cnpj = "123",
                DataCadastro = DateTime.Now,
                Produtos = new List<Produto>() {
                    new Produto { Codigo = 1, Nome = "CRM" },
                    new Produto { Codigo = 2, Nome = "CRM VENDAS" }
                }
            });
            Clientes.Add(new Cliente{ Codigo = 2, Nome = "Leonardo", cpf_cnpj = "456", DataCadastro = DateTime.Now,
                Produtos = new List<Produto>() {
                    new Produto { Codigo = 1, Nome = "CRM" },
                    new Produto { Codigo = 3, Nome = "CHAT" }
                }
            });
        }
    }


    public class ClientesController : ApiController
    {
        [HttpGet]
        [Route("clientes/{nome?}")]
        public HttpResponseMessage Get(string nome = null)
        {
            var filtro = ClienteRepository.Clientes.AsEnumerable();

            if (nome != null)
            {
                filtro = filtro.Where(c => c.Nome == nome);
            }

            var result = filtro.Select(c => new { c.Codigo, c.cpf_cnpj, c.DataCadastro, c.Nome }).ToList();

            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        [HttpGet]
        [Route("clientes/{codigo}")]
        public HttpResponseMessage Get([FromUri] int codigo)
        {
            var data = ClienteRepository.Clientes.Where(c => c.Codigo == codigo);

            return Request.CreateResponse(HttpStatusCode.OK, data);
        }

        [HttpGet]
        [Route("clientes/{codigo}/produtos/{nome?}")]
        public HttpResponseMessage GetProducts([FromUri] int codigo, [FromUri] string nome = null)
        {
            var data = ClienteRepository.Clientes.Where(c => c.Codigo == codigo).Single().Produtos;

            if (nome != null)
            {
                data = data.Where(p => p.Nome == nome).ToList();
            }

            return Request.CreateResponse(HttpStatusCode.OK, data);
        }


        [HttpPost]
        [AcceptVerbs("Post")]
        [Route("clientes/")]
        public HttpResponseMessage Post(
            [FromBody] PostIn postin)
        {

            var cliente = new Cliente()
            {
                Codigo = ClienteRepository.Clientes.Count + 1,
                Nome = postin.Nome,
                cpf_cnpj = postin.CpfCnpj,
                DataCadastro = DateTime.Now
            };

            ClienteRepository.Clientes.Add(cliente);

            return Request.CreateResponse(HttpStatusCode.OK, cliente);
        }

        [HttpPut]
        [Route("clientes/{codigo}")]
        public HttpResponseMessage Put([FromUri] int codigo, [FromBody] PutIn putIn )
        {
            var cliente = ClienteRepository.Clientes.Where(c => c.Codigo == codigo).FirstOrDefault();
            cliente.Nome = putIn.Nome;
            cliente.cpf_cnpj = putIn.CpfCnpj;

            return Request.CreateResponse(HttpStatusCode.OK, cliente);
        }

        [HttpDelete]
        [Route("clientes/{codigo}")]
        public HttpResponseMessage Delete([FromUri] int codigo)
        {
            var cliente = ClienteRepository.Clientes.Where(c => c.Codigo == codigo).FirstOrDefault();
            ClienteRepository.Clientes.Remove(cliente);


            return Request.CreateResponse(HttpStatusCode.OK, new { Message = "Cliente excluido com sucesso!"});
        }

        

    }
}