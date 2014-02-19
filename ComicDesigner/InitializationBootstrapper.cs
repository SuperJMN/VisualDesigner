using System.Reflection;
using AutoMapper;
using Glass.Design.Pcl.Core;
using Glass.Design.WinRT.PlatformSpecific;
using StyleMVVM;
using Point = Windows.Foundation.Point;
using Rect = Windows.Foundation.Rect;

namespace ComicDesigner
{
    internal class InitializationBootstrapper
    {
        private readonly App application;

        public InitializationBootstrapper(App application)
        {
            this.application = application;
            SetupWinRTToPclMappings();
            SetupStyleMVVMBootstrapper();


            // We must set the appropiate providers for the platform. They will instance the correct types that cannot be instanced or called in PCL code
            ServiceLocator.InputProvider = new WinRTInputProvider();
            ServiceLocator.UIElementFactory = new WinRTUIElementFactory();
        }
        
        private void SetupStyleMVVMBootstrapper()
        {
            if (!Bootstrapper.HasInstance)
            {
                var bootstrapper = new Bootstrapper();

                bootstrapper.Container.RegisterAssembly(application.GetType().GetTypeInfo().Assembly);

                bootstrapper.Start();
            }
        }

        private void SetupWinRTToPclMappings()
        {
            Mapper.CreateMap<Point, Glass.Design.Pcl.Core.Point>();
            Mapper.CreateMap<Rect, Glass.Design.Pcl.Core.Rect>()
                .ForMember(rect => rect.Location, expression => expression.Ignore())
                .ForMember(rect => rect.Size, expression => expression.Ignore())
                ;


            Mapper.CreateMap<Glass.Design.Pcl.Core.Point, Point>();
            Mapper.CreateMap<Glass.Design.Pcl.Core.Rect, Rect>()               
                ;

            Mapper.AssertConfigurationIsValid();
        }
    }
}