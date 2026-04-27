using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class ReviewScoreEntity
    {
        public Guid Id {  get; set; }
        public Guid ReviewId { get; set; }
        public ReviewEntity Review { get; set; }
        public Guid UserId { get; set; }
        public UserEntity User { get; set; }
        public bool isLiked { get; set; }
    }
}
