using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static Parky.API.Models.Enums.TrialEnums;

namespace Parky.API.Models
{
    public class Trial
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Distance { get; set; }
        public DifficultyType Difficulty { get; set; }
        [Required]
        [ForeignKey("NationalParkId")]
        public int NationalParkId { get; set; }
        public NationalPark NationalPark { get; set; }
        public DateTime Created { get; set; }
        public bool isOpen { get; set; }
        public bool isActive { get; set; }
        public bool isDeleted { get; set; }
    }
}
