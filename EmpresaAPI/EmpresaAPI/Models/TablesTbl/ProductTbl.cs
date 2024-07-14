using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace EmpresaAPI.Models.TablesTbl
{
    public class ProductTbl
    {
        [Key]
        [JsonIgnore]
        public int ProductId { get; set; }
        public int PhotoId { get; set; }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public decimal ProductPrice { get; set; }
        public int ProductQuantity { get; set; }
        public bool ProductActive { get; set; }
        public DateTime ModifiedDateTime { get; set; }
    }
}
