using System.ComponentModel.DataAnnotations;
using static Parky.API.Models.Enums.TrialEnums;

namespace Parky.API.Models.Dtos
{
    public class TrailCreateDto
    {
        public string Name { get; set; }
        public double Distance { get; set; }
        public DifficultyType Difficulty { get; set; }
        [Required]
        public int NationalParkId { get; set; }
    }
}
