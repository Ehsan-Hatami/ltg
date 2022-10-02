namespace LiveTixGroup.Service;

public interface IHttpCallHandler
{
	/// <summary>
	/// Encapsulates external http calls.
	/// Due to scope of this project, a generic method could accomodate all necessary calls.
	/// </summary>
	/// <param name="endpoint"></param>
	/// <returns></returns>
	Task<IList<T>> GetMetaData<T>(string endpoint);
}
