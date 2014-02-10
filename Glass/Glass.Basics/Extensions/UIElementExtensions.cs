using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Glass.Basics.Wpf.Extensions {

    public static class UIElementExtensions {

        public static IEnumerable<UIElement> GetChildrenUnderPosition(this Panel panel, Point pointRelativeToParent) {

            var children = new List<UIElement>();

            foreach (UIElement child in panel.Children) {

                var rect = VisualTreeHelper.GetDescendantBounds(child);
                // Siempre desplazamos las coordenadas ya que el método GetDescendantBounds siempre devuelve en 0,0 (hasta lo que yo sé)
                rect.Offset(VisualTreeHelper.GetOffset(child));

                if (rect.Contains(pointRelativeToParent)) {
                    children.Add(child);
                }
            }

            return children;
        }

        public static IEnumerable<UIElement> GetChildrenInsideBounds(this Panel panel, Rect boundsRelativeToParent) {

            var children = new List<UIElement>();

            foreach (UIElement child in panel.Children) {

                var rect = VisualTreeHelper.GetDescendantBounds(child);
                // Siempre desplazamos las coordenadas ya que el método GetDescendantBounds siempre devuelve en 0,0 (hasta lo que yo sé)
                rect.Offset(VisualTreeHelper.GetOffset(child));

                if (rect.Contains(boundsRelativeToParent)) {
                    children.Add(child);
                }
            }

            return children;
        }

        public static void MoveRelative(this UIElement uiElement, Vector deltaPoint) {

            var currentPoint = new Point(Canvas.GetLeft(uiElement), Canvas.GetTop(uiElement));
            var finalPoint = currentPoint + deltaPoint;

            Canvas.SetLeft(uiElement, finalPoint.X);
            Canvas.SetTop(uiElement, finalPoint.Y);
        }
    }
}
