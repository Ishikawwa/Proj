using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class InstitutionLabelEntity
    {
        public Guid InstitutionId { get; set; }
        public InstitutionEntity Institution { get; set; }
        public Label Label { get; set; }
    }
}
