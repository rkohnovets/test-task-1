using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace WebApi.Models;

public class Contact
{
    [JsonPropertyName("contactID")]
    public int ContactID { get; set; }

    [Required]
    [JsonPropertyName("fullName")]
    public string FullName { get; set; }

    [Required]
    [EmailAddress]
    [JsonPropertyName("email")]
    public string Email { get; set; }

    [JsonPropertyName("contractorID")]
    public int ContractorID { get; set; }

    [JsonIgnore]
    [JsonPropertyName("createdAt")]
    public DateTime CreatedAt { get; set; }

    [JsonIgnore]
    [JsonPropertyName("updatedAt")]
    public DateTime UpdatedAt { get; set; }

    // без "?" не работает
    [JsonIgnore]
    public Contractor? Contractor { get; set; }
}