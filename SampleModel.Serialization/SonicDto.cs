using System.Xml.Serialization;
using Glass.Design.Pcl.Annotations;

namespace SampleModel.Serialization
{
    [UsedImplicitly]
    [XmlType("Sonic")]
    public class SonicDto : ObjectDto
    {

    }

    [UsedImplicitly]
    [XmlType("Label")]
    public class LabelDto : ObjectDto
    {
        private string text;

        public string Text
        {
            get { return text; }
            set { text = value; }
        }
    }
}