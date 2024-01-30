using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Movies.Core.Models;
using Movies.Infrastructure.Interfaces;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.Annotations;

namespace Movies.API.Controllers;

[Route("movie/api/v1/")]
[ApiController]
//[Authorize]
public class MovieController : ControllerBase
{
    private readonly IMovieApi _service;
    private readonly ILogger _logger;

    public MovieController(IMovieApi service, ILogger logger)
    {
        _service = service;
        _logger = logger;
    }

    [HttpGet("getmovie")]
    [SwaggerOperation(Summary = "getMovie")]
    [SwaggerResponse(StatusCodes.Status200OK, "Request Successful", typeof(List<MovieResponse>))]
    public async Task<IActionResult> GetMovie()
    {
        
        var response = await _service.GetMovie();
        _logger.LogInformation("Get all movie response", JsonConvert.SerializeObject(response));
        return Ok(response);
    }


    [HttpGet("getmovie/{id:int}")]
    [SwaggerOperation(Summary = "GetMovie by Id ")]
    [SwaggerResponse(StatusCodes.Status200OK, "Request Successful", typeof(MovieResponse))]
    public async Task<IActionResult> GetMovieById(GetMovieByIdRequest request)
    {
        _logger.LogInformation("Get movie request", JsonConvert.SerializeObject(request));
        var response = await _service.GetMovieById(request);
        _logger.LogInformation("Get movie response", JsonConvert.SerializeObject(response));
        return Ok(response);
    }


    [HttpPost("postmovie")]
    [SwaggerOperation(Summary = "PostMovie")]
    [SwaggerResponse(StatusCodes.Status200OK, "Request Successful", typeof(MovieResponse))]
    public async Task<IActionResult> PostMovie(PostMovieRequest request)
    {
        _logger.LogInformation("post movie request", JsonConvert.SerializeObject(request));
        var response = await _service.PostMovie(request);
        _logger.LogInformation("post movie response", JsonConvert.SerializeObject(response));
        return Ok(response);
    }


    [HttpPut("updatemovie")]
    [SwaggerOperation(Summary = "Update Movie")]
    [SwaggerResponse(StatusCodes.Status200OK, "Request Successful", typeof(MovieResponse))]
    public async Task<IActionResult> UpdateMovie(UpdateMovieRequest request)
    {
        _logger.LogInformation("update movie request", JsonConvert.SerializeObject(request));
        var response = await _service.UpdateMovie(request);
        _logger.LogInformation("Update movie response", JsonConvert.SerializeObject(response));
        return Ok(response);
    }


    [HttpPut("deletemovie")]
    [SwaggerOperation(Summary = "Delete Movie")]
    [SwaggerResponse(StatusCodes.Status200OK, "Request Successful", typeof(bool))]
    public async Task<IActionResult> DeleteMovie(Guid id)
    {
        _logger.LogInformation("delete movie request", JsonConvert.SerializeObject(id));
        var response = _service.DeleteMovie( id);
        _logger.LogInformation("delete movie response", JsonConvert.SerializeObject(response));
        return Ok();
    }
}