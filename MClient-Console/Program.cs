using MClient;

Client client = new Client("ws://localhost:8080", "Console");

client.OnMessage += (sender, e) =>
{
    Console.WriteLine(e.sender + ": " + e.message);
};

while (true) {}