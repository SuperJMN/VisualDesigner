using System.Windows;
using AutoMapper;
using Glass.Design.Pcl.Core;
using Glass.Design.Wpf.PlatformSpecific;
using PostSharp.Patterns.Recording;
using NetCorePoint = System.Windows.Point;
using PclPoint = Glass.Design.Pcl.Core.Point;

using NetCoreRect = System.Windows.Rect;
using PclRect = Glass.Design.Pcl.Core.Rect;



namespace Glass.Design.WpfTester
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            SetupPlatformToPclMappings();
            ServiceLocator.InputProvider = new NetCoreInputProvider();
            ServiceLocator.UIElementFactory = new NetCoreUIElementFactory();

            RecordingServices.OperationFormatter = new MyOperationFormatter( RecordingServices.OperationFormatter );
        }

        private void SetupPlatformToPclMappings()
        {
            Mapper.CreateMap<NetCorePoint, PclPoint>();
            Mapper.CreateMap<NetCoreRect, PclRect>()
                .ForMember(rect => rect.Location, expression => expression.Ignore())
                .ForMember(rect => rect.Size, expression => expression.Ignore())
                ;


            Mapper.CreateMap<PclPoint, NetCorePoint>();
            Mapper.CreateMap<PclRect, NetCoreRect>()
                .ForMember(rect => rect.Location, expression => expression.Ignore())
                .ForMember(rect => rect.Size, expression => expression.Ignore())
                ;

            Mapper.AssertConfigurationIsValid();
        }
    }

    
}
