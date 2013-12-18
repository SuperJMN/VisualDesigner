using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Glass.Design.Pcl.Core;

namespace Glass.Design.Pcl.DesignSurface.VisualAids.Snapping
{
    public abstract class SnappingEngine : ISnappingEngine
    {
        
        public abstract double SnapPoint(double value);

        public bool ShouldSnap(Edge edge, double value)
        {
            return Math.Abs(value - edge.Origin) < Threshold;
        }

        public double Threshold { get; set; }
    }
}
