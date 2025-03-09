using PseConnector.Data;

Console.WriteLine("Hello, World!");

var httpClient = new HttpClient { BaseAddress = new Uri("https://api.raporty.pse.pl/api/") };

var response = await httpClient.GetStringAsync("rce-pln?$filter=doba%20eq%20'2025-03-01'");

Console.WriteLine(response);

Console.WriteLine("Bye, World!");
