using System.Windows.Controls.Primitives;
using Design.Interfaces;

namespace Glass.Design
{
    public interface IMoveThumb
    {        
        event DeltaMoveEventHandler MoveDelta;
    }
}