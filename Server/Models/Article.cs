using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPEMoorthy.Server.Models
{
    public class Article
    {
        [Key]
        long Id { get; set; }
        string Category { get; set; }
        string Title { get; set; }
        string Description { get; set; }
        string URI { get; set; }
        bool IsActive { get; set; }
        string Version { get; set; }
        DateTime CreatedDate { get; set; }
        DateTime ModifiedDate { get; set; }
    }
}
