using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class SpamReportEntity
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public UserEntity User { get; set; }
        public Guid ReviewId { get; set; }
        public ReviewEntity ReviewEntity { get; set; }
        public string Comment { get; set; }
    }
}
