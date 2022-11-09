using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    internal class AnimalNode
    {
        public int Key { get; set; }
        public AnimalNode Left { get; set; }
        public AnimalNode Right { get; set; }
        public AnimalNode(int elem)
        {
            this.Key = elem;
        }
    }
}
