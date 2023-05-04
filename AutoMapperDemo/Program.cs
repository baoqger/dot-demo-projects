using AutoMapper;
using AutoMapperDemo.Model;

var config = new MapperConfiguration(cfg =>
{
    cfg.CreateMap<Source, Destination>()
        .ForAllMembers(opt => opt.Ignore());

    cfg.CreateMap<Source, Destination>()
        .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.FirstName + " " + src.LastName));
        // .ForMember(dest => dest.Street, opt => opt.MapFrom(src => src.Address.Split(',', StringSplitOptions.None)[0]))
        // .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.Address.Split(',', StringSplitOptions.None)[1]));
        
});

var mapper = config.CreateMapper();

var source = new Source { Id = 1, FirstName = "Jennifer", LastName = "Lawrence", Address = "123 Queen St, Hollywood" };
var destination = mapper.Map<Source, Destination>(source);

Console.WriteLine($"debug: {destination.Id}, {destination.FullName}, {destination.Street}, {destination.City}");


Console.ReadLine();




