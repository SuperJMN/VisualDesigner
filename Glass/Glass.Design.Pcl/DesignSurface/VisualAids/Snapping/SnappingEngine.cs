using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Glass.Design.Pcl.Annotations;
using Glass.Design.Pcl.CanvasItem;
using Glass.Design.Pcl.Core;

namespace Glass.Design.Pcl.DesignSurface.VisualAids.Snapping
{
    public abstract class SnappingEngine : ISnappingEngine
    {
        public SnappingEngine(double thresohold)
        {
            Threshold = thresohold;
        }

        public abstract double SnapLeft(double value);
        public abstract double SnapTop(double value);

        public double Threshold { get; set; }

        [NotNull]
        public ICanvasItem Snappable { get; set; }

        public void SetSourceRect(IRect originalRect)
        {
            Snappable.Left = SnapLeft(originalRect.Left);
            Snappable.Top = SnapTop(originalRect.Top);
        }
    }
}
