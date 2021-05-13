using Eagency.BLL.Services.Interfaces;
using Eagency.Web.Shared.Dto;
using Eagency.Web.Shared.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Eagency.Web.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContractController : ControllerBase
    {
        private readonly IContractService contractService;

        public ContractController(IContractService contractService)
        {
            this.contractService = contractService;
        }

        [Authorize(Policy = "AgentPolicy")]
        [SwaggerOperation(
                Summary = "Az összes szerződés lekérdezése.",
                Description = "Az összes szerződés lekérése az adatbázisból."
        )]
        [HttpGet]
        [SwaggerResponse(StatusCodes.Status200OK, "Az összes szerződés sikeresen lekérdezve.")]
        public async Task<ActionResult<IEnumerable<ContractDto>>> GetAll()
        {
            var contracts = await contractService.GetAllAsync();
            return Ok(contracts);
        }

        [Authorize]
        [SwaggerOperation(
                Summary = "Az összes szerződés lekérdezése oldalazva felhasználó id alapján.",
                Description = "A lekérdezés előtt ellenőrzi, hogy az oldalak értékei érvényesek és a felhasználó id-val létezik-e felhasználó." +
            "Ha minden rendben, akkor lekérdezi az összes adatot oldalazva felhasználó id alapján."
        )]
        [Route("userid/{id}")]
        [HttpGet]
        [SwaggerResponse(StatusCodes.Status200OK, "A szerződések sikeresen lekérdezve.")]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Az oldalszám vagy oldalméret értéke nem érvényes.")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Nem található az adatbázisban ilyen felhasználó.")]
        public async Task<ActionResult<PagedResult<ContractDto>>> GetAllPagedByUserId(string id, [FromQuery] int pagesize, [FromQuery] int pagenumber)
        {
            var contracts = await contractService.GetAllByUserIdPagedAsync(id, pagesize, pagenumber);
            return Ok(contracts);
        }

        [Authorize(Policy = "AgentPolicy")]
        [SwaggerOperation(
                Summary = "Szerződés létrehozása.",
                Description = "Létrehozás előtt a kapott modell paramétereit ellenőrzi, majd ha minden rendben, akkor létrehozza."
        )]
        [HttpPost]
        [SwaggerResponse(StatusCodes.Status200OK, "A szerződés sikeresen létrehozva.")]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "A paraméterek között van olyan, ami nem felel meg a követelményeknek.")]
        public async Task<ActionResult<ContractDto>> Create([FromBody] ContractDto contractDto)
        {
            var contract = await contractService.CreateAsync(contractDto);
            return Ok(contract);
        }

        [Authorize(Policy = "AgentPolicy")]
        [SwaggerOperation(
                Summary = "Szerződés törlése.",
                Description = "Törlés előtt ellenőrzi, hogy létezik-e a kapott id-val elem az adatbázisban. Ha igen, akkor kitörli, egyébként hibát dob."
        )]
        [Route("{id}")]
        [HttpDelete]
        [SwaggerResponse(StatusCodes.Status200OK, "A szerződés sikeresen törölve.")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Nincs ilyen elem az adatbázisban.")]
        public async Task<ActionResult> Delete(int id)
        {
            await contractService.DeleteAsync(id);
            return Ok();
        }

        [Authorize]
        [SwaggerOperation(
                Summary = "Szerződés kifizetett státusz megváltoztatása.",
                Description = "A módosítás előtt ellenőrzi, hogy létezik-e a kapott id-val elem az adatbázisban. Ha igen, akkor megváltoztatja az ispaid értékét."
        )]
        [Route("{id}/paid")]
        [HttpPatch]
        [SwaggerResponse(StatusCodes.Status200OK, "A szerződés sikeresen megváltoztatva.")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Nincs ilyen elem az adatbázisban.")]
        public async Task<ActionResult<ContractDto>> ModifyPaid(int id, [FromQuery] bool ispaid)
        {
            var contract = await contractService.ModifyPaidAsync(id, ispaid);
            return Ok(contract);
        }

        [Authorize]
        [SwaggerOperation(
                Summary = "Szerződés aláírt státusz megváltoztatása.",
                Description = "A módosítás előtt ellenőrzi, hogy létezik-e a kapott id-val elem az adatbázisban. Ha igen, akkor megváltoztatja az issigned értékét."
        )]
        [Route("{id}/sign")]
        [HttpPatch]
        [SwaggerResponse(StatusCodes.Status200OK, "A szerződés sikeresen megváltoztatva.")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Nincs ilyen elem az adatbázisban.")]
        public async Task<ActionResult<ContractDto>> ModifySigned(int id)
        {
            var contract = await contractService.ModifySignedAsync(id);
            return Ok(contract);
        }
    }
}
