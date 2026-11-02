using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AndrewProg30000Assignment2.Models;

[Table("VehicleInfo")]
public class Vehicle
{
    public int VehicleId { get; set; }

    [Required]
    [StringLength(50)]
    public string Make { get; set; } = string.Empty;

    [Display(Name = "Type")]
    public VehicleType Type { get; set; }

    [Required]
    [Display(Name = "Purchase Date")]
    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
    public DateTime PurchaseDate { get; set; }

    [Required]
    [Range(1990, 2027)]
    public int Year { get; set; }

    [Required]
    [Range(1.0, 500000.0)]
    [DataType(DataType.Currency)]
    [DisplayFormat(DataFormatString = "{0:C}")]
    [Column(TypeName = "decimal(18,2)")]
    public decimal Price { get; set; }
}