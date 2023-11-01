using System.Net;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.Testing;
using MyTestableAPI.Api.Controllers;

namespace MyTestableAPI.Tests;

public class UnitAPIPays
{


    // Feature : Superficie d’un pays choisi

    // Scénario : Je récupère la superficie d’un pays choisi existant
    // GIVEN Le pays choisi est « France »
    // WHEN Je demande sa superficie
    // THEN Je récupère en JSON « La superficie de France est de 551695 km².»
    [Fact]
    public async Task GetSuperficieFromPaysOK()
    {
        await using var _factory = new WebApplicationFactory<Program>();
        var client = _factory.CreateClient();

        var response = await client.GetAsync("Superficies/France");
        string stringResponse = await response.Content.ReadAsStringAsync();

        response.EnsureSuccessStatusCode();

        Assert.Equal("La superficie de France est de 551695 km².", stringResponse);
    }


    // Scénario : J’entre un pays n’existant pas
    // GIVEN Le pays choisi est « Wakanda »
    // WHEN Je demande sa superficie
    // THEN je récupère le JSON «Le pays est introuvable.»	
    [Fact]
    public async Task GetSuperficieFromPaysNotFound()
    {
        await using var _factory = new WebApplicationFactory<Program>();
        var client = _factory.CreateClient();

        var response = await client.GetAsync("Superficies/Wakanda");
        string stringResponse = await response.Content.ReadAsStringAsync();


        Assert.Equal("Le pays est introuvable.", stringResponse);
    }



    //  Scénario : J’entre un pays qui n’a pas de superficie
    //  GIVEN Le pays choisi est «Vatican»
    //  WHEN Je demande sa superficie
    //  THEN je récupère le JSON «Nous n'avons pas la superficie du pays : Vatican.»	
    [Fact]
    public async Task GetSuperficieFromPaysWithNoSuperficie()
    {
        await using var _factory = new WebApplicationFactory<Program>();
        var client = _factory.CreateClient();

        var response = await client.GetAsync("Superficies/Vatican");
        string stringResponse = await response.Content.ReadAsStringAsync();

        Assert.Equal("Nous n'avons pas la superficie du pays : Vatican.", stringResponse.ToString());
    }


    // Feature : Liste des pays avec leurs superficies

    // Scénario : Affichage de toutes les superficies de tous les pays
    // GIVEN Tous les pays sont présents
    // WHEN Je récupère leurs superficies
    // THEN Je récupère en JSON « [{\"pays\":\"France\",\"superficie\":551695},{\"pays\":\"Allemagne\",\"superficie\":357022}, ...»
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