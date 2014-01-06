using System.Windows;
using Glass.Design.Pcl.DesignSurface;
using SampleModel;

namespace Glass.Design.WpfTester
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();

            GroupCommandArgs = new GroupCommandArgs
                        {
                            CreateHostingItem = () => new Group()
                        };
        }

        #region GroupCommandArgs
        public static readonly DependencyProperty GroupCommandArgsProperty =
          DependencyProperty.Register("GroupCommandArgs", typeof(GroupCommandArgs), typeof(MainWindow),
            new FrameworkPropertyMetadata(null));

        public GroupCommandArgs GroupCommandArgs
        {
            get { return (GroupCommandArgs)GetValue(GroupCommandArgsProperty); }
            set { SetValue(GroupCommandArgsProperty, value); }
        }

        #endregion

        

    }
}
