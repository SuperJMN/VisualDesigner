using System;
using Design.Interfaces;
using Glass.Design.Converters;
using Glass.Design.DesignSurface.VisualAids;

namespace Glass.Design
{
    public static class ThumbCursorConverterFactory
    {
        public static IThumbCursorConverter Create(IResizeThumb resizeThumb)
        {
            if (resizeThumb is ResizeThumb)
            {
                return new WindowsSizeCursorsThumbCursorConverter();
            }

            throw new ArgumentException("There is no associated converter for this thumb", "resizeThumb");
        }
    }
}