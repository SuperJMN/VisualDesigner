namespace ComicDesigner.Controls.ColorPicker
{
    public struct HSV
    {
        public float Hue;
        public float Saturation;
        public float Value;

        public HSV(float hue, float saturation, float value)
        {
            Hue = hue;
            Saturation = saturation;
            Value = value;
        }
    }
}
