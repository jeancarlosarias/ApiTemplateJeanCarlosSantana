using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace EmpresaAPI.Models.TablesTbl
{
    public class ClientTbl
    {
        [Key]
        [JsonIgnore]
        public int ClientId { get; set; }
        public string ClientName { get; set; }
        public string ClientDirection { get; set; }
        public string ClientTel { get; set; }
        public DateTime ModifiedDateTime { get; set; }

    }
}
