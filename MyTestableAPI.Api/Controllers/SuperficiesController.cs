using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

namespace MyTestableAPI.Api.Controllers;

[ApiController]
public class SuperficiesController : ControllerBase
{

    private readonly ILogger<SuperficiesController> _logger;

    public SuperficiesController(ILogger<SuperficiesController> logger)
    {
        _logger = logger;
    }

    // Cette liste fictive contient des exemples de pays et de superficie.
    private List<SuperficiesModel> paysSuperficieList = new List<SuperficiesModel>
    {
        new SuperficiesModel { Pays = "France", Superficie = 551695 },
        new SuperficiesModel { Pays = "Allemagne", Superficie = 357022 },
        new SuperficiesModel { Pays = "Espagne", Superficie = 505990 },
        new SuperficiesModel { Pays = "Vatican", Superficie = 0 },
    };

    [HttpGet("superficies", Name = "GetAllSuperficies")]
    public IActionResult GetAll()
    {
        return Ok(paysSuperficieList.ToArray());
    }

    [HttpGet("superficies/{pays}", Name = "GetSuperficie")]
    public IActionResult Get(string pays)
    {
        if (pays == null)
        {
            return Ok(paysSuperficieList.ToArray());
        }

        var paysInfo = paysSuperficieList.Find(p => p.Pays.Equals(pays, StringComparison.OrdinalIgnoreCase));

        if (paysInfo == null)
        {
            return NotFound($"Le pays est introuvable.");
        }else
        if(paysInfo.Superficie == 0){
            return Ok($"Nous n'avons pas la superficie du pays : {paysInfo.Pays}.");
        }

        return Ok($"La superficie de {paysInfo.Pays} est de {paysInfo.Superficie} km².");
    }
}

