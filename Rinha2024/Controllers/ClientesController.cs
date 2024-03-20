using Microsoft.AspNetCore.Mvc;
using Rinha2024.Data.Repositories.Interface;
using Rinha2024.Entitdade;
using Rinha2024.Extensions;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Rinha2024.Controllers
{
    [Route("/[controller]")]
    [ApiController]
    public class ClientesController : Controller
    {
        private readonly IClienteRepository _ClienteR;
        private readonly ITransacaoRepository _TransacaoR;

        public ClientesController(IClienteRepository clienteR, ITransacaoRepository transacaoR)
        {
            _ClienteR = clienteR;
            _TransacaoR = transacaoR;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var MachineName = System.Environment.MachineName;
            return Ok(new { Status = "online", MachineName });
        }

        [HttpGet("teste")]
        public IActionResult Index()
        {
            int s = 0;

            s = s - 1;
            return Ok(s);
        }

        [HttpPost("{id}/transacoes")]
        public async Task<IActionResult> RealizarTransacao([FromRoute] int id, [FromBody] RealizarTransacaoDto transacao)
        {
            var cliente = await _ClienteR.GetByIdAsync(id);
            if (cliente == null)
            {
                return NotFound("Cliente não existe");
            }

            if (transacao.Tipo == TipoTransacaoEnum.d)
            {
                if (cliente.Limite - (transacao.Valor - cliente.Saldo) < 0)
                {
                    return UnprocessableEntity("Cliente sem limite para transação");
                }
                await _TransacaoR.CreateAsync(transacao.toTransacaoWithCliente(cliente));
                cliente.Saldo = cliente.Saldo - transacao.Valor;

                await _ClienteR.UpdateAsync(cliente);


                return Ok(new RespostaTransacaoDto
                {
                    Limite = cliente.Limite,
                    Saldo = cliente.Saldo,
                });
            }

            if (transacao.Tipo == TipoTransacaoEnum.c)
            {
                await _TransacaoR.CreateAsync(transacao.toTransacaoWithCliente(cliente));

                cliente.Saldo = cliente.Saldo + transacao.Valor;
                await _ClienteR.UpdateAsync(cliente);


                return Ok(new RespostaTransacaoDto
                {
                    Limite = cliente.Limite,
                    Saldo = cliente.Saldo,
                });
            }
            throw new NotImplementedException();
        }

        [HttpGet("{id}/extrato")]
        public async Task<ActionResult<Extrato>> MostrarExstrato([FromRoute] int id)
        {
            var dataRequisao = DateTime.Now;
            var cliente = await _ClienteR.GetByIdAsync(id);
            if (cliente == null)
            {
                return NotFound("Cliente não existe");
            }
            var result = await _TransacaoR.GetLastTenByClientIdAsync(id);
            var extrato = new Extrato
            {
                Saldo = new Saldo
                {
                    DataExtrato = dataRequisao,
                    Limite = cliente.Limite,
                    Total = cliente.Saldo
                },
                UltimasTransacoes = TransacaoExtension.ToUltimasTransacoes(result)
            };
            return Ok(extrato);
        }


        public class RealizarTransacaoDto
        {
            [Range(1, int.MaxValue, ErrorMessage = "O campo valor não pode ser menor do que '1'")]
            public int Valor { get; set; }
            public TipoTransacaoEnum Tipo { get; set; }
            [Range(1, 10, ErrorMessage = "O campo descricao deve ser do tipo string ou array com comprimento máximo de '10'.")]
            public string Descricao { get; set; }

            public Transacao toTransacaoWithCliente(Cliente cliente)
            {
                return new Transacao
                {
                    Valor = this.Valor,
                    Tipo = this.Tipo,
                    Descricao = this.Descricao,
                    Cliente = cliente,
                    RealizadaEm = DateTime.Now
                };
            }
        }

        public class RespostaTransacaoDto
        {
            public int Limite { get; set; }
            public int Saldo { get; set; }
        }


        public class Extrato
        {
            public Saldo Saldo { get; set; }
            [JsonPropertyName("ultimas_transacoes")]
            public IList<UltimasTransacoes> UltimasTransacoes { get; set; }
        }

        public class Saldo
        {
            public int Total { get; set; }
            [JsonPropertyName("data_extrato")]
            public DateTime DataExtrato { get; set; }
            public int Limite { get; set; }
        }
        public class UltimasTransacoes
        {
            public int Valor { get; set; }
            public TipoTransacaoEnum Tipo { get; set; }
            public string Descricao { get; set; }
            [JsonPropertyName("realizada_em")]
            public DateTime RealizadaEm { get; set; }

        }
    }
}
