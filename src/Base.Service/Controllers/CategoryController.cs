using Base.Core.Domain;
using Base.UseCases.Abstractions;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Base.Service.Controllers;

[Route("api/category")]
[ApiController]
public class CategoryController : ControllerBase
{
    private readonly ICategoryRepository categoryRepository;
    private readonly IAuthUserAccessor authUserAccessor;

    public CategoryController(ICategoryRepository categoryRepository, IAuthUserAccessor authUserAccessor)
    {
        this.categoryRepository = categoryRepository;
        this.authUserAccessor = authUserAccessor;
    }

    /// <summary>
    /// Получить список всех категорий
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [ProducesResponseType(typeof(List<string>), 200)]
    public async Task<IActionResult> GetCategories()
    {
        var categories = await categoryRepository.GetAll();
        if (categories == null)
        {
            return NotFound();
        }

        List<string> result = new();
        foreach (var item in categories)
        {
            result.Add(item.Name);
        }

        return Ok(result);
    }
    
    /// <summary>
    /// Создание новой категории
    /// </summary>
    /// <param name="category"></param>
    /// <returns></returns>
    [HttpPost]
    [ProducesResponseType(typeof(List<string>), 200)]
    public async Task<IActionResult> Create(Category category)
    {
        var id = await categoryRepository.Create(category);
        if (id == null)
            return BadRequest();

        return Ok(id);
    }

    /// <summary>
    /// Пометить категорию любимой
    /// </summary>
    /// <param name="categoryName"></param>
    /// <returns></returns>
    [HttpPatch("{categoryName}")]
    public async Task<IActionResult> FavoriteCategory(string categoryName)
    {
        await categoryRepository.Favorite(categoryName, authUserAccessor.GetUserId());

        return Ok();
    }

}
