using System;
using System.ComponentModel.DataAnnotations;

namespace AdessoRideShare.Core.DTOs
{
    public class TravelPlanDto
    {
        public Guid Id { get; set; }
        [Required]
        public int From { get; set; }
        [Required]
        public int Where { get; set; }
        [Required]
        public DateTime TravelDate { get; set; } = DateTime.Now;
        [Required]
        [Range(1, 15)] public int SeatCapacity { get; set; } = 1;
        [Required]
        public string Description { get; set; }
        public string ConnectedCityList { get; set; }
        public bool isPublish { get; set; } = true;
    }
}
