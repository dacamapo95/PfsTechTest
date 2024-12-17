using Contracts;
using Microsoft.AspNetCore.Mvc;
using PfsTechClient.Models;
using System.Diagnostics;
using System.Text;
using System.Text.Json;

namespace PfsTechClient.Controllers;
public class HomeController(ILogger<HomeController> logger, IHttpClientFactory httpClientFactory) : Controller
{
    private readonly ILogger<HomeController> _logger = logger;
    private readonly HttpClient _httpClient = httpClientFactory.CreateClient("NumbersApi");

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    [HttpPost]
    public async Task<IActionResult> CalculateFactorial(int number)
    {
        var response = await _httpClient.GetAsync($"/numbers/factorial?number={number}");
        if (response.IsSuccessStatusCode)
        {
            var result = await response.Content.ReadFromJsonAsync<FactorialResponse>();
            ViewBag.FactorialResult = result.Value;
        }
        else
        {
            ViewBag.FactorialResult = "Error calculating factorial.";
        }

        return View("Index");
    }

    [HttpPost]
    public async Task<IActionResult> SortNumbers(string numbers)
    {
        var numberArray = numbers.Split(',').Select(int.Parse).ToArray();
        var content = new StringContent(JsonSerializer.Serialize(numberArray), Encoding.UTF8, "application/json");

        var response = await _httpClient.PostAsync("/numbers/sort", content);
        if (response.IsSuccessStatusCode)
        {
            var result = await response.Content.ReadAsStringAsync();
            var sortedNumbers = JsonSerializer.Deserialize<int[]>(result);
            ViewBag.SortedNumbers = sortedNumbers;
        }
        else
        {
            ViewBag.SortedNumbers = "Error sorting numbers.";
        }

        return View("Index");
    }
}
