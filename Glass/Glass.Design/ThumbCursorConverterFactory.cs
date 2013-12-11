using System;
using Design.Interfaces;
using Glass.Design.Converters;

namespace Glass.Design
{
    public class ThumbCursorConverterFactory
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