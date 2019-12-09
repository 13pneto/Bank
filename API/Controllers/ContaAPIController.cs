using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Repository;

namespace API.Controllers
{
    [Route("api/Conta")]
    [ApiController]
    public class ContaAPIController : Controller
    {
        private readonly MovimentacaoDAO _movimentacaoDAO;
        private readonly ContaDAO _contaDAO;
        //private readonly MovimentacaoDAO _movimentacaoDAO;

        public ContaAPIController(MovimentacaoDAO movimentacaoDAO, ContaDAO contaDAO)
        {
            _movimentacaoDAO = movimentacaoDAO;
            _contaDAO = contaDAO;
        }

        ///api/Conta/ListarTodos
        [Route("ListarTodos")]
        [HttpGet]
        public IActionResult ListarTodos()
        {
            return Ok(_contaDAO.ListarTodos());
        }

        [Route("BuscarContaPorId/{id}")]
        [HttpGet]
        public IActionResult BuscarPorId([FromRoute]int id)
        {
            Conta conta = _contaDAO.BuscarPorId(id);

            if (conta != null)
            {
                return Ok(conta);
            }
            return NotFound();
        }

        [Route("Cadastrar")]
        [HttpPost]
        public IActionResult Cadastrar([FromBody] Conta conta)
        {
            if (ModelState.IsValid)
            {
                if (_contaDAO.Cadastrar(conta))
                {
                    return Created("", conta);
                }
                return Conflict(new { msg = "Essa conta já foi cadastrada!" });
            }
            return BadRequest();
        }

    }
}