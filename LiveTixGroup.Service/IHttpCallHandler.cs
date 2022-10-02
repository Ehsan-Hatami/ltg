namespace LiveTixGroup.Service;

public interface IHttpCallHandler
{
	Task<IList<T>> GetMetaData<T>(string endpoint);
}
