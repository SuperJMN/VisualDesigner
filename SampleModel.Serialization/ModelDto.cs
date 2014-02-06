using System.Collections.Generic;
using System.Xml.Serialization;

namespace SampleModel.Serialization
{
    [XmlRoot("Composition")]
    public class ModelDto
    {
        
        public ModelDto()
        {
            
        }
        
        [XmlArray("Objects")]
        [XmlArrayItem("Object")]
        public List<ObjectDto> Objects { get; set; }
        
        
    }
}