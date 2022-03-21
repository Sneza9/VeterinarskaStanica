using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class Zivotinja
    {
        [Key]
        public int ID { get; set; }
        [Required]
        public int BrojKartona { get; set; }
        [Required]
        [MaxLength(50)]
        public string ImeZivotinje { get; set; }
        [Required]
        [MaxLength(50)]
        public string ImeVlasnika { get; set; }
        [MaxLength(15)]
        public string BrojTelefonaVlasnika { get; set; }
        //Foreign key ka Vrsti zivotinja 
        public VrstaZivotinje VrstaZivotinje { get; set; }
        public List<Veza> ZivotinjaVeterinar { get; set; } 
    }
}