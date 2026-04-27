using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class InstitutionEntity
    {
        public Guid Id { get; set; }    
        public string Name { get; set; }
        public long Longitude { get; set; }
        public long Latitude { get; set; }
        public string AvatarUrl { get; set; }
        public List<string> PhotoUrls { get; set; }
        public string Address { get; set; }
        public InstitutionTypeEnum Type { get; set; }
        public float Rating { get; set; }
        public List<WorkingHoursEntity> workingHours { get; set; }
        public int? AveragePrice { get; set; }
        public string? WebUrl { get; set; }
        public List<LabelTypeEnum> Labels { get; set; }
        public List<VisitingEntity> Visitings { get; set; }
        public List<ViewingEntity> Viewwings { get; set; }
        public Guid? OwnerId { get; set; }

    }
}
