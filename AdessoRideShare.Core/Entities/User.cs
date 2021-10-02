using System;
using System.Collections.Generic;

namespace AdessoRideShare.Core.Entities
{
    public class User : BaseEntity
    {
        public string UserName { get; set; }
        public virtual List<TravelPlan> TravelPlans { get; set; }
    }
}
