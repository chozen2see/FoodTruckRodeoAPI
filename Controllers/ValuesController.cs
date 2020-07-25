using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DatingApp.API.Controllers
{
  // attribute based routing. 
  // will be mapped as endpoint api/[first part of class name without controller]
  // http://localhost:5000/api/values
  [Route("api/[controller]")]
  [ApiController]
  public class ValuesController : ControllerBase
  {
    // to get values from db we need to inject DataContext class into our controller
    private readonly DataContext _context; // use underscore for private vars
    public ValuesController(DataContext context)
    {
      _context = context;

    }

    // GET api/values - READ ALL 
    [HttpGet]
    // replace ActionResult with IActionResult to access HTTP responses
    // make it asynchronous -> async Task<> represents an async operation that can return a value as TResult
    // keeps thread open while waiting on results
    public async Task<IActionResult> GetValues()
    {
      // go do db retrieve data from Values table as a list
      var values = await _context.Values.ToListAsync();
      // return status ok 200 and values
      return Ok(values);
    }

    // GET api/values/5 - READ ONE using route parameter
    [HttpGet("{id}")]
    public async Task<IActionResult> GetValue(int id)
    {
      // use FirstOrDefault so it returns NULL as default if no record found, First will return an exception!
      var value = await _context.Values.FirstOrDefaultAsync(
        // use lambda arrow function to find match for param "id" passed in
        record => record.Id == id
      );

      // return status ok 200 and values
      // 204 no content is ok - couldn't find matching record
      return Ok(value);
    }
    // POST api/values - CREATE
    [HttpPost]
    public void Post([FromBody] string value)
    {
    }
    // PUT api/values/5 - UPDATE
    [HttpPut("{id}")]
    public void Put(int id, [FromBody] string value)
    {
    }
    // DELETE api/values/5
    [HttpDelete("{id}")]
    public void Delete(int id)
    {
    }
  }
}