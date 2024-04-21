using Base.Contracts.Event;
using Base.Contracts.Merch;
using Base.Core.Domain;
using Base.UseCases.Abstractions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Base.Service.Controllers;

[Route("api/merch")]
[ApiController]
public class MerchController : ControllerBase
{
    private readonly IMerchRepository merchRepository;
    private readonly IAuthUserAccessor authUserAccessor;

    public MerchController(IMerchRepository merchRepository, IAuthUserAccessor authUserAccessor)
    {
        this.merchRepository = merchRepository;
        this.authUserAccessor = authUserAccessor;
    }

    /// <summary>
    /// Создать новый мерч
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost]
    [ProducesResponseType(typeof(Guid), 200)]
    public async Task<IActionResult> Create(CreateMerchRequest request)
    {
        var id = await merchRepository.Create(new Merch()
        {
            Name = request.Name,
            Description = request.Description,
            Image = request.Image,
            Stock = request.Stock,
            Price = request.Price
        });
        return Ok(id);
    }


    /// <summary>
    /// Купить мерч
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPatch]
    [ProducesResponseType(typeof(void), 200)]
    public async Task<IActionResult> Purchase(PurchaseRequest request)
    {
        await merchRepository.Purchase(authUserAccessor.GetUserId(), request.MerchId, request.Amount);
        return Ok();
    }

    /// <summary>
    /// Получить список всего мерча
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [ProducesResponseType(typeof(List<Merch>), 200)]
    public async Task<IActionResult> GetAll()
    {
        var merch = await merchRepository.GetAll();
        return Ok(merch);
    }
}
