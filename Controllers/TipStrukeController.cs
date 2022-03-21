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
    public class TiStrukeController : ControllerBase
    {
        public VetStanicaContext Context { get; set; }
        public TiStrukeController(VetStanicaContext context)
        {
            Context = context; 
        }

        #region ProcitajTipStruke  
        [Route("ProcitajTipoveStruke")]
        [HttpGet]
        public async Task<ActionResult> ProcitajTipoveStruke()
        {
            return Ok(await Context.TipoviStruke.Select(p =>
            new
            {
                ID = p.ID,
                Tip = p.Tip
            }).ToArrayAsync());
        }
        #endregion 

        #region DodajTipStruke
        [Route("DodajTipStruke")]
        [HttpPost]
        public async Task<ActionResult> DodajTipStruke([FromBody] TipStruke tipStruke)
        {
            //Proverava da li je ime null ili white space ili duzina veca od 50  
            if (string.IsNullOrWhiteSpace(tipStruke.Tip) || tipStruke.Tip.Length > 50)
            {
                return BadRequest("Pogresan tip struke!");
            }
            try
            {
                Context.TipoviStruke.Add(tipStruke);
                await Context.SaveChangesAsync(); 
                return Ok($"Tip struke je uspesno dodat, ID: {tipStruke.ID}!");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        #endregion

        #region AzurirajTipStruke 
        [Route("AzurirajTipStruke/{tipStuke}")]
        [HttpPut]
        public async Task<ActionResult> AzurirajTipStruke(int id, string tipStuke)
        {
            //Provere 
            if (id < 0)
            {
                return BadRequest("Pogresan id!");
            }
            if (string.IsNullOrWhiteSpace(tipStuke) || tipStuke.Length > 15)
            {
                return BadRequest("Pogresan tip struke!");
            }
            //Azurianje 
            try
            {
                var tipStruke = Context.TipoviStruke.Where(p => p.ID == id).FirstOrDefault();
                if (tipStruke != null)
                {
                    //Update u Modelu 
                    tipStruke.Tip = tipStuke;

                    //Salju se promene u bazi podataka 
                    await Context.SaveChangesAsync();
                    return Ok($"Tip stuke je uspesno azuriran, ID: {tipStruke.ID}!");
                }
                else
                {
                    return BadRequest("Tip struke nije pronadjen!");
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        #endregion  

        #region ObrisiTipStruke
        //Obrisi veterinara 
        [Route("ObrisiTipStruke")]
        [HttpDelete]
        public async Task<ActionResult> ObrisiTipStruke(int id)
        {
            //Provere
            if (id < 0)
            {
                return BadRequest("Pogresan id tipa struke!");
            }
            //Brisanje 
            try
            {
                var tipStruke = await Context.TipoviStruke.FindAsync(id);
                var tip = tipStruke.Tip;
                Context.TipoviStruke.Remove(tipStruke);
                await Context.SaveChangesAsync();
                return Ok($"Uspesno obrisan tip struke: {tip}");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        #endregion 

    }
}