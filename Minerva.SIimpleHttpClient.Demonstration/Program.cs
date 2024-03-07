/* Simple Http Client - a simple Http client.
 *  Copyright (C) 2024  Raven Crowe
 *
 *  This program is free software: you can redistribute it and/or modify
 *  it under the terms of the GNU General Public License as
 *  published by the Free Software Foundation, either version 3 of the
 *  License, or (at your option) any later version.
 *
 *  This program is distributed in the hope that it will be useful,
 *  but WITHOUT ANY WARRANTY; without even the implied warranty of
 *  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 *  GNU General Public License for more details.
 *
 *  You should have received a copy of the GNU General Public License
 *  along with this program.  If not, see <https://www.gnu.org/licenses/>.
 */

using Minerva.SimpleHttpClient;
using Minerva.SimpleHttpClient.Framework;
using System.Net;
using System.Net.Http.Headers;

var tokenSource = new CancellationTokenSource ();

var client = new SimpleClient (new ClientConfig());

var response = await client.GetAsync (new RequestConfig {
    Target = "posts/1",
    CancellationToken = tokenSource.Token
});

if (response.IsSuccessStatusCode) {
    Console.WriteLine(await response.Content.ReadAsStringAsync());
}
else {
    Console.WriteLine($"{response.StatusCode}");
}

Console.ReadKey();

internal class ClientConfig : IClientConfig {
    public IHttpClientFactory ClientFactory { get; } = DefaultApiRequestClientFactory.Instance;

    public string BaseUrl { get; } = @"https://jsonplaceholder.typicode.com/";

    public Version? DefaultRequestVersion { get; } = HttpVersion.Version11;

    public HttpRequestHeaders? DefaultRequestHeaders { get; } = null;

    public long MaxResponseContentBufferSize { get; } = 2000000000;

    public HttpVersionPolicy? DefaultVersionPolicy { get; } = HttpVersionPolicy.RequestVersionOrHigher;

    public TimeSpan? Timeout { get; } = new TimeSpan (5000000000);
}

internal class RequestConfig : IRequestConfig {
    public string? Target { get; set; }

    public HttpCompletionOption HttpCompletionOption { get; set; } = HttpCompletionOption.ResponseContentRead;

    public CancellationToken CancellationToken { get; set; }
}