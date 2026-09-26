using MinhaApi.Models;
using MinhaApi.Services;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class DepartamentoController : ControllerBase
{
    private readonly IDepartamentoService _service;

    public DepartamentoController(IDepartamentoService service)
    {
        _service = service;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        var departamentos = _service.GetAll();

        return Ok(departamentos);
    }

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var departamento = _service.GetById(id);

        if (departamento == null)
            return NotFound();

        return Ok(departamento);
    }

    [HttpPost]
    public IActionResult Create([FromBody] Departamento departamento)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var criado = _service.Create(departamento);

        return CreatedAtAction(
            nameof(GetById),
            new { id = criado.Id },
            criado
        );
    }

    // PUT /api/produto/1
    [HttpPut("{id}")]
    public IActionResult Update(
        int id,
        [FromBody] Departamento departamento)
    {
        var atualizado = _service.Update(id, departamento);

        if (atualizado == null)
            return NotFound();

        return Ok(atualizado);
    }

    // DELETE /api/produto/1
    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var deletado = _service.Delete(id);

        if (!deletado)
            return NotFound();

        return NoContent();
    }
}