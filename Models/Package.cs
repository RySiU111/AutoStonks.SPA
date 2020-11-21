using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoStonks.SPA.Models
{
    public class Package
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Generation Generation { get; set; }
        public int GenerationId { get; set; }
    }
}
