using PseConnector.Data.Endpoints.Rce;

Console.WriteLine("Hello, World!");

var httpClient = new HttpClient { BaseAddress = new Uri("https://api.raporty.pse.pl/api/") };

var dateFrom = new DateOnly(2025, 3, 1);
var dateTo = new DateOnly(2025, 3, 10);

var rceQueryWithFilter =
    $"rce-pln?$first=10000&$filter=doba%20ge%20'{dateFrom:yyyy-MM-dd}'%20and%20doba%20le%20'{dateTo:yyyy-MM-dd}'";
var response = await httpClient.GetStringAsync(rceQueryWithFilter);

Console.WriteLine(response);

Console.WriteLine("Bye, World!");
