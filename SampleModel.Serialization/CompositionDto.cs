using System.Collections.Generic;
using System.Xml.Serialization;

namespace SampleModel.Serialization
{
    [XmlRoot("Composition")]
    public class CompositionDto
    {
        
        public CompositionDto()
        {
            
        }
        
        [XmlArray("Objects")]
        [XmlArrayItem("Object")]
        public List<ObjectDto> Objects { get; set; }
        
        
    }
}