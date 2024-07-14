using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace EmpresaAPI.Models.TablesTbl
{
    public class BillTbl
    {
        [Key]
        [JsonIgnore]
        public int BillId { get; set; }
        public int ClientId { get; set; }
        public int ProductId { get; set; }
        public decimal BillAmount { get; set; }
        public int BillQuantity { get; set; }
        public DateTime ModifiedDateTime { get; set; }
    }
}
