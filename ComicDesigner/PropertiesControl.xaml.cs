using Windows.UI.Xaml;
using Glass.Design.Pcl.Canvas;

namespace ComicDesigner
{
    public sealed partial class PropertiesControl
    {
        public PropertiesControl()
        {
            this.InitializeComponent();
        }

        #region SelectedItem
        public static readonly DependencyProperty SelectedItemProperty =
          DependencyProperty.Register("SelectedItem", typeof(CanvasItem), typeof(PropertiesControl),
            new PropertyMetadata(null));

        public bool SelectedItem
        {
            get { return (bool)GetValue(SelectedItemProperty); }
            set { SetValue(SelectedItemProperty, value); }
        }

        #endregion        
    }
}
