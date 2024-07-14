using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace EmpresaAPI.Models.TablesTbl
{
    public class PhotoTbl
    {
        [Key]
        [JsonIgnore]
        public int PhotoId { get; set; }
        public string PhotoName { get; set; }
        public string PhotoDescription { get; set; }
        public string PhotoUrl { get; set;}
        public string PhotoCategory { get; set; }
        public DateTime ModifiedDateTime { get; set; }
    }
}
