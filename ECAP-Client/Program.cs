using ECAP_Client;

Console.WriteLine("Press \"Enter\" to kill my masterpiece...");

var receiver = new ClientManager();
await receiver.ContinuousTyping();
