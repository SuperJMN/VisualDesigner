#region

using System.Windows;
using System.Windows.Controls;
using Glass.Basics.Wpf.Collections;

#endregion

namespace Glass.Basics.Wpf.Controls.BindableGrid
{
    public class BindableGrid : Grid
    {
        public BindableGrid()
        {
            ColumnSyncronizer = new ObservableCollectionSynchronizer<ColumnDefinition> {Destination = ColumnDefinitions};
            RowSyncronizer = new ObservableCollectionSynchronizer<RowDefinition> {Destination = RowDefinitions};
        }

        private ObservableCollectionSynchronizer<ColumnDefinition> ColumnSyncronizer { get; set; }
        private ObservableCollectionSynchronizer<RowDefinition> RowSyncronizer { get; set; }

        #region ColumnDefinitionsSource

        public static readonly DependencyProperty ColumnDefinitionsSourceProperty =
            DependencyProperty.Register("ColumnDefinitionsSource", typeof (BoundGridColumnDefinitionCollection),
                typeof (BindableGrid),
                new FrameworkPropertyMetadata(null,
                    OnColumnDefinitionsSourceChanged));

        public BoundGridColumnDefinitionCollection ColumnDefinitionsSource
        {
            get { return (BoundGridColumnDefinitionCollection) GetValue(ColumnDefinitionsSourceProperty); }
            set { SetValue(ColumnDefinitionsSourceProperty, value); }
        }

        private static void OnColumnDefinitionsSourceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var target = (BindableGrid) d;
            var oldColumnDefinitionsSource = (BoundGridColumnDefinitionCollection) e.OldValue;
            var newColumnDefinitionsSource = target.ColumnDefinitionsSource;
            target.OnColumnDefinitionsSourceChanged(oldColumnDefinitionsSource, newColumnDefinitionsSource);
        }

        protected virtual void OnColumnDefinitionsSourceChanged(
            BoundGridColumnDefinitionCollection oldColumnDefinitionsSource,
            BoundGridColumnDefinitionCollection newColumnDefinitionsSource)
        {
            ColumnSyncronizer.Source = newColumnDefinitionsSource;
        }

        #endregion

        #region RowDefinitionsSource

        public static readonly DependencyProperty RowDefinitionsSourceProperty =
            DependencyProperty.Register("RowDefinitionsSource", typeof (BoundGridRowDefinitionCollection),
                typeof (BindableGrid),
                new FrameworkPropertyMetadata(null,
                    OnRowDefinitionsSourceChanged));

        public BoundGridRowDefinitionCollection RowDefinitionsSource
        {
            get { return (BoundGridRowDefinitionCollection) GetValue(RowDefinitionsSourceProperty); }
            set { SetValue(RowDefinitionsSourceProperty, value); }
        }

        private static void OnRowDefinitionsSourceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var target = (BindableGrid) d;
            var oldRowDefinitionsSource = (BoundGridRowDefinitionCollection) e.OldValue;
            var newRowDefinitionsSource = target.RowDefinitionsSource;
            target.OnRowDefinitionsSourceChanged(oldRowDefinitionsSource, newRowDefinitionsSource);
        }

        protected virtual void OnRowDefinitionsSourceChanged(BoundGridRowDefinitionCollection oldRowDefinitionsSource,
            BoundGridRowDefinitionCollection newRowDefinitionsSource)
        {
            RowSyncronizer.Destination = newRowDefinitionsSource;
        }

        #endregion
    }
}