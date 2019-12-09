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
        private readonly PessoaDAO _pessoaDAO;
        public ContaCliente contaOrigemGlobal;
        public ContaCliente contaDestinoGlobal;


        public ContaClienteController(Context context, ContaDAO contaDAO, ContaClienteDAO contaClienteDAO, PessoaDAO pessoaDAO)
        {
            _contaClienteDAO = contaClienteDAO;
            _contaDAO = contaDAO;
            _context = context;
            _pessoaDAO = pessoaDAO;
        }

        #region Defaults

        public async Task<IActionResult> BuscarPessoa(ContaCliente c)
        {
            c.Pessoa = _pessoaDAO.BuscarPorId(c.Pessoa.IdCliente);

            if (c.Pessoa == null)
            {
                ModelState.AddModelError("", "Pessoa não encontrada!");
            }

            TempData["PessoaEncontrada"] = JsonConvert.SerializeObject(c);

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Create));
        }

        


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

        public IActionResult Limpar() 
        {
            ModelState.Clear();
            return View();
        }

        // GET: ContaCliente/Create
        public IActionResult Create()
         {
            ContaCliente c;

            List<Conta> contas = _contaDAO.ListarTodos();
            //ViewBag.Contas = new SelectList(contas);
            ViewBag.Contas = contas;

            if (TempData["PessoaEncontrada"] != null)
            {
                c = new ContaCliente();
                c = JsonConvert.DeserializeObject<ContaCliente>(TempData["PessoaEncontrada"].ToString());
                return View(c);
            }
            return View();
        }

        // POST: ContaCliente/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ContaCliente contaCliente, int IdConta)
        {
            Conta c = _contaDAO.BuscarPorId(contaCliente.ContaDoCliente.IdConta);
            Pessoa p = _pessoaDAO.BuscarPorId(contaCliente.Pessoa.IdCliente);
            contaCliente.ContaDoCliente = c;
            contaCliente.Pessoa = p;


            if(contaCliente.Pessoa == null)
            {
                ModelState.AddModelError("", "Pessoa não encontrada!");
            }

            if(contaCliente.ContaDoCliente == null)
            {
                ModelState.AddModelError("", "Tipo de conta não Encontrada/Selecionada!");
            }

            else
            {
                //if (ModelState.IsValid)
                //{
                    _context.Add(contaCliente);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                //}
                
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
            _contaClienteDAO.InativarPessoa(IdContaCliente);    
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

        #region Deposito

        public async Task<IActionResult> BuscarContaClienteDeposito(ContaCliente c)
        {
            c = _contaClienteDAO.BuscarPorId(c.IdContaCliente);

            if (c == null)
            {
                ModelState.AddModelError("", "Conta não encontrada!");
            }

            TempData["ContaEncontrada"] = JsonConvert.SerializeObject(c);

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Deposito));
        }

        public IActionResult Deposito()
        {
            ContaCliente c;

            if (TempData["ContaEncontrada"] != null)
            {
                c = new ContaCliente();
                c = JsonConvert.DeserializeObject<ContaCliente>(TempData["ContaEncontrada"].ToString());
                return View(c);
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Deposito(int IdContaCliente, double ValorDeposito)
        {
            var conta = _contaClienteDAO.BuscarPorId(IdContaCliente);

            if (conta == null)
            {
                ModelState.AddModelError("", "Conta não encontrada!");
            }
            else
            {
                _contaClienteDAO.RealizaDeposito(conta, ValorDeposito);
                _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(conta);
        }

        #endregion

        #region Transferencia

        public async Task<IActionResult> BuscarContaClientes(int ContaOrigemId, int ContaDestinoId)
            {
            var contaOrigem = _contaClienteDAO.BuscarPorId(ContaOrigemId);
            var contaDestino = _contaClienteDAO.BuscarPorId(ContaDestinoId);

            if (contaOrigem == null || contaDestino == null)
            {
                ModelState.AddModelError("", "Conta não encontrada!");
            }

            //ViewBag.contaOrigem = contaOrigem;
            //ViewBag.contaDestino = contaDestino;

            //contaOrigemGlobal = contaOrigem;
            //contaDestinoGlobal = contaDestino;

            TempData["ContaOrigem"] = JsonConvert.SerializeObject(contaOrigem);
            TempData["ContaDestino"] = JsonConvert.SerializeObject(contaDestino);

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Transferencia));
        }

        public IActionResult Transferencia()
        {
            ContaCliente contaOrigem;
            ContaCliente contaDestino;

            if (TempData["ContaOrigem"] != null)
            {
            //    contaOrigem = contaOrigemGlobal;
            //    contaDestino = contaDestinoGlobal;

                contaOrigem = JsonConvert.DeserializeObject<ContaCliente>(TempData["ContaOrigem"].ToString());
                contaDestino = JsonConvert.DeserializeObject<ContaCliente>(TempData["ContaDestino"].ToString());

                ContasRetorno cr = new ContasRetorno();
                cr.C1 = contaOrigem;
                cr.C2 = contaDestino;

                return View(cr);
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
