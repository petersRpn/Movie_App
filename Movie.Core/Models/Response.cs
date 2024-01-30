using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.Core.Models
{
    public class Resp<T>
    {
        public string? Status { get; set; }
        public string? Message { get; set;}
        public List<T>? Data { get; set; }
    }
}
