using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;

//Zivotinja
namespace VeterinarskaStanica.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class VetStanicaController : ControllerBase
    {
        public VetStanicaContext Context { get; set; }
        public VetStanicaController(VetStanicaContext context)
        {
            Context = context;
        }

        #region ProcitajZivotinju

        [Route("ProcitajZivotinje/{veterinarID}/{meseci}/{vrstaID}")]
        [HttpGet]
        public async Task<ActionResult> ProcitajZivotinje(int veterinarID, string meseci, int vrstaID)
        {
            try
            {
                //String konkateniran sa m se splituje i parsuje u int, dobija se niz integer-a tj 
                //Meseci kada su se obavljali pregledi 
                var mesecID = meseci.Split('m')
                                    .Where(x => int.TryParse(x, out _))
                                    .Select(int.Parse)
                                    .ToList();

                //Izdvaja se vrsta zivotinje koja je prosledjena (id) 
                var vrsta = await Context.VrsteZivotinja.Where(p => p.ID == vrstaID).FirstOrDefaultAsync();

                //Eager Loading 
                //Izdvaja se trazena zivotinja, ne izvlace se sve zivotinje 
                var zivotinje = Context.ZivotinjeVeterinari
                            //Ukljucuje prvi nivo
                            .Include(p => p.Zivotinja)
                            //Ukljucuje drugi nivo
                            .ThenInclude(p => p.VrstaZivotinje)
                            .Include(p => p.Pregled)
                            .Include(p => p.Veterinar)
                            //Trazi id veterinara jednak onom koji je prosledjen 
                            //Trazi se da li je Pregled.ID unutar kolekcije mesecID
                            //Trazi id vrste zivotinje jednak onom koji je prosledjen 
                            .Where(p => p.Veterinar.ID == veterinarID && mesecID.Contains(p.Pregled.ID) && p.Zivotinja.VrstaZivotinje == vrsta);

                var zivotinja = await zivotinje.ToListAsync();
                return Ok(
                    zivotinja.Select(p =>
                    new
                    {
                        BrojKartona = p.Zivotinja.BrojKartona,
                        ImeZivotinje = p.Zivotinja.ImeZivotinje,
                        ImeVlasnika = p.Zivotinja.ImeVlasnika,
                        BrojTelefonaVlasnika = p.Zivotinja.BrojTelefonaVlasnika,
                        Vrsta = vrsta.Vrsta,
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

        #region DodajZivotinju
        [Route("DodajZivotinju/{brojKartona}/{imeZivotinje}/{imeVlasnika}/{brTelefonaVlasnika}/{vrstaID}")]
        [HttpPost]
        public async Task<ActionResult> DodajZivotinju(int brojKartona, string imeZivotinje, string imeVlasnika, string brTelefonaVlasnika, int vrstaID)
        {
            //Provera da li je ime null ili white space ili duzina veca od 50  
            if (string.IsNullOrWhiteSpace(imeZivotinje) || imeZivotinje.Length > 50)
            {
                return BadRequest("Pogresno ime zivotinje!");
            }
            if (string.IsNullOrWhiteSpace(imeVlasnika) || imeVlasnika.Length > 50)
            {
                return BadRequest("Pogresno ime vlasnika!");
            }
            if (string.IsNullOrWhiteSpace(brTelefonaVlasnika) || brTelefonaVlasnika.Length > 15)
            {
                return BadRequest("Pogresan broj telefona!");
            }
            try
            {
                var vrsta = await Context.VrsteZivotinja.Where(p => p.ID == vrstaID).FirstOrDefaultAsync();
                Zivotinja z = new Zivotinja
                {
                    BrojKartona = brojKartona,
                    ImeZivotinje = imeZivotinje,
                    ImeVlasnika = imeVlasnika,
                    BrojTelefonaVlasnika = brTelefonaVlasnika,
                    VrstaZivotinje = vrsta
                };
                //Update u modelu
                Context.Zivotinje.Add(z);
                //Update u bazi 
                await Context.SaveChangesAsync(); 
                return Ok($"Zivotinja je uspesno dodata, ID: {z.ID}!");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        #endregion

        #region AzurirajZivotinju 

        //Azuriranje zivotinje preko parametara 
        [Route("AzurirajZivotinju/{brojKartona}/{imeZivotinje}/{imeVlasnika}/{brojTelefona}")]
        [HttpPut]
        public async Task<ActionResult> AzurirajZivotinju(int brojKartona, string imeZivotinje, string imeVlasnika, string brojTelefona)
        {
            //Provera da li je ime null ili white space ili duzina veca od 50  
            if (string.IsNullOrWhiteSpace(imeZivotinje) || imeZivotinje.Length > 50)
            {
                return BadRequest("Pogresno ime zivotinje!");
            }
            if (string.IsNullOrWhiteSpace(imeVlasnika) || imeVlasnika.Length > 50)
            {
                return BadRequest("Pogresno ime vlasnika!");
            }
            if (string.IsNullOrWhiteSpace(brojTelefona) || brojTelefona.Length > 15)
            {
                return BadRequest("Pogresan broj telefona!");
            }
            try
            {
                var zivotinja = Context.Zivotinje.Where(p => p.BrojKartona == brojKartona).FirstOrDefault();
                if (zivotinja != null)
                {
                    //Update u Modelu 
                    zivotinja.ImeZivotinje = imeZivotinje;
                    zivotinja.ImeVlasnika = imeVlasnika;
                    zivotinja.BrojTelefonaVlasnika = brojTelefona;

                    //Salju se promene u bazi podataka 
                    await Context.SaveChangesAsync();
                    return Ok($"Zivotinja je uspesno azurirana, ID: {zivotinja.ID}!");
                }
                else
                {
                    return BadRequest("Zivotinja nije pronadjena!");
                }

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        #endregion

        #region ObrisiZivotinju
        [Route("ObrisiZivotinju/{brojKartona}")]
        [HttpDelete]
        public async Task<ActionResult> ObrisiZivotinju(int brojKartona)
        {
            try
            {
                //Prvo se brisu pregledi zivotinje ako ih ima pa tek onda sama zivotinja 
                var pregled = Context.ZivotinjeVeterinari
                            .Include(p => p.Zivotinja)
                            .Where(p => p.Zivotinja.BrojKartona==brojKartona).ToList(); 
                foreach (var p in pregled)
                {
                    Context.ZivotinjeVeterinari.Remove(p);      
                }
                Context.SaveChanges();
                //Brisanje se vrsi po broju kartona 
                var zivotinja = Context.Zivotinje.Where(p=>p.BrojKartona==brojKartona).FirstOrDefault();
                int br = zivotinja.BrojKartona;
                Context.Zivotinje.Remove(zivotinja);
                await Context.SaveChangesAsync();
                return Ok($"Zivotinja je uspesno obrisana! Broj kartona zivotinje je bio: {br}");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        #endregion
    }
}
