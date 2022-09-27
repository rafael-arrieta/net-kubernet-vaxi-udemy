using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NetKubernet.Models;

public class Property
{

    [Key]
    [Required]
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Adress { get; set; }

    [Required]
    [Column(TypeName = "decimal(18,4)")]
    public decimal Price { get; set; }

    public string? Picture { get; set; }

    public DateTime? DateCreated { get; set; }

    public Guid? UserId { get; set; }

}