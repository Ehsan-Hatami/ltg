using AutoMapper;
using LiveTixGroup.Models;

namespace LiveTixGroup.Service.Mappers;

public class LtgEntitiesMapping : Profile
{
	public LtgEntitiesMapping()
	{
		CreateMap<AlbumResponse, AlbumModel>(MemberList.None)
			.ForMember(x => x.Id, x => x.MapFrom(y => y.Id))
			.ForMember(x => x.Title, x => x.MapFrom(y => y.Title));

		CreateMap<PhotoResponse, PhotoModel>(MemberList.None)
			.ForMember(x => x.Id, x => x.MapFrom(y => y.Id))
			.ForMember(x => x.Title, x => x.MapFrom(y => y.Title))
			.ForMember(x => x.ThumbnailUrl, x => x.MapFrom(y => y.ThumbnailUrl))
			.ForMember(x => x.Url, x => x.MapFrom(y => y.Url));
	}
}