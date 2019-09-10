using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Runtime.Serialization.Json;

namespace WebAPIClient
{
  class RESTClient
  {
    private readonly HttpClient httpClient;
    private readonly string gitHubURL;

    public RESTClient()
    {
      httpClient = new HttpClient();
      gitHubURL = "https://api.github.com/orgs/dotnet/repos";
    }

    private async Task<List<Repository>> ProcessRepositories()
    {
      httpClient.DefaultRequestHeaders.Accept.Clear();
      httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/vnd.github.v3+json"));
      httpClient.DefaultRequestHeaders.Add("User-Agent", ".NET Foundation Repository Reporter");

      var serializer = new DataContractJsonSerializer(typeof(List<Repository>));

      var streamTask = httpClient.GetStreamAsync(gitHubURL);

      var repositories = serializer.ReadObject(await streamTask) as List<Repository>;

      return repositories;
    }

    public void GetRepositories()
    {
      List<Repository> repositories = ProcessRepositories().Result;

      repositories.ForEach(repo =>
      {
        Console.WriteLine(repo.Name);
        Console.WriteLine(repo.Description);
        Console.WriteLine(repo.GitHubHomeUrl);
        Console.WriteLine(repo.Homepage);
        Console.WriteLine(repo.Watchers);
        Console.WriteLine(repo.LastPush);
        Console.WriteLine();
      });
    }
  }
}