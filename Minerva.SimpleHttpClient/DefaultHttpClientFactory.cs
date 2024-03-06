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

namespace Minerva.SimpleHttpClient;

public class DefaultApiRequestClientFactory : IHttpClientFactory, IDisposable {
    private static readonly object _instLock = new ();

    public static DefaultApiRequestClientFactory Instance {
        get {
            if (_instance is null) {
                lock (_instLock) {
                    if (_instance is null) {
                        _instance = new DefaultApiRequestClientFactory ();
                    }
                }
            }

            return _instance;
        }
    }

    private DefaultApiRequestClientFactory () { }

    private static DefaultApiRequestClientFactory? _instance;

    private readonly Lazy<HttpMessageHandler> _lazyHandler = new (() => new HttpClientHandler ());

    public HttpClient CreateClient (string name) => new (_lazyHandler.Value, disposeHandler: false);

    public void Dispose () {
        GC.SuppressFinalize (this);

        if (_lazyHandler.IsValueCreated) {
            _lazyHandler.Value.Dispose ();
        }
    }
}
