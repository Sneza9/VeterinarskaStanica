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
    public class PregledController : ControllerBase
    {
        public VetStanicaContext Context { get; set; }
        public PregledController(VetStanicaContext context)
        {
            Context = context;
        }

        #region ProcitajMesece 
        //Vraca sve Mesece 
        [Route("ProcitajMesece")]
        [HttpGet]
        public async Task<ActionResult> ProcitajMesece()
        {
            return Ok(await Context.Pregledi.Select(p =>
            new
            {
                ID = p.ID,
                Mesec = p.Mesec
            }).ToArrayAsync());
        }
        #endregion 

        #region DodajMesec 
        [Route("DodajMesec/{mesec}")]
        [HttpPost]
        public async Task<ActionResult> DodajMesec(string mesec)
        {
            //Provere 
            if (string.IsNullOrWhiteSpace(mesec) || mesec.Length > 10)
            {
                return BadRequest("Pogresno ime meseca!");
            }
            try
            {
                Pregled pregled = new Pregled
                {
                    Mesec = mesec
                };

                Context.Pregledi.Add(pregled);
                //Salju se promene u bazi podataka 
                await Context.SaveChangesAsync();
                return Ok($"Mesec je uspesno dodat, ID: {pregled.ID}");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        #endregion 

        #region DodajLek/Dijagnozu 

        [Route("DodajLek/{brojKartona}/{idVeterinara}/{idPregleda}/{lek}")]
        [HttpPost]
        public async Task<ActionResult> DodajLek(int brojKartona, int idVeterinara, int idPregleda, string lek)
        {
            //Provere
            if (idVeterinara <= 0)
            {
                return BadRequest("Pogresan ID veterinara!");
            }
            if (idPregleda <= 0)
            {
                return BadRequest("Pogresan ID pregleda!");
            }
            if (string.IsNullOrWhiteSpace(lek) || lek.Length > 50)
            {
                return BadRequest("Pogresno unet lek!");
            }
            try
            {
                var zivotinja = await Context.Zivotinje.Where(p => p.BrojKartona == brojKartona).FirstOrDefaultAsync();
                var Veterinar = await Context.Veterinari.Where(p => p.ID == idVeterinara).FirstOrDefaultAsync();
                //Kada je zivotinja obavila pregled (mesec)
                var pregled = await Context.Pregledi.Where(p => p.ID == idPregleda).FirstOrDefaultAsync();

                //Kreira se jedan unos u tabeli Veza 
                Veza v = new Veza
                {
                    Zivotinja = zivotinja,
                    Veterinar = Veterinar,
                    Pregled = pregled,
                    Lek = lek
                };
                //Salju se promene u bazi podataka 
                Context.ZivotinjeVeterinari.Add(v); 
                await Context.SaveChangesAsync();

                var zivotinje = Context.ZivotinjeVeterinari
                            .Include(p => p.Zivotinja)
                            .ThenInclude(p => p.VrstaZivotinje)
                            .Include(p => p.Pregled)
                            .Include(p => p.Veterinar)
                            .Where(p => p.Zivotinja.BrojKartona == brojKartona);
                var z = await zivotinje.ToListAsync();
                return Ok(
                    z.Select(p =>
                    new
                    {
                        BrojKartona = p.Zivotinja.BrojKartona,
                        ImeZivotinje = p.Zivotinja.ImeZivotinje,
                        ImeVlasnika = p.Zivotinja.ImeVlasnika,
                        BrojTelefonaVlasnika = p.Zivotinja.BrojTelefonaVlasnika,
                        Vrsta = p.Zivotinja.VrstaZivotinje.Vrsta,
                        VeterinarIme = p.Veterinar.Ime,
                        VeterinarPrezime = p.Veterinar.Prezime,
                        Mesec = p.Pregled.Mesec,
                        Lek = p.Lek
                    }).ToList()
                );
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        #endregion 

        #region AzurirajLek 
        [Route("AzurirajLek/{id}/{lek}")]
        [HttpPut]
        public async Task<ActionResult> AzurirajLek(int id, string lek)
        {
            //Provere 
            if (string.IsNullOrWhiteSpace(lek) || lek.Length > 50)
            {
                return BadRequest("Pogresno unet lek!");
            }
            //Azuriranje  
            try
            {
                //Pretrazujemo po id-ju  
                var pregled = Context.ZivotinjeVeterinari.Where(p => p.ID == id).FirstOrDefault();
                if (pregled != null)
                {
                    //Update u Modelu 
                    pregled.Lek = lek;
                }
                //Salju se promene u bazi podataka 
                await Context.SaveChangesAsync();
                return Ok("Lek je uspesno azuriran!");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        #endregion 

        #region ObrisiPregled 
        [Route("ObrisiPregled")]
        [HttpDelete]
        public async Task<ActionResult> ObrisiPregled(int id)
        {
            //Provere
            if (id < 0)
            {
                return BadRequest("Pogresan id pregleda!");
            }
            //Brisanje 
            try
            {
                var pregled = await Context.ZivotinjeVeterinari.FindAsync(id);
                var lek = pregled.Lek;
                Context.ZivotinjeVeterinari.Remove(pregled);
                await Context.SaveChangesAsync();
                return Ok($"Uspesno obrisan pregled, lek je bio: {lek}");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        #endregion 
    }
}