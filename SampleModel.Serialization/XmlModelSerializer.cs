using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using AutoMapper;
using Glass.Design.Pcl.CanvasItem;
using Glass.Design.Pcl.CanvasItem.NotifyPropertyChanged;

namespace SampleModel.Serialization
{
    public class XmlModelSerializer
    {
        private readonly Stream stream;
        private readonly XmlSerializer serializer;

        static XmlModelSerializer()
        {
            Mapper.CreateMap<ICanvasItem, ObjectDto>()
                .ForMember(dto => dto.Objects, expression => expression.ResolveUsing(item => item.Children.Count == 0 ? null : item.Children))
                .Include<Mario, MarioDto>()
                .Include<Sonic, SonicDto>()
                .Include<Link, LinkDto>()
                .Include<Group, GroupDto>();


            Mapper.CreateMap<ObjectDto, CanvasItemINPC>()
                .AfterMap((dto, inpc) =>
                          {
                              var mappedChildren = dto.Objects.Select(Mapper.Map<CanvasItemINPC>);
                              foreach (var canvasItem in mappedChildren)
                              {
                                  inpc.Children.Add(canvasItem);
                              }
                          })
                .ForMember(inpc => inpc.Parent, expression => expression.Ignore())
                .ForMember(inpc => inpc.Children, expression => expression.Ignore())
                .Include<MarioDto, Mario>()
                .Include<SonicDto, Sonic>()
                .Include<LinkDto, Link>()
                .Include<GroupDto, Group>();

            Mapper.CreateMap<MarioDto, Mario>();
            Mapper.CreateMap<SonicDto, Sonic>();
            Mapper.CreateMap<LinkDto, Link>();
            Mapper.CreateMap<GroupDto, Group>();


            Mapper.AssertConfigurationIsValid();
        }

        public XmlModelSerializer(Stream stream)
        {


            this.stream = stream;

            serializer = new XmlSerializer(typeof(CompositionDto));


        }

        public void Serialize(IList<ICanvasItem> items)
        {
            var objects = Mapper.Map<List<ObjectDto>>(items);

            var compositionDto = new CompositionDto
                                 {
                                     Objects = objects
                                 };

            serializer.Serialize(stream, compositionDto);
        }

        public IEnumerable<ICanvasItem> Deserialize()
        {
            var compositionDto = (CompositionDto)serializer.Deserialize(stream);
            var objectDtos = compositionDto.Objects;
            var items = Mapper.Map<List<CanvasItemINPC>>(objectDtos);
            return items.Cast<ICanvasItem>().ToList();
        }
    }
}
