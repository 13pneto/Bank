using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Repository;

namespace API.Controllers
{
    [Route("api/Boleto")]
    [ApiController]
    public class BoletoAPIController : Controller
    {
        private readonly ContaClienteDAO _contaClienteDAO;
        private readonly BoletoDAO _boletoDAO;

        public BoletoAPIController(ContaClienteDAO contaClienteDAO, BoletoDAO boletoDAO)
        {
            _contaClienteDAO = contaClienteDAO;
            _boletoDAO = boletoDAO;
        }

        ///api/Boleto/ListarTodos
        [Route("ListarTodos")]
        [HttpGet]
        public IActionResult ListarTodos()
        {
            return Ok(_contaClienteDAO.ListarTodos());
        }

        [Route("BuscarPorId/{id}")]
        [HttpGet]
        public IActionResult BuscarPorId([FromRoute]int id)
        {
            Boleto boleto = _boletoDAO.BuscarPorId(id);

            if (boleto != null)
            {
                return Ok(boleto);
            }
            return NotFound();
        }

        [Route("Cadastrar")]
        [HttpPost]
        public IActionResult Cadastrar([FromBody] Boleto b)
        {
            if (ModelState.IsValid)
            {
                if (_boletoDAO.Cadastrar(b))
                {
                    return Created("", b);
                }
                return Conflict(new { msg = "Esse boleto já foi cadastrado!" });
            }
            return BadRequest();
        }

        [Route("Efetivar/{id}")]
        [HttpGet]
        public IActionResult Efetivar([FromRoute]int id)
        {
            Boleto boleto = _boletoDAO.BuscarPorId(id);
            if (boleto.Status == "PG")
            {
                return NotFound();
            }
            else if (boleto.Status == "ES")
            {
                return NotFound();
            }
            else
            {

                if (boleto != null)
                {
                    if (_boletoDAO.EfetivarBoletoAPI(boleto))
                    {
                        return Ok(boleto);
                    }
                    else
                    {
                        return NotFound();
                    }
                }
                return NotFound();
            }
        }

        [Route("Estornar/{id}")]
        [HttpGet]
        public IActionResult Estornar([FromRoute]int id)
        {
            Boleto boleto = _boletoDAO.BuscarPorId(id);
            if (boleto.Status == "NP")
            {
                return NotFound();
            }
            else if (boleto.Status == "ES")
            {
                return NotFound();
            }
            else
            {

                if (boleto != null)
                {
                    if (_boletoDAO.EstornarBoletoAPI(boleto))
                    {
                        return Ok(boleto);
                    }
                    else
                    {
                        return NotFound();
                    }
                }
                return NotFound();
            }
        }

    }
}