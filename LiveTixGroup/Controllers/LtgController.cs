using LiveTixGroup.Service;
using Microsoft.AspNetCore.Mvc;

namespace LiveTixGroup.Controllers;

[Route("[controller]"), ApiController, Produces("application/json")]
public class LtgController : Controller
{
	private readonly IPhotoAlbumGetter _photoAlbumGetter;

	public LtgController(IPhotoAlbumGetter photoAlbumGetter)
	{
		_photoAlbumGetter = photoAlbumGetter;
	}

	/// <summary>
	/// Gets aggregated result for photo and album 
	/// </summary>
	/// <param name="userId">
	/// Results can be filtered based on userId
	/// </param>
	/// <returns>filtered(if userId is provided) aggregated photo and album result</returns>
	/// <response code="200">Returns the results</response>
	/// <response code="400">If request fails</response>
	/// <response code="500">Any internal error</response>
	[HttpGet]
	[Route("GetAggregatedPhotoAlbum")]
	public async Task<IActionResult> Get(int? userId)
	{
		var response = await _photoAlbumGetter.Get(userId ?? default);

		return Ok(response);
	}
}