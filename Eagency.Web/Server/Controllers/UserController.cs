using Eagency.BLL.Services.Interfaces;
using Eagency.Web.Shared.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Eagency.Web.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService userService;

        public UserController(IUserService userService)
        {
            this.userService = userService;
        }

        [SwaggerOperation(
               Summary = "Az összes felhasználó lekérdezése.",
               Description = "Az összes felhasználó lekérdezése, ami visszaad egy listát oldalazás nélkül."
        )]
        [HttpGet]
        [SwaggerResponse(StatusCodes.Status200OK, "Sikeresen lekérdezve.")]
        public async Task<ActionResult<List<UserDto>>> GetAll()
        {
            var users = await userService.GetAllAsync();
            return Ok(users);
        }

        [SwaggerOperation(
               Summary = "Egy felhasználó lekérdezése id alapján.",
               Description = "Ha nem található az adatbázisban a kapott id-val rendelkező elem, akkor hibát dob, egyébként visszaadja a megfelelőt."
        )]
        [Route("{id}")]
        [HttpGet]
        [SwaggerResponse(StatusCodes.Status200OK, "A felhasználó sikeresen lekérdezve.")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Nincs ilyen elem az adatbázisban.")]
        public async Task<ActionResult<UserDto>> GetById(string id)
        {
            var user = await userService.GetByIdAsync(id);
            return Ok(user);
        }

        [SwaggerOperation(
               Summary = "Vásárló létrehozása.",
               Description = "A kapott entitás paramétereit ellenőrzi és ha nem felel meg a követelményeknek akkor hibát dob, egyébként létrehozza az új entitást."
        )]
        [Route("customer")]
        [HttpPost]
        [SwaggerResponse(StatusCodes.Status200OK, "A felhasználó sikeresen létrehozva.")]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "A felhasználó paraméterei között volt olyan, ami nem felel meg a követelményeknek.")]
        public async Task<ActionResult<UserDto>> CreateCustomer([FromBody] UserDto userDto)
        {
            var user = await userService.CreateCustomerAsync(userDto);
            return Ok(user);
        }

        [SwaggerOperation(
               Summary = "Admin létrehozása.",
               Description = "A kapott entitás paramétereit ellenőrzi és ha nem felel meg a követelményeknek akkor hibát dob, egyébként létrehozza az új entitást."
        )]
        [Route("admin")]
        [HttpPost]
        [SwaggerResponse(StatusCodes.Status200OK, "A felhasználó sikeresen létrehozva.")]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "A felhasználó paraméterei között volt olyan, ami nem felel meg a követelményeknek.")]
        public async Task<ActionResult<UserDto>> CreateAdmin([FromBody] UserDto userDto)
        {
            var user = await userService.CreateAdminAsync(userDto);
            return Ok(user);
        }

        [SwaggerOperation(
               Summary = "Felhasználó törlése.",
               Description = "Törlés előtt ellenőrzi, hogy a kapott id-val rendelkezik-e entitás. Ha igen, akkor elvégzi a törlést, egyébként hibát dob."
        )]
        [Route("{id}")]
        [HttpDelete]
        [SwaggerResponse(StatusCodes.Status200OK, "A felhasználó sikeresen törölve.")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Nincs ilyen elem az adatbázisban.")]
        public async Task<ActionResult> Delete(string id)
        {
            await userService.DeleteAsync(id);
            return Ok();
        }

        [SwaggerOperation(
               Summary = "Felhasználó email módosítása.",
               Description = "Módosítás előtt ellenőrzi, hogy a kapott id-val rendelkezik-e entitás. Ha igen, akkor elvégzi a módosítást, egyébként hibát dob."
        )]
        [Route("{id}/email")]
        [HttpPatch]
        [SwaggerResponse(StatusCodes.Status200OK, "A felhasználó sikeresen módosítva.")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Nincs ilyen elem az adatbázisban.")]
        public async Task<ActionResult<UserDto>> ModifyEmail(string id, [FromQuery] string email)
        {
            var user = await userService.ModifyEmailAsync(id, email);
            return Ok(user);
        }

        [SwaggerOperation(
              Summary = "Felhasználó username módosítása.",
              Description = "Módosítás előtt ellenőrzi, hogy a kapott id-val rendelkezik-e entitás. Ha igen, akkor elvégzi a módosítást, egyébként hibát dob."
        )]
        [Route("{id}/username")]
        [HttpPatch]
        [SwaggerResponse(StatusCodes.Status200OK, "A felhasználó sikeresen módosítva.")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Nincs ilyen elem az adatbázisban.")]
        public async Task<ActionResult<UserDto>> ModifyUserName(string id, [FromQuery] string username)
        {
            var user = await userService.ModifyUserNameAsync(id, username);
            return Ok(user);
        }

        [SwaggerOperation(
              Summary = "Felhasználó teljes nevének módosítása.",
              Description = "Módosítás előtt ellenőrzi, hogy a kapott id-val rendelkezik-e entitás. Ha igen, akkor elvégzi a módosítást, egyébként hibát dob."
        )]
        [Route("{id}/name")]
        [HttpPatch]
        [SwaggerResponse(StatusCodes.Status200OK, "A felhasználó sikeresen módosítva.")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Nincs ilyen elem az adatbázisban.")]
        public async Task<ActionResult<UserDto>> ModifyName(string id, [FromQuery] string lastname, [FromQuery] string firstname)
        {
            var user = await userService.ModifyNameAsync(id, firstname, lastname);
            return Ok(user);
        }

        [SwaggerOperation(
              Summary = "Felhasználó jelszavának módosítása.",
              Description = "Módosítás előtt ellenőrzi, hogy a kapott id-val rendelkezik-e entitás. Ha igen, akkor megnézi, hogy a két jelszóparaméter üres vagy null. Amennyiben nem üres, akkor módosít, egyébként hibát dob."
        )]
        [Route("{id}/password")]
        [HttpPatch]
        [SwaggerResponse(StatusCodes.Status200OK, "A felhasználó sikeresen módosítva.")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Nincs ilyen elem az adatbázisban.")]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "A régi jelszó vagy az új jelszó nem érvényes.")]
        public async Task<ActionResult<UserDto>> ModifyPassword(string id, [FromBody] PasswordChangeDto password)
        {
            var user = await userService.ModifyPasswordAsync(id, password.CurrentPassword, password.NewPassword);
            return Ok(user);
        }
    }
}
