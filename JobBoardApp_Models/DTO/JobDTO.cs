using System;
using System.Collections.Generic;
using System.Text;

namespace JobBoardApp_Models.DTO
{
    public class JobDTO
    {
        public int? Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime? ExpiresAt { get; set; }
    }
}
