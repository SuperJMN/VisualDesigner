using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Glass.Design.Pcl;
using Glass.Design.Pcl.Core;
using Model;
using StyleMVVM.DependencyInjection;
using StyleMVVM.ViewModel;

namespace ComicDesigner
{
    [Export("MainViewModel")]
    public class MainViewModel : BaseViewModel
    {
        private IEditingContext EditingContext { get; set; }
        private CanvasItemViewModelCollection items;
        private static readonly Random RandomGenerator = new Random((int)DateTime.Now.Ticks);

        [ImportConstructor]
        public MainViewModel(IEditingContext editingContext)
        {
            EditingContext = editingContext;

            // Since we're Main, we have to instance the document into the editing context.
            EditingContext.Document = new Document();

            LoadItemsCommand = new DelegateCommand(OnLoadItems);
        }

        private void OnLoadItems(object parameter)
        {
            const int numOfItems = 20;

            for (var i = 0; i < numOfItems; i++)
            {
                var item = GetSampleItem(i);
                Items.Add(item);
            }


            var middlePoint = new Point(SurfaceWidth / 2, SurfaceHeight / 2);

            var marioWidth = 200;
            var marioHeight = 240;

            var mario = new Mario { Left = middlePoint.X - marioWidth / 2D, Top = middlePoint.Y - marioHeight / 2D, Width = marioWidth, Height = marioHeight };
            var bubble = new Bubble { Left = mario.Right - 70, Top = mario.Top - 170, Width = 250, Height = 280, Text = "WOW. Much ViewModel. So RT. Such Designer." };

            Items.Add(mario);
            Items.Add(bubble);
        }

        private CanvasItemViewModel GetSampleItem(int i)
        {

            CanvasItemViewModel item;

            switch (i % 2)
            {
                case 0:
                    item = new CanvasRectangle { Width = 200, Height = 200 };
                    break;
                case 1:
                    item = new Ellipse { Width = 200, Height = 100 };
                    break;

                default:
                    throw new NotSupportedException("Invalid type of model");
            }

            var horzRange = Math.Max(0, (int)(SurfaceWidth - item.Width));
            var vertRange = Math.Max(0, (int)(SurfaceHeight - item.Height));

            var x = RandomGenerator.Next(horzRange);
            var y = RandomGenerator.Next(vertRange);

            item.SetLocation(new Point(x, y));

            return item;
        }

        public CanvasItemViewModelCollection Items
        {
            get { return EditingContext.Document.Graphics; }            
        }

        public ICommand LoadItemsCommand { get; set; }

        public double SurfaceWidth { get; set; }

        public double SurfaceHeight { get; set; }
    }
}