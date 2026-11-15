// See https://aka.ms/new-console-template for more information
using System.Text;
using System.Text.Json;
using static System.Net.Mime.MediaTypeNames;

Console.WriteLine("Hello, World!");

HttpClient client = new HttpClient();

//HttpResponseMessage responseGet = await client.GetAsync("https://fake-birds-apis.vercel.app/api/v1/birds"); // 5 sec
//HttpResponseMessage responseDelete = await client.DeleteAsync("https://fake-birds-apis.vercel.app/api/v1/birds/1"); // 5 sec
//if (responseGet.IsSuccessStatusCode)
//{
//    string jsonStr = await responseGet.Content.ReadAsStringAsync();
//    Console.WriteLine(jsonStr);
//}

//HttpResponseMessage responseGetById = await client.GetAsync("https://fake-birds-apis.vercel.app/api/v1/birds/1"); // 5 sec
//if (responseGetById.IsSuccessStatusCode)
//{
//    string jsonStr = await responseGetById.Content.ReadAsStringAsync();
//    Console.WriteLine(jsonStr);
//}

BirdCreateRequest request = new BirdCreateRequest
{
    birdEnglishName = "Orange-bellied Leafbird",
    birdMyanmarName = "ငှက်စိမ်းရင်ဝါ",
    description = "A beautiful green bird with a yellow belly.",
    imagePath = "/birds/img/1_Orange-belliedLeafbird.jpg"
};
string jsonCreateRequest = JsonSerializer.Serialize(request);
StringContent content = new StringContent(jsonCreateRequest, System.Text.Encoding.UTF8, Application.Json);
var responseCreate = await client.PostAsync("https://fake-birds-apis.vercel.app/api/v1/birds", content);
if (responseCreate.IsSuccessStatusCode)
{
    string jsonStr = await responseCreate.Content.ReadAsStringAsync();
    Console.WriteLine(jsonStr);
}

//Console.WriteLine("Hello World");
Console.WriteLine(".....");
Console.ReadLine();


public class BirdCreateRequest
{
    public string birdMyanmarName { get; set; }
    public string birdEnglishName { get; set; }
    public string description { get; set; }
    public string imagePath { get; set; }
}
