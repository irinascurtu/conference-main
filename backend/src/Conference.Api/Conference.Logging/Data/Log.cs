using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Conference.Logging.Data
{
    public class Log
    {
        [Key]
        public int Id { get; set; }
        public int ApplicationId { get; set; }
        public string Message { get; set; }
        public string StackTrace { get; set; }
        public int SeverityId { get; set; }
    }
}
