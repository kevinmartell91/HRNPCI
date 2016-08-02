using System;
using System.Windows;
using System.Windows.Controls;

namespace UPC.HRNPCI.DesktopApplication.Views.ResultadosReportesPacientes
{
    /// <summary>
    /// Canvas derivative which sizes according to its contents.
    /// </summary>
    public class CustomCanvas : Canvas
    {
        /// <summary>
        /// Measures the child elements of a <see cref="T:System.Windows.Controls.Canvas"/> in anticipation of arranging them during the <see cref="M:System.Windows.Controls.Canvas.ArrangeOverride(System.Windows.Size)"/> pass.
        /// </summary>
        /// <param name="availableSize">An upper limit <see cref="T:System.Windows.Size"/> that should not be exceeded.</param>
        /// <returns>
        /// A <see cref="T:System.Windows.Size"/> that represents the size that is required to arrange child content.
        /// </returns>
        protected override Size MeasureOverride(Size availableSize)
        {
            double width = 0d;
            double height = 0d;

            foreach (object obj in Children)
            {
                FrameworkElement child = obj as FrameworkElement;
                if (child != null)
                {
                    child.Measure(availableSize);
                    double x = GetTop(child) + child.DesiredSize.Height;
                    if (!double.IsInfinity(x) && !double.IsNaN(x))
                        width = Math.Max(width, x);
                    x = GetLeft(child) + child.DesiredSize.Width;
                    if (!double.IsInfinity(x) && !double.IsNaN(x))
                        height = Math.Max(height, x);
                }
            }

            return new Size(height, width);
        }
    }
}
