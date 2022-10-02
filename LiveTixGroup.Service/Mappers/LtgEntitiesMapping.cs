using AutoMapper;
using LiveTixGroup.Models;
using LiveTixGroup.Models.ExternalApiResponses;

namespace LiveTixGroup.Service.Mappers;

public class LtgEntitiesMapping : Profile
{
	public LtgEntitiesMapping()
	{
		CreateMap<AlbumApiResponse, AlbumModel>(MemberList.None)
			.ForMember(x => x.Id, x => x.MapFrom(y => y.Id))
			.ForMember(x => x.Title, x => x.MapFrom(y => y.Title));

		CreateMap<PhotoApiResponse, PhotoModel>(MemberList.None)
			.ForMember(x => x.Id, x => x.MapFrom(y => y.Id))
			.ForMember(x => x.Title, x => x.MapFrom(y => y.Title))
			.ForMember(x => x.ThumbnailUrl, x => x.MapFrom(y => y.ThumbnailUrl))
			.ForMember(x => x.Url, x => x.MapFrom(y => y.Url));
	}
}