using System.Text.Json;

namespace LiveTixGroup.Service;

public class HttpCallHandler : IHttpCallHandler
{
	private readonly IHttpClientFactory _clientFactory;

	public HttpCallHandler(IHttpClientFactory clientFactory)
	{
		_clientFactory = clientFactory;
	}

	public async Task<IList<T>> GetMetaData<T>(string endpoint)
	{
		using var client = _clientFactory.CreateClient("placeholderEndpoint");
		var photoResponse = await client.GetAsync(endpoint);

		var responseStream = await photoResponse.Content.ReadAsStreamAsync();

		return await JsonSerializer.DeserializeAsync<IList<T>>(responseStream);
	}
}