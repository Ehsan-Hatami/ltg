using AutoMapper;
using LiveTixGroup.Models;

namespace LiveTixGroup.Service
{
	public class Aggregator : IAggregator
	{
		private readonly IMapper _mapper;

		public Aggregator(IMapper mapper)
		{
			_mapper = mapper;
		}

		public IList<PhotoAlbumAggregatedModel> Aggregate(int userId, IList<PhotoResponse> photos, IList<AlbumResponse> albums)
		{
			var albumGroupedResult = albums
				.GroupBy(album => album.UserId)
				.Select(x =>
					new
					{
						userId = x.Key,
						albumModels = x.Select(m => _mapper.Map<AlbumModel>(m)).ToList()
					});

			var photoGroupedResult = photos
				.GroupBy(photo => photo.AlbumId)
				.Select(x =>
					new
					{
						albumId = x.Key,
						photoModels = x.Select(m => _mapper.Map<PhotoModel>(m)).ToList()
					});

			//if condition to avoid unnecessary where condition
			var filteredGroup = userId != default
				? albumGroupedResult.Where(x => x.userId == userId).ToList()
				: albumGroupedResult.ToList();


			foreach (var album in filteredGroup.SelectMany(x => x.albumModels))
			{
				album.PhotoModels = photoGroupedResult.FirstOrDefault(x => x.albumId == album.Id)?.photoModels;
			}

			var result = filteredGroup.Select(x =>
				new PhotoAlbumAggregatedModel
				{
					UserId = x.userId,
					AlbumModels = x.albumModels
				}).ToList();

			return result;
		}
	}
}