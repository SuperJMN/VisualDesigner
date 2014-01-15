using System.Xml.Serialization;

namespace SampleModel.Serialization
{
    [XmlInclude(typeof(RectangleDto))]
    [XmlInclude(typeof(EllipseDto))]
    public class ShapeDto: ObjectDto
    {
        [XmlAttribute]
        public Color FillColor { get; set; }
    }
}