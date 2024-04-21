using Base.Contracts.Event;
using Base.UseCases.Abstractions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Base.Service.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository userRepository;
        private readonly IAuthUserAccessor authUserAccessor;

        public UserController(IUserRepository userRepository, IAuthUserAccessor authUserAccessor)
        {
            this.userRepository = userRepository;
            this.authUserAccessor = authUserAccessor;
        }

        /// <summary>
        /// Получить список избранных категорий
        /// </summary>
        /// <returns></returns>
        [HttpGet("categories")]
        [ProducesResponseType(typeof(List<string>), 200)]
        public async Task<IActionResult> GetFavoritedCategories()
        {
            var categories = await userRepository.GetFavoritedCategories(authUserAccessor.GetUserId());
            var result = categories.Select(x => x.Name).ToList();
            return Ok(result);
        }

        /// <summary>
        /// Получить список будующих ивентом юзера
        /// </summary>
        /// <returns></returns>
        [HttpGet("events")]
        [ProducesResponseType(typeof(List<ReducedEventDto>), 200)]
        public async Task<IActionResult> GetFavoritedEvents()
        {
            var events = await userRepository.GetFavoritedEvents(authUserAccessor.GetUserId());
            var result = events.Select(x => new ReducedEventDto() 
            {
                Id = x.Id, 
                Name = x.Name, 
                City = x.City, 
                StartAt = x.StartAt
            })
                .ToList();
            return Ok(result);
        }

        /// <summary>
        /// Фильтр пользователей
        /// </summary>
        /// <param name="byCrossedEvents"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(List<UserCrossDto>), 200)]
        public async Task<IActionResult> Filter(bool byCrossedEvents = false)
        {
            var result = await userRepository.Filter(authUserAccessor.GetUserId(), byCrossedEvents);
            return Ok(result);
        }
    }
}
