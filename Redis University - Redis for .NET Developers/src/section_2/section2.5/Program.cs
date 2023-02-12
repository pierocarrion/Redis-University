using StackExchange.Redis;
using System.Net.Security;
using System.Reflection.Metadata;
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
var muxer = ConnectionMultiplexer.Connect(options);
var db = muxer.GetDatabase();

// TODO for Coding Challenge Start here on starting-point branch
var person1 = "person:1";
var person2 = "person:2";
var person3 = "person:3";

db.KeyDelete(new RedisKey[]{person1, person2, person3});

db.HashSet(person1, new HashEntry[]
{
    new("name","Alice"),
    new("age", 33),
    new("email","alice@example.com")
});

db.HashSet(person2, new HashEntry[]
{
    new("name","Bob"),
    new("age", 27),
    new("email","robert@example.com")
});

db.HashSet(person3, new HashEntry[]
{
    new("name","Charlie"),
    new("age", 50),
    new("email","chuck@example.com")
});

var newAge = db.HashIncrement(person3, "age");
Console.WriteLine($"person:3 new age: {newAge}");

var person1Name = db.HashGet(person1, "name");
Console.WriteLine($"person:1 name: {person1Name}");

// HashGetAll
var person2Fields = db.HashGetAll(person2);
Console.WriteLine($"person:2 fields: {string.Join(", ", person2Fields)}");

// HashScan
var person3Fields = db.HashScan(person3);
Console.WriteLine($"person:3 fields: {string.Join(", ", person3Fields)}");
// end coding challenge

// Result
// person: 3 new age: 51
// person: 1 name: Alice
// person:2 fields: name: Bob, age: 27, email: robert @example.com
// person:3 fields: name: Charlie, age: 51, email: chuck @example.com