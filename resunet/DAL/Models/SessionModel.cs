using System.ComponentModel.DataAnnotations;

namespace resunet.DAL.Models
{
    public class SessionModel
    {
        [Key]
        public int? DbSessionID { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime LastAccessedAt { get; set; }

        public int UserID { get; set; }
    }
}
