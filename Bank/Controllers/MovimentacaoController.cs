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
    public class MovimentacaoController : Controller
    {
        private readonly Context _context;
        private readonly MovimentacaoDAO _movimentacaoDAO;
        private readonly ContaClienteDAO _contaClienteDAO;

        public MovimentacaoController(Context context, MovimentacaoDAO movimentacaoDAO, ContaClienteDAO contaClienteDAO)
        {
            _context = context;
            _movimentacaoDAO = movimentacaoDAO;
            _contaClienteDAO = contaClienteDAO;
        }

        #region Default
        // GET: Movimentacao
        public IActionResult Index()
        {
            return View(_movimentacaoDAO.ListarTodos());
        }

        // GET: Movimentacao/Details/5
        public IActionResult Details(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }

            Movimentacao m = _movimentacaoDAO.BuscarPorId(id);
            if (m == null)
            {
                return NotFound();
            }

            return View(m);
        }

        // GET: Movimentacao/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Movimentacao/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdMovimento,Valor,DtMovimentacao,Status")] Movimentacao movimentacao)
        {
            if (ModelState.IsValid)
            {
                _context.Add(movimentacao);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(movimentacao);
        }

        // GET: Movimentacao/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movimentacao = await _context.Movimentacoes.FindAsync(id);
            if (movimentacao == null)
            {
                return NotFound();
            }
            return View(movimentacao);
        }

        // POST: Movimentacao/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Movimentacao movimentacao)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(movimentacao);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MovimentacaoExists(movimentacao.IdMovimento))
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
            return View(movimentacao);
        }

        // GET: Movimentacao/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movimentacao = await _context.Movimentacoes
                .FirstOrDefaultAsync(m => m.IdMovimento == id);
            if (movimentacao == null)
            {
                return NotFound();
            }

            return View(movimentacao);
        }

        // POST: Movimentacao/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int IdMovimento)
        {
            //Movimentacao m = _movimentacaoDAO.BuscarPorId(IdMovimentacao);
            //var movimentacao = await _context.Movimentacoes.FindAsync(IdMovimentacao);
            //_context.Movimentacoes.Remove(movimentacao);
            _movimentacaoDAO.RemoverPorId(IdMovimento);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MovimentacaoExists(int id)
        {
            return _context.Movimentacoes.Any(e => e.IdMovimento == id);
        }

        #endregion

        #region Extrato
        public IActionResult Conta()
        {
            Movimentacao m;

            if (TempData["ContaEncontrada"] != null)
            {
                m = new Movimentacao();
                m = JsonConvert.DeserializeObject<Movimentacao>(TempData["ContaEncontrada"].ToString());
                return View(m);
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Conta(Movimentacao m)
        {
            List<Movimentacao> extratoCliente = _movimentacaoDAO.ListarTodos(m.ContaOrigem.IdContaCliente);

            if (extratoCliente == null)
            {
                ModelState.AddModelError("", "Cliente sem movimentações.");
            }

            else
            {
                if (ModelState.IsValid)
                {

                    //if (valorSaque > conta.Saldo)
                    //{
                    //    ModelState.AddModelError("", "Não é possivel sacar, saldo insuficiente!");
                    //}
                    //else
                    //{
                    //ViewBag.ExtratoPorCliente = extratoCliente;
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(ExtratoPorCliente));
                    //}
                }
            }
            return View(m);
        }

        [HttpPost]
        public async Task<IActionResult> BuscarContaClienteSaque(Movimentacao m)
        {
            ContaCliente c = _contaClienteDAO.BuscarPorId(m.ContaOrigem.IdContaCliente);

            if (c == null)
            {
                ModelState.AddModelError("", "Conta não encontrada!");
            }

            m.ContaOrigem = c;

            TempData["ContaEncontrada"] = JsonConvert.SerializeObject(m);

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Conta));
        }


        public async Task<IActionResult> ExtratoPorCliente(int id)
        {
            Movimentacao m = new Movimentacao();
            m.ContaOrigem = _contaClienteDAO.BuscarPorId(id);

            List<Movimentacao> extratoCliente = _movimentacaoDAO.ListarTodos(id);
            ViewBag.ExtratoPorCliente = extratoCliente;
            ViewBag.ContaEncontrada = m;
            return View(extratoCliente);
        }

        //[HttpPost, ActionName("ExtratoPorCliente")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> ExtratoPorCliente(Movimentacao m)
        //{
           
        //}


        #endregion
    }
}
