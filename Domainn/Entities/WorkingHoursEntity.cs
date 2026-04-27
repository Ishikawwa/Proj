using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class WorkingHoursEntity
    {
        public TimeOnly Start {  get; set; }
        public TimeOnly End { get; set; }
        public DayOfWeek DayOfWeek { get; set; }
        public Guid InstitutionId { get; set; }
        public InstitutionEntity Institution { get; set; }
    }
}
