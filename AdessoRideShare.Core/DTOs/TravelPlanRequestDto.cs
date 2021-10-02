using System;
using System.ComponentModel.DataAnnotations;

namespace AdessoRideShare.Core.DTOs
{
    public class TravelPlanRequestDto
    {
        //Belirli bir travel plana katılmak istenirken kulllanilan model.
        [Required]
        public Guid UserId { get; set; }
        [Required]
        public Guid TravelPlanId { get; set; }
    }
}
