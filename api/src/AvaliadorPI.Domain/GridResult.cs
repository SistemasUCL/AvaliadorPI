using System.Collections.Generic;
using System.Linq;

namespace AvaliadorPI.Domain
{
    public class GridResult
    {
        public GridResult()
        {
            Data = Enumerable.Empty<dynamic>();
        }

        public int Total { get; set; }

        public IEnumerable<dynamic> Data { get; set; }
    }
}
