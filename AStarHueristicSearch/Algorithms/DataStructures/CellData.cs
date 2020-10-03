using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace HeuristicSearch.Algorithms.DataStructures
{
    [Serializable]
    public class CellData
    {
        [NonSerialized]
        private Vector2? parent;

        public Vector2? Parent
        {
            get { return parent; }
            set
            {
                modified = true;
                parent = value;
            }
        }

        private decimal f;
        public decimal F
        {
            get { return f; }
            set
            {
                modified = true;
                f = value;
            }
        }

        private decimal g;
        public decimal G
        {
            get { return g; }
            set
            {
                modified = true;
                g = value;
            }
        }

        private decimal h;
        public decimal H
        {
            get { return h; }
            set
            {
                modified = true;
                h = value;
            }
        }

        private bool modified = false;
        public bool Modified { get { return modified; } }
    }
}
