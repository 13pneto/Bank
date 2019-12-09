using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Repository;

namespace API.Controllers
{
    [Route("api/Extrato")]
    [ApiController]
    public class ExtratoAPIController : Controller
    {
        private readonly MovimentacaoDAO _movimentacaoDAO;
        //private readonly MovimentacaoDAO _movimentacaoDAO;

        public ExtratoAPIController(MovimentacaoDAO movimentacaoDAO)
        {
            _movimentacaoDAO = movimentacaoDAO;
        }

        ///api/Extrato/ListarTodos
        [Route("ListarTodos")]
        [HttpGet]
        public IActionResult ListarTodos()
        {
            return Ok(_movimentacaoDAO.ListarTodos());
        }

        [Route("BuscarPorConta/{id}")]
        [HttpGet]
        public IActionResult BuscarPorId([FromRoute]int id)
        {
            List<Movimentacao> movimentacao = _movimentacaoDAO.ListarTodos(id);

            if (movimentacao != null)
            {
                return Ok(movimentacao);
            }
            return NotFound();
        }

        [Route("Cadastrar")]
        [HttpPost]
        public IActionResult Cadastrar([FromBody] Movimentacao m)
        {
            if (ModelState.IsValid)
            {
                if (_movimentacaoDAO.Cadastrar(m))
                {
                    return Created("", m);
                }
                return Conflict(new { msg = "Esse Movimentação já foi cadastrada!" });
            }
            return BadRequest();
        }

    }
}