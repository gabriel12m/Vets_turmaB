using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Vets.Data;
using Vets.Models;

namespace Vets.Controllers
{
    public class VeterinariosController : Controller
    {
        private readonly VetsDB db;

        public VeterinariosController(VetsDB context)
        {
            db = context;
        }

        // GET: Veterinarios
        public async Task<IActionResult> Index()
        {
            return View(await db.Veterinarios.ToListAsync());
        }

        // GET: Veterinarios/Details/5
        /// <summary>
        /// Mostra os detalhes de um Veterinário
        /// Se houverem, mostra os detalhes das consultas associadas a ele
        /// Pesquisa feita em modo 'Eager Loading'
        /// </summary>
        /// <param name="id">Identificador do Veterinário</param>
        /// <returns></returns>
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // em SQL, a pesquisa serie esta:
            // SELECT *
            // FROM Veterinarios v, Animais a, Donos d, Consultas c
            // WHERE c.AnimalFK = id = a.ID AND
            //       c.VeterinarioFK = v.ID AND
            //       a.DonoFK = d.ID AND
            //       v.ID = id

            // acesso aos dados em modo 'Eager Loading'
            var veterinario = await db.Veterinarios
                                    .Include(v => v.Consultas)
                                    .ThenInclude(a => a.Animal)
                                    .ThenInclude(d => d.Dono)
                                    .FirstOrDefaultAsync(v => v.ID == id);
            if (veterinario == null)
            {
                return NotFound();
            }

            return View(veterinario);
        }

        // GET: Veterinarios/Create
        public IActionResult Create()
        {
            return View();
        }

        // GET: Veterinarios/Details2/5
        /// <summary>
        /// Mostra os detalhes de um Veterinário
        /// Se houverem, mostra os detalhes das consultas associadas a ele
        /// Pesquisa feita em modo 'Lazy Loading'
        /// </summary>
        /// <param name="id">Identificador do Veterinário</param>
        /// <returns></returns>
        public async Task<IActionResult> Details2(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // em SQL, a pesquisa serie esta:
            // SELECT *
            // FROM Veterinarios v
            // WHERE v.ID = id

            // acesso aos dados em modo 'Lazy Loading'
            var veterinario = await db.Veterinarios.FirstOrDefaultAsync(v => v.ID == id);
            // necessário adicionar o termo 'virtual' aos relacionamentos
            //
            // necessário adicionar um novo pacote (package)
            // Install-Package Microsoft.EntityFrameworkCore.Proxies
            //
            // dar a ordem ao programa para usar este serviço
            // no ficheiro 'startup.cs' adicionar esta funcionalidade à BD
            //          services.AddDbContext<VetsDB>(options => options
            //                              .UseSqlServer(Configuration.GetConnectionString("ConnectionDB"))
            //                              .UseLazyLoadingProxies() // ativamos a opção do Lazy Loading
            //                              );



            if (veterinario == null)
            {
                return NotFound();
            }

            return View(veterinario);
        }


        // POST: Veterinarios/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Nome,NumCedulaProf,Fotografia")] Veterinarios veterinario, IFormFile fotoVet)
        {
            // processar a fotografia


            if (ModelState.IsValid)
            {
                db.Add(veterinario);
                await db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(veterinario);
        }

        // GET: Veterinarios/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var veterinario = await db.Veterinarios.FindAsync(id);
            if (veterinario == null)
            {
                return NotFound();
            }
            return View(veterinario);
        }

        // POST: Veterinarios/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Nome,NumCedulaProf,Fotografia")] Veterinarios veterinario)
        {
            if (id != veterinario.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    db.Update(veterinario);
                    await db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VeterinariosExists(veterinario.ID))
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
            return View(veterinario);
        }

        // GET: Veterinarios/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var veterinario = await db.Veterinarios
                .FirstOrDefaultAsync(m => m.ID == id);
            if (veterinario == null)
            {
                return NotFound();
            }

            return View(veterinario);
        }

        // POST: Veterinarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var veterinario = await db.Veterinarios.FindAsync(id);
            db.Veterinarios.Remove(veterinario);
            await db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VeterinariosExists(int id)
        {
            return db.Veterinarios.Any(e => e.ID == id);
        }
    }
}
