# Simple WebSockets Demo

This is a demo to test running a small socket server in a dotnet core console app and connecting to it via websockets from javascript.

## Server

_Based on https://github.com/paulbatum/WebSocket-Samples/blob/master/HttpListenerWebSocketEcho/Server/Server.cs_

* dotnet core console app
* Uses `System.Net.HttpListener` ([docs](https://docs.microsoft.com/en-us/dotnet/api/system.net.httplistener?view=netcore-2.1)) and `System.Net.WebSockets` ([docs](https://docs.microsoft.com/en-us/dotnet/api/system.net.websockets?view=netcore-2.1))

## Client

_Based on https://codepen.io/matt-west/details/tHlBb_

* Simple html page with vanilla js
* Uses the `WebSocket` api ([docs](https://developer.mozilla.org/en-US/docs/Web/API/WebSocket))

## Usage

### Server

1. Make sure Visual studio is running as an administrator
2. Run console app

### Client

1. Run `npm i`
2. Run `npm run serve`
3. Type your message and click "Send" and you should see your message echoed back, both on the console and the site
