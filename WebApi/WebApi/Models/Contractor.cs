using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace WebApi.Models;

public class Contractor : BaseEntity
{
    [Key] [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int ContractorID { get; set; }

    [Required]
    public string ContractorName { get; set; }

    [JsonIgnore]
    public ICollection<Contact>? Contacts { get; set; }
}
