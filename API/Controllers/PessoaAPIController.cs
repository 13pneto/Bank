using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Repository;

namespace API.Controllers
{
    [Route("api/Pessoa")]
    [ApiController]
    public class PessoaAPIController : Controller
    {
        private readonly MovimentacaoDAO _movimentacaoDAO;
        private readonly PessoaDAO _pessoaDAO;

        public PessoaAPIController(MovimentacaoDAO movimentacaoDAO, PessoaDAO pessoaDAO)
        {
            _movimentacaoDAO = movimentacaoDAO;
            _pessoaDAO = pessoaDAO;
        }

        ///api/Pessoa/ListarTodos
        [Route("ListarTodos")]
        [HttpGet]
        public IActionResult ListarTodos()
        {
            return Ok(_movimentacaoDAO.ListarTodos());
        }

        [Route("BuscarPorId/{id}")]
        [HttpGet]
        public IActionResult BuscarPorId([FromRoute]int id)
        {
            Pessoa pessoa = _pessoaDAO.BuscarPorId(id);

            if (pessoa != null)
            {
                return Ok(pessoa);
            }
            return NotFound();
        }

        [Route("Cadastrar")]
        [HttpPost]
        public IActionResult Cadastrar([FromBody] Pessoa pessoa)
        {
            if (ModelState.IsValid)
            {
                if (_pessoaDAO.Cadastrar(pessoa))
                {
                    return Created("", pessoa);
                }
                return Conflict(new { msg = "Esse pessoa já foi cadastrada!" });
            }
            return BadRequest();
        }

    }
}