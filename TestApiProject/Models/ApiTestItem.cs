using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TestApiProject.Models
{
    [Table("ApiTestItem",Schema ="dbo")]
    public class ApiTestItem
    {
        [Key]
        public int ApiTestId { get; set; }
        public string Name { get; set; }
        public Boolean IsComplete { get; set; }
    }
}
