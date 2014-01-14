using System.ComponentModel;
using System.Runtime.CompilerServices;
using Glass.Design.Pcl.Annotations;
using Glass.Design.Pcl.DesignSurface;

namespace Glass.Design.Pcl.CanvasItem.NotifyPropertyChanged
{
    public abstract class CanvasItemINPC : CanvasItem, INotifyPropertyChanged
    {
        public CanvasItemINPC()
        {
            
        }

        protected override void OnWidthChanged(SizeChangeEventArgs e)
        {
            base.OnWidthChanged(e);
            OnPropertyChanged("Width");
        }

        protected override void OnTopChanged(LocationChangedEventArgs e)
        {
            base.OnTopChanged(e);
            OnPropertyChanged("Top");
        }

        protected override void OnHeightChanged(SizeChangeEventArgs e)
        {
            base.OnHeightChanged(e);
            OnPropertyChanged("Height");
        }

        protected override void OnLeftChanged(LocationChangedEventArgs e)
        {
            base.OnLeftChanged(e);
            OnPropertyChanged("Left");
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}