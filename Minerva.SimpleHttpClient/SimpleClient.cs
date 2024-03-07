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

using Minerva.SimpleHttpClient.Framework;

namespace Minerva.SimpleHttpClient;

public class SimpleClient : ISimpleClient {
    private readonly IClientConfig _config;

    private readonly HttpClient _client;

    public SimpleClient (IClientConfig config) {
        _config = config;

        _client = _config.ClientFactory.CreateClient ();

        _client.BaseAddress = new Uri (_config.BaseUrl);
        _client.DefaultRequestVersion = _config.DefaultRequestVersion ?? _client.DefaultRequestVersion;
        _client.DefaultVersionPolicy = _config.DefaultVersionPolicy ?? _client.DefaultVersionPolicy;
        _client.MaxResponseContentBufferSize = _config.MaxResponseContentBufferSize > 0 ? _config.MaxResponseContentBufferSize : _client.MaxResponseContentBufferSize;
        _client.Timeout = _config.Timeout ?? _client.Timeout;

        if(config.DefaultRequestHeaders != null) {
            foreach(var header in config.DefaultRequestHeaders) {
                _client.DefaultRequestHeaders.Add (header.Key, header.Value);
            }
        }
    }

    public async Task<HttpResponseMessage> GetAsync (IRequestConfig config) => await _client.GetAsync (config.Target, config.HttpCompletionOption, config.CancellationToken);

    public async Task<HttpResponseMessage> PostAsync (IRequestWithContentConfig config) => await _client.PostAsync (config.Target, config.Content, config.CancellationToken);

    public async Task<HttpResponseMessage> PutAsync (IRequestWithContentConfig config) => await _client.PutAsync (config.Target, config.Content, config.CancellationToken);

    public async Task<HttpResponseMessage> PatchAsync (IRequestWithContentConfig config) => await _client.PatchAsync (config.Target, config.Content, config.CancellationToken);

    public async Task<HttpResponseMessage> DeleteAsync (IRequestConfig config) => await _client.DeleteAsync (config.Target, config.CancellationToken);
}
