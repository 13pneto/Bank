using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Repository;

namespace API.Controllers
{
    [Route("api/ContaCliente")]
    [ApiController]
    public class CadastroAPIController : Controller
    {
        private readonly ContaClienteDAO _contaClienteDAO;

        public CadastroAPIController(ContaClienteDAO contaClienteDAO)
        {
            _contaClienteDAO = contaClienteDAO;
        }

        [Route]






    }
}