using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Repository;

namespace Bank.Controllers
{
    public class HomeController : Controller
    {
        public readonly PessoaDAO _pessoaDAO;

        public HomeController(PessoaDAO pessoaDAO)
        {
            _pessoaDAO = pessoaDAO;
        }


        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string Cpf)
        {
            if (_pessoaDAO.BuscarPorCpf(Cpf) != null)
            {
                Pessoa cpfLogin = _pessoaDAO.BuscarPorCpf(Cpf);
                return View(cpfLogin);
            }
            ModelState.AddModelError("", "CPF não cadastrado!");
            return View();
        }


    }
}