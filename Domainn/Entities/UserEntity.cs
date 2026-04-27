using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class UserEntity
    {
        public Guid Id { get; set; }
        public string Nickname { get; set; }
        public string AvatarUrl { get; set; }
        public DateTime? CreatedAt { get; set; }
    }
}
