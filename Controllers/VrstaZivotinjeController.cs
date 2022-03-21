using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;

namespace VeterinarskaStanica.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class VrstaZivotinjeController : ControllerBase
    {
        public VetStanicaContext Context { get; set; }
        public VrstaZivotinjeController(VetStanicaContext context)
        {
            Context = context;
        }

        #region ProcitajVrsteZivotinja   
        [Route("ProcitajVrsteZivotinja")]
        [HttpGet]
        public async Task<ActionResult> ProcitajVrsteZivotinja()
        { 
            return Ok(await Context.VrsteZivotinja.Select(p =>
            new
            {
                ID = p.ID,
                Vrsta = p.Vrsta
            }).ToArrayAsync());
        }
        #endregion 

        #region DodajVrstuZivotinje
        [Route("DodajVrstuZivotinje/{vrsta}")]
        [HttpPost]
        public async Task<ActionResult> DodajVrstuZivotinje(string vrsta)
        {
            //Proverava da li je ime null ili white space ili duzina veca od 50  
            if (string.IsNullOrWhiteSpace(vrsta) || vrsta.Length > 50)
            {
                return BadRequest("Pogresna vrsta zivotinje!");
            }
            try
            {
                VrstaZivotinje v = new VrstaZivotinje{
                    Vrsta=vrsta
                };
                Context.VrsteZivotinja.Add(v);
                await Context.SaveChangesAsync(); 
                return Ok($"Vrsta zivotinje je uspesno dodata, ID: {v.ID}!");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        #endregion

        #region AzurirajVrstuZivotinje 
        [Route("AzurirajVrstuZivotinje/{vrsta}")]
        [HttpPut]
        public async Task<ActionResult> AzurirajVrstuZivotinje(int id, string vrsta)
        {
            //Provere 
            if (id < 0)
            {
                return BadRequest("Pogresan id!");
            }
            if (string.IsNullOrWhiteSpace(vrsta) || vrsta.Length > 40)
            {
                return BadRequest("Pogresna vrsta zivotinje!");
            }
            //Azurianje 
            try
            {
                var vrstaZivotinje = Context.VrsteZivotinja.Where(p => p.ID == id).FirstOrDefault();
                if (vrstaZivotinje != null)
                { 
                    vrstaZivotinje.Vrsta = vrsta;

                    //Saljemo promene u bazi podataka 
                    await Context.SaveChangesAsync();
                    return Ok($"Vrsta zivotinje je uspesno azurirana, ID: {vrstaZivotinje.ID}!"); 
                }
                else
                {
                    return BadRequest("Vrsta zivotinje nije pronadjena!"); 
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        #endregion 

        #region ObrisiVrstuZivotinje
        [Route("ObrisiVrstuZivotinje")]
        [HttpDelete]
        public async Task<ActionResult> ObrisiVrstuZivotinje(int id)
        {
            //Provere
            if (id < 0)
            {
                return BadRequest("Pogresan id vrste!");
            }
            //Brisanje 
            try
            {
                var vrstaZivotinje = await Context.VrsteZivotinja.FindAsync(id);
                var vrsta = vrstaZivotinje.Vrsta;
                Context.VrsteZivotinja.Remove(vrstaZivotinje);
                await Context.SaveChangesAsync();
                return Ok($"Uspesno obrisana vrsta zivotinje: {vrsta}"); 
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        #endregion 
    }
}