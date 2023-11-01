using System.Net;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.Testing;
using MyTestableAPI.Api.Controllers;

namespace MyTestableAPI.Tests;

public class UnitAPIPays
{
    [Fact]
    public async Task GetSuperficieFromPaysOK()
    {
        await using var _factory = new WebApplicationFactory<Program>();
        var client = _factory.CreateClient();

        var response = await client.GetAsync("Superficies?pays=France");
        string stringResponse = await response.Content.ReadAsStringAsync();

        response.EnsureSuccessStatusCode();

        Assert.Equal("La superficie de France est de 551695 km².", stringResponse);
    }

    [Fact]
    public async Task GetSuperficieFromPaysNotFound()
    {
        await using var _factory = new WebApplicationFactory<Program>();
        var client = _factory.CreateClient();

        var response = await client.GetAsync("Superficies?pays=Wakanda");
        string stringResponse = await response.Content.ReadAsStringAsync();


        Assert.Equal("Le pays est introuvable.", stringResponse);
    }

    [Fact]
    public async Task GetSuperficieFromPaysWithNoSuperficie()
    {
        await using var _factory = new WebApplicationFactory<Program>();
        var client = _factory.CreateClient();

        var response = await client.GetAsync("Superficies?pays=Vatican");
        string stringResponse = await response.Content.ReadAsStringAsync();

        Assert.Equal("Nous n'avons pas la superficie du pays : Vatican.", stringResponse.ToString());
    }

    [Fact]
    public async Task GetAllSuperficies()
    {
        await using var _factory = new WebApplicationFactory<Program>();
        var client = _factory.CreateClient();
        String expectedResponse = "[{\"pays\":\"France\",\"superficie\":551695},{\"pays\":\"Allemagne\",\"superficie\":357022},{\"pays\":\"Espagne\",\"superficie\":505990},{\"pays\":\"Vatican\",\"superficie\":0}]";


        var response = await client.GetAsync("Superficies");
        string stringResponse = await response.Content.ReadAsStringAsync();

        response.EnsureSuccessStatusCode();

        Assert.Equal(expectedResponse, stringResponse);
    }

}