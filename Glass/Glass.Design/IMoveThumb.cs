using System.Windows.Controls.Primitives;
using Design.Interfaces;

namespace Glass.Design
{
    public interface IMoveThumb
    {        
        event DeltaMoveEventHandler MoveDelta;
    }

    public delegate void DeltaMoveEventHandler(object sender, DeltaEventArgs args);

    public class DeltaEventArgs
    {
        public DeltaEventArgs(double horizontalChange, double verticalChange)
        {
            HorizontalChange = horizontalChange;
            VerticalChange = verticalChange;
        }

        public double HorizontalChange { get; set; }
        public double VerticalChange { get; set; }
    }
}