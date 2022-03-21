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
    public class VeterinarController : ControllerBase
    {
        public VetStanicaContext Context { get; set; }
        public VeterinarController(VetStanicaContext context)
        {
            Context = context;
        }

        #region ProcitajVeterinara   
        [Route("ProcitajVeterinare")]
        [HttpGet]
        public async Task<ActionResult> ProcitajVeterinare()
        {
            return Ok(await Context.Veterinari.Select(p =>
            new
            {
                ID = p.ID,
                Ime = p.Ime,
                Prezime = p.Prezime,
                TipStruke=p.TipStrukeVeterinara
            }).ToArrayAsync());
        }
        #endregion 

        #region DodajVeterinara 
        [Route("DodajVeterinara/{ime}/{prezime}/{brojTelefona}/{idTipStruke}")]
        [HttpPost]
        public async Task<ActionResult> DodajVeterinara(string ime, string prezime, string brojTelefona, int idTipStruke)
        {
            //Provere 
            if (string.IsNullOrWhiteSpace(ime) || ime.Length > 50)
            {
                return BadRequest("Pogresno ime veterinara!");
            }
            if (string.IsNullOrWhiteSpace(prezime) || prezime.Length > 50)
            {
                return BadRequest("Pogresno prezime veterinara!");
            }
            if (string.IsNullOrWhiteSpace(brojTelefona) || brojTelefona.Length > 15)
            {
                return BadRequest("Pogresan broj telefona!");
            }
            if (idTipStruke < 0)
            {
                return BadRequest("Pogresan id tipa struke!");
            }
            //Dodavanje veterinara 
            try
            {
                var tipStruke = await Context.TipoviStruke.Where(p => p.ID == idTipStruke).FirstOrDefaultAsync();
                Veterinar veterinar = new Veterinar{
                    Ime=ime,
                    Prezime=prezime,
                    BrojTelefona=brojTelefona,
                    TipStrukeVeterinara = tipStruke
                };
                Context.Veterinari.Add(veterinar);
                await Context.SaveChangesAsync();
                return Ok($"Veterinar je uspesno dodat, ID: {veterinar.ID}!");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        #endregion 

        #region AzurirajVeterinara 
        [Route("AzurirajVeterinara/{brojTelefona}")]
        [HttpPut]
        public async Task<ActionResult> AzurirajVeterinara(int id, string brojTelefona)
        {
            //Provere 
            if (id < 0)
            {
                return BadRequest("Pogresan id!");
            }
            if (string.IsNullOrWhiteSpace(brojTelefona) || brojTelefona.Length > 15)
            {
                return BadRequest("Pogresan broj telefona!");
            }
            //Azurianje 
            try
            {
                var veterinar = Context.Veterinari.Where(p => p.ID == id).FirstOrDefault();
                if (veterinar != null)
                {
                    //Radimo update u Modelu 
                    veterinar.BrojTelefona = brojTelefona;

                    //Saljemo promene u bazi podataka 
                    await Context.SaveChangesAsync();
                    return Ok($"Veterinar je uspesno azuriran, ID: {veterinar.ID}!");
                }
                else
                {
                    return BadRequest("Veterinar nije pronadjen!");
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        //Azuriranje celog veterinara 
        [Route("AzurirajVeterinaraSve")]
        [HttpPut]
        public async Task<ActionResult> AzurirajVeterinaraSve([FromBody] Veterinar veterinar)
        {
            //Provere 
            if (string.IsNullOrWhiteSpace(veterinar.Ime) || veterinar.Ime.Length > 50)
            {
                return BadRequest("Pogresno ime veterinara!");
            }
            if (string.IsNullOrWhiteSpace(veterinar.Prezime) || veterinar.Prezime.Length > 50)
            {
                return BadRequest("Pogresno prezime veterinara!");
            }
            if (string.IsNullOrWhiteSpace(veterinar.BrojTelefona) || veterinar.BrojTelefona.Length > 15)
            {
                return BadRequest("Pogresan broj telefona!");
            }
            //Azuriranje 
            try
            {
                Context.Veterinari.Update(veterinar);
                await Context.SaveChangesAsync();
                return Ok($"Veterinar je uspesno azuriran, ID: {veterinar.ID}!");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        #endregion 

        #region ObrisiVeterinara
        //Obrisi veterinara 
        [Route("ObrisiVeterinara")]
        [HttpDelete]
        public async Task<ActionResult> ObrisiVeterinara(int id)
        {
            //Provere
            if (id < 0)
            {
                return BadRequest("Pogresan id veterinara!");
            }
            //Brisanje 
            try
            {
                var veterinar = await Context.Veterinari.FindAsync(id);
                var imeVeterinara = veterinar.Ime;
                var prezimeVeterinara = veterinar.Prezime;
                Context.Veterinari.Remove(veterinar);
                await Context.SaveChangesAsync();
                return Ok($"Uspesno obrisan veterinar! Ime i prezime veterinara su bili: {imeVeterinara} i {prezimeVeterinara}");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        #endregion 

    }
}