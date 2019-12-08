using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Repository;
using Domain;
using Newtonsoft.Json;

namespace Bank.Controllers
{
    public class ContasRetorno : Controller
    {
        private readonly Context _context;
        private readonly ContaDAO _contaDAO;
        private readonly ContaClienteDAO _contaClienteDAO;
        Domain.ContasRetorno conta;

        public ContasRetorno(Context context, ContaDAO contaDAO, ContaClienteDAO contaClienteDAO)
        {
            _contaClienteDAO = contaClienteDAO;
            _contaDAO = contaDAO;
            _context = context;
            //if (conta == null)
            //{
            //    conta = new Domain.ContasRetorno();
            //}
        }

        public IActionResult Index()
        {
            Domain.ContasRetorno cc = new Domain.ContasRetorno();
            return View();

            

        }

        #region Transferencia

        public async Task<IActionResult> BuscarContaClienteOrigem(Domain.ContasRetorno c)
        {
            conta.C1 = _contaClienteDAO.BuscarPorId(c.C1.IdContaCliente);


            //c = _contaClienteDAO.BuscarPorId(c.IdContaCliente);

            if (c.C1 == null)
            {
                ModelState.AddModelError("", "Conta não encontrada!");
            }

            TempData["ContaEncontrada"] = JsonConvert.SerializeObject(c.C1);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Transferencia));
        }

        public async Task<IActionResult> BuscarContaClienteDestino(Domain.ContasRetorno c)
        {


            c.C2 = _contaClienteDAO.BuscarPorId(c.C2.IdContaCliente);

            if (c.C2 == null)
            {
                ModelState.AddModelError("", "Conta não encontrada!");
            }

            TempData["ContaEncontradaDestino"] = JsonConvert.SerializeObject(c.C2);

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Transferencia));
        }

        public IActionResult Transferencia()

        {

            if (TempData["ContaEncontrada"] != null)
            {
                
                conta.C1 = JsonConvert.DeserializeObject<ContaCliente>(TempData["ContaEncontrada"].ToString());
                return View(conta);
            }

            if (TempData["ContaEncontradaDestino"] != null)
            {
                conta.C2 = JsonConvert.DeserializeObject<ContaCliente>(TempData["ContaEncontradaDestino"].ToString());
                return View(conta);
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Transferencia(int IdContaOrigem, int IdContaDestino, double ValorTransf)
        {
            var contaOrigem = _contaClienteDAO.BuscarPorId(IdContaOrigem);
            var contaDestino = _contaClienteDAO.BuscarPorId(IdContaDestino);

            if (contaOrigem == null && contaOrigem == contaDestino)
            {
                ModelState.AddModelError("", "Conta de origem não encontrada!");

                if (contaDestino == null)
                {
                    ModelState.AddModelError("", "Conta de destino não foi encontrada!");
                }
                else
                {
                    var isValid = _contaClienteDAO.RealizarTransferencia(contaOrigem, contaDestino, ValorTransf);

                    if (isValid)
                    {
                        _context.SaveChangesAsync();
                        return RedirectToAction(nameof(Index));
                    }
                }
            }
            return View();
        }

        #endregion
    }
}