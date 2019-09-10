using WebAPIClient;

namespace core_console
{
  class Program
  {
    private readonly static RESTClient restClient = new RESTClient();
    static void Main(string[] args)
    {
      restClient.GetRepositories();
    }
  }
}
