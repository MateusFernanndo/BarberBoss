using BarberBoss.Application.Usecase.Billing.Delete;
using BarberBoss.Application.Usecase.Billing.GetAll;
using BarberBoss.Application.Usecase.Billing.GetById;
using BarberBoss.Application.Usecase.Billing.Register;
using BarberBoss.Application.Usecase.Billing.Update;
using BarberBoss.Comunication.Request;
using BarberBoss.Comunication.Response;
using Microsoft.AspNetCore.Mvc;

namespace BarberBoss.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BillingController : ControllerBase
{
    [HttpPost] //registrar faturamento
    [ProducesResponseType(typeof(ResponseRegisterBillingJson), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Register(
        [FromBody] RequestBillingJson request,
        [FromServices] IRegisterBillingUseCase useCase)
    {
        var response = await useCase.Execute(request);
        return Created(string.Empty, response);
    }

    [HttpGet] //listar todos os faturamentos
    [ProducesResponseType(typeof(ResponseAllBillingsJson), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> GetAllBillings(
        [FromServices] IGetAllBillingUseCase useCase)
    {
        var response = await useCase.Execute();
        if (response.Billings.Count != 0)
            return Ok(response);

        return NoContent();
    }

    [HttpGet] //Listar faturamento por ID
    [Route("{id}")]
    [ProducesResponseType(typeof(ResponseBillingJson), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetById(
        [FromServices] IGetBillingByIdUseCase useCase,
        [FromRoute] long id)
    {
        var response = await useCase.Execute(id);
        return Ok(response);
    }

    [HttpDelete] //Deletar faturamento
    [Route("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(
        [FromServices] IDeleteBillingUseCase useCase,
        [FromRoute] long id)
    {
        await useCase.Execute(id);
        return NoContent();
    }

    [HttpPut] //atualizar faturamento
    [Route("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(
        [FromServices] IUpdateBillingUseCase useCase,
        [FromRoute] long id,
        [FromBody] RequestUpdateBillingJson request)
    {
        await useCase.Execute(id, request);
        return NoContent();
    }
}

