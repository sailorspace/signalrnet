# signalrnet
SignalR was created in 2011 by David Fowler and Damian Edwards.
To achieve real-time messaging for the Web, developers used inefficient techniques such as 
AJAX polling and 
long-polling, and technologies like 
server-sent events that weren't broadly implemented by browsers

SignalR negotiates the best protocol to use for a specified connection based on what's supported by both the server and the client.
It then provides a consistent API for sending and receiving messages in real-time
When SignalR was created, almost every Web application used jQuery, so it was an easy decision for the SignalR JavaScript client to depend on jQuery as well
WebSockets are available in all major browsers and are now the standard for real-time Web communications.
WebSocket is a computer communications protocol, providing a simultaneous two-way communication channel over a single Transmission Control Protocol connection.
WebSockets are used for real-time, event-driven communication between clients and servers. They are particularly useful for building software applications requiring 
instant updates, such as real-time chat, messaging, and multiplayer games
 
 - clients sends a http GET request with a upgrade header, so that the server know that this is a upgrade request and it responds with status 101 
   if the server supports the upgrade properties and return error code if not.
 - If any code other than 101 is returned from the server, Clients has to end the connection.
 - Websockets are consider as they make a single connection between client and server and there is no overhead of making those handshakes with the server every time 
   we have to communicate.
 
# Client side request headers look like this:
 GET /chat HTTP/1.1
        Host: server.example.com
        Upgrade: websocket
        Connection: Upgrade
        Sec-WebSocket-Key: dGhlIHNhbXBsZSBub25jZQ==
        Origin: http://example.com
        Sec-WebSocket-Protocol: chat, superchat
        Sec-WebSocket-Version: 13

- We are making a GET Request over http. (version needs to be greater or equals to 1.1)
- Host name is added in the header so that both the client and server verify that they agree on which host is in use.
- Sec-WebSocket-Protocol is send by the client to specify which protocol to use (OPTIONAL)
- Origin header is used to protect against Unauthorised cross-origin use of a WebSocket server by scripts using the WebSocket API in a web browser. 
- Server will only accept connections from listed origins.
- Sec-WebSocket-key is the base64 encoded value that is generated by randomly selecting 16-byte value as a nonce.

# Server will do following things
- In order to prove that handshake was received, server has to take the Sec-WebSocket-key from the request header , 
 combine it with the Globally Unique Identifier , create a SHA-1 hash of this concatenation string.
- Then it encodes that string using base64 and return as server handshake.
HTTP/1.1 101 Switching Protocols
        Upgrade: websocket
        Connection: Upgrade
        Sec-WebSocket-Accept: s3pPLMBiTxaQ9kYGzzhZRbK+xOo=
        Sec-WebSocket-Protocol: chat

- Responds with a 101 status code, any code other than 101 results in error and means that websocket handshake was not completed.
- The Connection and Upgrade field indicate the HTTP upgrade
- Sec-WebSocket-Accept indicates whether the server is willing to accept the connection request or not. if this field is present, 
  this field contains the base64 encoded hash of Sec-WebSocket-key and Globally Unique Identifier combined.
- Sec-WebSocket-Protocol is an optional field, it indicates which protocol is selected by the server for communication.

These fields are checked by the WebSocket client for scripted pages.If the Sec-WebSocket-Accept value does not match the expected value, 
if the header field is missing, or if the HTTP status code is not 101, the connection will not be established, and WebSocket frames will not be sent.

# Websocket has a default URI format
ws-URI = "ws:" "//" host [ ":" port ] path [ "?" query ]
wss-URI = "wss:" "//" host [ ":" port ] path [ "?" query ]

Port component is optional as default port 80 is used for ws and 443 is used for wss .

Handshake (http upgrade -> ) -> connection opened (<- server response) ->  (<>)Bidirectional Messages -> Open and Persistant connection -> connection closed

The JavaScript/TypeScript client library can now be used without referencing jQuery, allowing it to be used with frameworks such as Angular, React, and Vue without friction. 
In addition, this allows the client to be used in a Node.js application.

SignalR had clients for other languages such as Java, Python, Go, and PHP, all created by Microsoft and the open source community; 
you can expect the same for ASP.NET Core SignalR as its adoption increases.
ASP.NET Core SignalR ships with a new JSON message protocol that's incompatible with earlier versions of SignalR.
In addition, it has a second built-in protocol based on MessagePack, which is a binary protocol that has smaller payloads than the text-based JSON.

ASP.NET didn't have dependency injection built in, so SignalR provided a GlobalHost class that included its own dependency resolver.
SignalR shipped with built-in support for scale-out using Redis, Service Bus, or SQL Server as a backplane.

SignalR uses hubs class to communicate between clients and servers.
A hub is a high-level pipeline class that allows a client and server to call methods to each other. SignalR handles the data communication across multiple boundaries automatically, it allows the clients to call methods which is available on the server and vice versa.

The Hub class contains Context property and many more, which further provides other information about the connection, such as:
- ConnectionAborted
- ConnectionId
- Features
- Items
- User
- UserIdentifier

When we are going to implement SignalR, then there are a set of goals which we need to achieve in this technology that are as follows:
- Connection Management(Handshake)
- Connect and Retry
- Send/Receive message
- Scales to handle increasing traffic.
- Authentication

