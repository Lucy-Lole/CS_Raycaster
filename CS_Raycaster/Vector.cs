using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS_Raycaster
{
    public class Vector
    {
        public double x { get; set; }
        public double y { get; set; }

        /// <summary>
        /// A simple class to hold an x and y coord.
        /// </summary>
        public Vector()
        {
        }

        /// <summary>
        /// A simple class to hold an x and y coord.
        /// </summary>
        /// <param name="x">X point of the vector</param>
        /// <param name="y">Y point of the vector</param>
        public Vector(double x, double y)
        {
            this.x = x;
            this.y = y;
        }
    }
}
