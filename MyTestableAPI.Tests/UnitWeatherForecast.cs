using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.Testing;
using MyTestableAPI.Api.Controllers;

namespace MyTestableAPI.Tests;

public class UnitWeatherForecast
{
    [Fact]
    public async Task IsGetWeatherforecastOK()
    {
        await using var _factory = new WebApplicationFactory<Program>();
        var client = _factory.CreateClient();

        var response = await client.GetAsync("WeatherForecast");
        string stringResponse = await response.Content.ReadAsStringAsync();

        response.EnsureSuccessStatusCode();

        //Assert.True(response.StatusCode.ToString().Equals("OK"));
        Assert.True(response.IsSuccessStatusCode);
    }
}