using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.RequestFeatures
{
    public class PostParameters : RequestParameters
    {
        public PostParameters()
        {
            OrderBy = "title";
        }

        public string Title { get; set; }
        public string Body { get; set; }
        public string SearchTerm { get; set; }
    }
}
