using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using Cinch;
using Glass.Design.Pcl.CanvasItem;
using Glass.Design.Pcl.DesignSurface;
using Glass.Design.Wpf;
using MEFedMVVM.ViewModelLocator;
using PostSharp.Patterns.Model;
using SampleModel;
using SampleModel.Serialization;

namespace Glass.Design.WpfTester
{
    [ExportViewModel("MainViewModel")]
    [NotifyPropertyChanged]
    public class MainWindowViewModel 
    {
        private ISaveFileService SaveFileService { get; set; }
        private IOpenFileService OpenFileService { get; set; }

        [ImportingConstructor]
        public MainWindowViewModel(ISaveFileService saveFileService, IOpenFileService openFileService)
        {
            SaveFileService = saveFileService;
            OpenFileService = openFileService;
            GroupCommandArgs = new GroupCommandArgs
            {
                CreateHostingItem = () => new Group()
            };

            LoadCommand = new SimpleCommand<object, object>(o => Load());
            SaveCommand = new SimpleCommand<object, object>(o => Save());

            this.items = CreateSampleItems();
        }

        private static CanvasItemCollection CreateSampleItems()
        {
            var items = new CanvasItemCollection();
            items.Add(new Link
                      {
                          Left = 0,
                          Top = 40,
                          Width = 200,
                          Height = 100,
                      });

          

            items.Add(new Label
                      {
                          Left = 400,
                          Top = 40,
                          Width = 250,
                          Height = 50,
                          Text = "Hello boys!\nThis is far from perfect, but it works :D"
                      });

            //items.Add(new CanvasRectangle
            //{
            //    Left = 200,
            //    Top = 40,
            //    Width = 100,
            //    Height = 100,
            //    FillColor = new Color(0, 0, 255)
            //});

            //items.Add(new Ellipse
            //{
            //    Left = 200,
            //    Top = 200,
            //    Width = 200,
            //    Height = 100,
            //    FillColor = new Color(0, 220, 255)
            //});

            var group = new Group { Top = 100, Left = 200, Width = 110, Height = 200 };
            group.Children.Add(new Mario { Left = 15, Top = 0, Width = 80, Height = 100 });
            group.Children.Add(new Sonic { Left = 15, Top = 100, Width = 80, Height = 100 });

            items.Add(group);


            return items;
        }

        private void Load()
        {
            if (OpenFileService.ShowDialog(Application.Current.MainWindow) == true)
            {
                using (var fileStream = new FileStream(OpenFileService.FileName, FileMode.Open))
                {
                    var modelSaver = new XmlModelSerializer(fileStream);
                    var composition = modelSaver.Deserialize();
                    this.items = new CanvasItemCollection(composition);
                }
            }
        }

        private void Save()
        {
            if (SaveFileService.ShowDialog(Application.Current.MainWindow) == true)
            {
                using (var fileStream = new FileStream(SaveFileService.FileName, FileMode.Create))
                {
                    var modelSaver = new XmlModelSerializer(fileStream);
                    modelSaver.Serialize(Items.ToList());
                }
            }
        }


        #region Property Items
        private CanvasItemCollection items;
        public static readonly PropertyChangedEventArgs itemsEventArgs = ObservableHelper.CreateArgs<MainWindowViewModel>(x => x.Items);
        public CanvasItemCollection Items
        {
            get { return items; }
            
        }
        #endregion


        public GroupCommandArgs GroupCommandArgs { get; set; }

        public ICommand LoadCommand { get; set; }
        public ICommand SaveCommand { get; set; }
    }
}