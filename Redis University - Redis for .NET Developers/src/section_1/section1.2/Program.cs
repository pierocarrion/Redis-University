using StackExchange.Redis;
using System.Net.Security;
using System.Security.Authentication;

var options = new ConfigurationOptions
{
    EndPoints = new EndPointCollection { "server:port" },
    Password = "PASSWORD",
    User= "default",
    SslClientAuthenticationOptions = new Func<string, SslClientAuthenticationOptions>(
        hostname => new SslClientAuthenticationOptions
        {
            EnabledSslProtocols = SslProtocols.Tls12 | SslProtocols.Tls13
        })
};

// initalize a multiplexer with ConnectionMultiplexer.Connect()
var muxer = ConnectionMultiplexer.Connect(options);

// get an IDatabase here with GetDatabase
var db = muxer.GetDatabase();

Console.WriteLine($"ping: {db.Ping().TotalMilliseconds} ms");
// end programming challenge