using System.ComponentModel.DataAnnotations;

namespace SpendWise.Users.Dto;

public class ChangeUserLanguageDto
{
    [Required]
    public string LanguageName { get; set; }
}