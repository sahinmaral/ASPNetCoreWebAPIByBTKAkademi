using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApp.Entities.Models.LinkModels
{
    public class Link
    {
        public string? HyperReference { get; set; }
        public string? Relation { get; set; }
        public string? Method { get; set; }

        public Link()
        {

        }
        public Link(string? hyperReference, string? relation, string? method)
        {
            HyperReference = hyperReference;
            Relation = relation;
            Method = method;
        }
    }
}
