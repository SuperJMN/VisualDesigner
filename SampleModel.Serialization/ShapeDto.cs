using System.Xml.Serialization;

namespace SampleModel.Serialization
{
    [XmlInclude(typeof(RectangleDto))]
    [XmlInclude(typeof(EllipseDto))]
    public class ShapeDto
    {
        [XmlAttribute]
        private Color FillColor { get; set; }
    }
}