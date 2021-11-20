using IdentityModel.Client;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace ConsoleApp
{
    class Program
    {
        static async Task Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("Press any key to continue calling Identity service");
                Console.ReadKey();
                Console.Clear();

                Console.WriteLine("Calling Identity service...");

                var client = new HttpClient();

                var discovery = await client.GetDiscoveryDocumentAsync("https://localhost:6001");

                if (discovery.IsError)
                {
                    Console.WriteLine($"Discovery error: {discovery.Error}");
                    continue;
                }

                var tokenResponse = await client.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
                {
                    Address = discovery.TokenEndpoint,
                    ClientId = "ClientApp",
                    ClientSecret = "ClientSecret",
                    Scope = "Api"
                });

                if (tokenResponse.IsError)
                {
                    Console.WriteLine($"Token response error: {tokenResponse.Error}");
                    continue;
                }

                client.SetBearerToken(tokenResponse.AccessToken);


                var response = await client.GetAsync("https://localhost:5001/identity/Get");
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    Console.Clear();
                    Console.WriteLine($"Token {tokenResponse.TokenType} response: {tokenResponse.AccessToken}\n");
                    Console.WriteLine($"Identity controller response: {content}\n");
                }
                else
                {
                    Console.WriteLine($"Controller error code: {response.StatusCode}");
                }
            }
        }
    }
}

