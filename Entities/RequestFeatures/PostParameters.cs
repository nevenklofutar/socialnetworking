using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.RequestFeatures
{
    public class PostParameters : RequestParameters
    {
        public PostParameters()
        {
            OrderBy = "CreatedOn Desc";
        }

        public string Title { get; set; }
        public string Body { get; set; }
    }
}
