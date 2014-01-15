using System.Collections.Generic;
using System.Xml.Serialization;

namespace SampleModel.Serialization
{
    [XmlInclude(typeof(MarioDto))]
    [XmlInclude(typeof(LinkDto))]
    [XmlInclude(typeof(SonicDto))]
    [XmlInclude(typeof(GroupDto))]
    [XmlInclude(typeof(ShapeDto))]
    public class ObjectDto
    {
        public ObjectDto()
        {
           
        }

        [XmlAttribute]
        public double Left { get; set; }
        [XmlAttribute]
        public double Top { get; set; }
        [XmlAttribute]
        public double Width { get; set; }
        [XmlAttribute]
        public double Height { get; set; }

        [XmlArray("Objects")]
        [XmlArrayItem("Object")]
        public List<ObjectDto> Objects { get; set; }
    }
}