using System;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Media;

namespace Glass.Basics.Presentation {

    public class ControlAdorner : Adorner {

        private UIElement chrome;

        public ControlAdorner(UIElement adornedElement, UIElement chrome)
            : base(adornedElement) {
                Chrome = chrome;
        }

        protected override int VisualChildrenCount {
            get {
                return 1;
            }
        }

        protected override Visual GetVisualChild(int index) {
            if (index != 0)
                throw new ArgumentOutOfRangeException();

            return chrome;
        }

        public UIElement Chrome {
            get { return chrome; }
            set {
                if (chrome != null) {
                    RemoveVisualChild(chrome);
                }
                chrome = value;
                if (chrome != null) {
                    AddVisualChild(chrome);
                }
            }
        }

        protected override Size MeasureOverride(Size constraint) {
            chrome.Measure(constraint);
            return chrome.DesiredSize;
        }

        protected override Size ArrangeOverride(Size finalSize) {
            chrome.Arrange(new Rect(new Point(0, 0), AdornedElement.RenderSize));
            return Chrome.RenderSize;
        }
    }
}
