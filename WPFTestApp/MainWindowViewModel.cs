using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using Cinch;
using Glass.Design.Pcl.CanvasItem;
using Glass.Design.Pcl.CanvasItem.NotifyPropertyChanged;
using Glass.Design.Pcl.DesignSurface;
using Glass.Design.Wpf;
using MEFedMVVM.ViewModelLocator;
using SampleModel;
using SampleModel.Serialization;

namespace Glass.Design.WpfTester
{
    [ExportViewModel("MainViewModel")]
    public class MainWindowViewModel : ViewModelBase
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

            Items = CreateSampleItems();
        }

        private static CanvasItemCollection CreateSampleItems()
        {
            var items = new CanvasItemCollection();
            items.Add(new Mario
                      {
                          Left = 0,
                          Top = 40,
                          Width = 200,
                          Height = 100,
                      });

            var group = new Group { Top = 100, Left = 200, Width = 400, Height = 300 };
            group.Children.Add(new Mario { Left = 0, Top = 0, Width = 100, Height = 200 });
            group.Children.Add(new Sonic { Left = 0, Top = 200, Width = 100, Height = 200 });

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
                    this.Items = new CanvasItemCollection(composition);
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
            set
            {

                if (object.ReferenceEquals(items, value)) return;
                items = value;
                NotifyPropertyChanged(itemsEventArgs);
            }
        }
        #endregion


        public GroupCommandArgs GroupCommandArgs { get; set; }

        public ICommand LoadCommand { get; set; }
        public ICommand SaveCommand { get; set; }
    }
}