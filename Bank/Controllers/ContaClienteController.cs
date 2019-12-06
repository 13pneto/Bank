using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Domain;
using Repository;
using Newtonsoft.Json;

namespace Bank.Controllers
{
    public class ContaClienteController : Controller
    {
        private readonly Context _context;
        private readonly ContaDAO _contaDAO;
        private readonly ContaClienteDAO _contaClienteDAO;

        public ContaClienteController(Context context, ContaDAO contaDAO, ContaClienteDAO contaClienteDAO)
        {
            _contaClienteDAO = contaClienteDAO;
            _contaDAO = contaDAO;
            _context = context;
        }

        #region Defaults
        // GET: ContaCliente
        public async Task<IActionResult> Index()
        {
            return View(await _context.ContaClientes.ToListAsync());
        }

        // GET: ContaCliente/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contaCliente = await _context.ContaClientes
                .FirstOrDefaultAsync(m => m.IdContaCliente == id);
            if (contaCliente == null)
            {
                return NotFound();
            }

            return View(contaCliente);
        }

        // GET: ContaCliente/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ContaCliente/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdContaCliente,Limite,Saldo,Status,CriadoEm")] ContaCliente contaCliente)
        {
            if (ModelState.IsValid)
            {
                _context.Add(contaCliente);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(contaCliente);
        }

        // GET: ContaCliente/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contaCliente = await _context.ContaClientes.FindAsync(id);
            if (contaCliente == null)
            {
                return NotFound();
            }
            return View(contaCliente);
        }

        // POST: ContaCliente/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ContaCliente contaCliente)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(contaCliente);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ContaClienteExists(contaCliente.IdContaCliente))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(contaCliente);
        }

        // GET: ContaCliente/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contaCliente = await _context.ContaClientes
                .FirstOrDefaultAsync(m => m.IdContaCliente == id);
            if (contaCliente == null)
            {
                return NotFound();
            }

            return View(contaCliente);
        }

        // POST: ContaCliente/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int IdContaCliente)
        {
            var contaCliente = await _context.ContaClientes.FindAsync(IdContaCliente);
            _context.ContaClientes.Remove(contaCliente);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ContaClienteExists(int id)
        {
            return _context.ContaClientes.Any(e => e.IdContaCliente == id);
        }



        #endregion

        #region Saque

        public async Task<IActionResult> BuscarContaClienteSaque(ContaCliente c)
        {
            c = _contaClienteDAO.BuscarPorId(c.IdContaCliente);

            if (c == null)
            {
                ModelState.AddModelError("", "Conta não encontrada!");
            }

            TempData["ContaEncontrada"] = JsonConvert.SerializeObject(c);

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Saque));
        }

        public IActionResult Saque()
        {
            ContaCliente c;

            if (TempData["ContaEncontrada"] != null)
            {
                c = new ContaCliente();
                c = JsonConvert.DeserializeObject<ContaCliente>(TempData["ContaEncontrada"].ToString());
                return View(c);
            }
            return View();

            //var conta = _contaClienteDAO.BuscarPorId(IdContaCliente);

            //if (conta != null)
            //{
            //    if (_contaClienteDAO.RealizaSaque(conta, ValorSaque))
            //    {
            //        return RedirectToAction(nameof(Index));
            //    }
            //}
            //else
            //{
            //    ModelState.AddModelError("", "Conta não encontrada!");
            //}
            //return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Saque(ContaCliente conta, double valorSaque)
        {
            conta = _contaClienteDAO.BuscarPorId(conta.IdContaCliente);

            if (conta == null)                                          //Verificar se a conta foi encontrada
            {
                ModelState.AddModelError("", "Favor informar a conta!");
            }

            else
            {

                if (ModelState.IsValid)
                {

                    if (valorSaque > conta.Saldo)
                    {
                        ModelState.AddModelError("", "Não é possivel sacar, saldo insuficiente!");
                    }
                    else
                    {
                        _contaClienteDAO.RealizaSaque(conta, valorSaque);
                        await _context.SaveChangesAsync();
                        return RedirectToAction(nameof(Index));
                    }
                }
            }

            return View(conta);
        }

        #endregion

        public IActionResult Deposito(int IdContaCliente, double ValorDeposito)
        {
            var conta = _contaClienteDAO.BuscarPorId(IdContaCliente);

            if (conta != null)
            {
                conta.Saldo += ValorDeposito;
            }
            else
            {
                ModelState.AddModelError("", "Não foi possível realizar o depósito!");
            }
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Transferencia(int IdContaOrigem, int IdContaDestino, double ValorTransf)
        {
            var contaOrigem = _contaClienteDAO.BuscarPorId(IdContaOrigem);
            var contaDestino = _contaClienteDAO.BuscarPorId(IdContaDestino);

            double saldoContaOrigem = contaOrigem.Saldo;

            if (contaOrigem != null && contaDestino != null)
            {
                if (ValorTransf > saldoContaOrigem)
                {
                    ModelState.AddModelError("", "Não foi possível realizar o depósito!");
                }

                contaOrigem.Saldo -= ValorTransf;
                contaDestino.Saldo += ValorTransf;
            }
            else
            {
                ModelState.AddModelError("", "Não foi possível encontrar uma das contas!");
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
