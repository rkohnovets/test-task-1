using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace WebApi.Models;

public class Contact : BaseEntity
{
    [Key] [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int ContactID { get; set; }

    [Required]
    public string FullName { get; set; }

    [Required] [EmailAddress]
    public string Email { get; set; }

    public int ContractorID { get; set; }

    // без "?" не работает
    [JsonIgnore]
    public Contractor? Contractor { get; set; }
}