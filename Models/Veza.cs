using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Models
{
    public class Veza
    {
        [Key]
        public int ID { get; set; }
        [Required]
        [MaxLength(50)]
        public string Lek { get; set; }
        //Foreign keys ka Pregledu, Zivotinji i Veterinaru 
        //Kada je pregled obavljen 
        public Pregled Pregled { get; set; }
        [JsonIgnore]
        public Zivotinja Zivotinja { get; set; }
        public Veterinar Veterinar { get; set; }
    }
}