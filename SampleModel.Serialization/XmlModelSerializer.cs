using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using AutoMapper;
using Glass.Design.Pcl.CanvasItem;

namespace SampleModel.Serialization
{
    public class XmlModelSerializer
    {
        private readonly Stream stream;
        private readonly XmlSerializer serializer;

        static XmlModelSerializer()
        {
            //Mapper.CreateMap<Color, Color>();

            //Mapper.CreateMap<Shape, ShapeDto>() 
            //    .ForMember(dto => dto.FillColor, expression => expression.MapFrom(shape => shape.FillColor))
            //    .Include<CanvasRectangle, RectangleDto>()
            //    .Include<Ellipse, EllipseDto>();

            Mapper.CreateMap<ICanvasItem, ObjectDto>()
                .ForMember(dto => dto.Objects, expression => expression.ResolveUsing(item => item.Items.Count == 0 ? null : item.Items))
                .Include<Mario, MarioDto>()
                .Include<Sonic, SonicDto>()
                .Include<Link, LinkDto>()
                .Include<Label, LabelDto>()
                //.Include<Shape, ShapeDto>()
                //.Include<Ellipse, EllipseDto>()
                //.Include<CanvasRectangle, RectangleDto>()                
                .Include<Group, GroupDto>();
            


            Mapper.CreateMap<ObjectDto, CanvasItem>()
                .AfterMap((dto, inpc) =>
                          {
                              var mappedChildren = dto.Objects.Select(Mapper.Map<CanvasItem>);
                              foreach (var canvasItem in mappedChildren)
                              {
                                  inpc.Items.Add(canvasItem);
                              }
                          })
                .ForMember(inpc => inpc.Parent, expression => expression.Ignore())
                .ForMember(inpc => inpc.Items, expression => expression.Ignore())
                .Include<MarioDto, Mario>()
                .Include<SonicDto, Sonic>()
                .Include<LinkDto, Link>()
                .Include<LabelDto, Label>()
                //.Include<RectangleDto, CanvasRectangle>()
                //.Include<EllipseDto, Ellipse>()
                .Include<GroupDto, Group>();
                

            Mapper.CreateMap<MarioDto, Mario>();
            Mapper.CreateMap<SonicDto, Sonic>();
            Mapper.CreateMap<LinkDto, Link>();
            //Mapper.CreateMap<RectangleDto, CanvasRectangle>();
            //Mapper.CreateMap<EllipseDto, Ellipse>();
            Mapper.CreateMap<LabelDto, Label>();
            Mapper.CreateMap<GroupDto, Group>();


            Mapper.AssertConfigurationIsValid();
        }

        public XmlModelSerializer(Stream stream)
        {


            this.stream = stream;

            serializer = new XmlSerializer(typeof(ModelDto));


        }

        public void Serialize(CanvasDocument document)
        {
            var objects = Mapper.Map<List<ObjectDto>>(document.Items);

            var compositionDto = new ModelDto
                                 {
                                     Objects = objects
                                 };

            serializer.Serialize(stream, compositionDto);
        }

        public CanvasDocument Deserialize()
        {
            var compositionDto = (ModelDto)serializer.Deserialize(stream);
            var objectDtos = compositionDto.Objects;
            var items = Mapper.Map<List<CanvasItem>>(objectDtos);
            CanvasDocument document = new CanvasDocument(items);
            
            return  document;
        }
    }
}
