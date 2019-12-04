﻿using System;
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
    public class BoletoController : Controller
    {
        private readonly Context _context;
        private readonly ContaClienteDAO _contaClienteDAO;
        private readonly BoletoDAO _boletoDAO;
        public BoletoController(Context context, ContaClienteDAO contaClienteDAO, BoletoDAO boletoDAO)
        {
            _context = context;
            _contaClienteDAO = contaClienteDAO;
            _boletoDAO = boletoDAO;
        }

        // GET: Boleto
        public async Task<IActionResult> Index()
        {
            List<Boleto> boletos = _boletoDAO.ListarTodos();
            return View(boletos);
        }

        // GET: Boleto/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var boleto = await _context.Boletos
                .FirstOrDefaultAsync(m => m.IdBoleto == id);
            if (boleto == null)
            {
                return NotFound();
            }

            return View(boleto);
        }

        // GET: Boleto/Create
        //public IActionResult Create()
        //{
        //    return View();
        //}


        //POST: BUSCAR CONTA
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> BuscarConta(Boleto b)
         {
            b.ContaOrigem = _contaClienteDAO.BuscarPorId(b.ContaOrigem.IdContaCliente);

            if (b.ContaOrigem == null)
            {
                ModelState.AddModelError("", "Conta não encontrada!");
            }

            TempData["ContaOrigem"] = JsonConvert.SerializeObject(b.ContaOrigem);

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Create));
        }

        public IActionResult Create()
        {
            Boleto b;
           
            if (TempData["ContaOrigem"] != null)
            {
                b = new Boleto();
                b.DtVencimento = DateTime.Now.AddDays(1);
                b.ContaOrigem = JsonConvert.DeserializeObject<ContaCliente>(TempData["ContaOrigem"].ToString());
                return View(b);
            }
            return View();
        }


        // POST: Boleto/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Boleto boleto)
        {
            boleto.ContaOrigem = _contaClienteDAO.BuscarPorId(boleto.ContaOrigem.IdContaCliente);
            if (ModelState.IsValid)
            {

                _context.Add(boleto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(boleto);
        }

        // GET: Boleto/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var boleto = await _context.Boletos.FindAsync(id);
            if (boleto == null)
            {
                return NotFound();
            }
            return View(boleto);
        }

        // POST: Boleto/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Boleto boleto)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(boleto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BoletoExists(boleto.IdBoleto))
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
            return View(boleto);
        }

        // GET: Boleto/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var boleto = await _context.Boletos
                .FirstOrDefaultAsync(m => m.IdBoleto == id);
            if (boleto == null)
            {
                return NotFound();
            }

            return View(boleto);
        }

        // POST: Boleto/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var boleto = await _context.Boletos.FindAsync(id);
            _context.Boletos.Remove(boleto);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BoletoExists(int id)
        {
            return _context.Boletos.Any(e => e.IdBoleto == id);
        }
    }
}
