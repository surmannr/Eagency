using Eagency.BLL.Services.Interfaces;
using Eagency.Web.Shared.Dto;
using Eagency.Web.Shared.Extensions;
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
    public class CommentController : ControllerBase
    {
        private readonly ICommentService commentService;

        public CommentController(ICommentService commentService)
        {
            this.commentService = commentService;
        }

        [SwaggerOperation(
                Summary = "Kommentek lekérdezése ingatlan id alapján.",
                Description = "Összes komment lekérése oldalak szerint a Property id-ja alapján."
        )]
        [Route("property/{id}")]
        [HttpGet]
        [SwaggerResponse(StatusCodes.Status200OK, "Kommentek sikeresen lekérdezve oldalak szerint.")]
        public async Task<ActionResult<PagedResult<CommentDto>>> GetAllByPropertyId(int id, [FromQuery] int pagesize, [FromQuery] int pagenumber)
        {
            var comments = await commentService.GetAllByPropertyIdPagedAsnyc(id, pagesize, pagenumber);
            return Ok(comments);
        }

        [SwaggerOperation(
                Summary = "Komment létrehozása.",
                Description = "Komment létrehozása. Létrehozás előtt validáláson megy keresztül és ha valamelyik paraméter nem felel meg hibát dob."
        )]
        [HttpPost]
        [SwaggerResponse(StatusCodes.Status200OK,"A komment sikeresen létrehozva.")]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Nem sikerült létrehozni, valamelyik paraméter hibás lehet.")]
        public async Task<ActionResult<CommentDto>> Create([FromBody] CommentDto commentDto)
        {
            var comment = await commentService.CreateAsync(commentDto);
            return Ok(comment);
        }

        [SwaggerOperation(
                Summary = "Komment törlése.",
                Description = "Komment törlése. Törlés előtt megnézi, hogy létezik-e a kapott id alapján elem az adatbázisban."
        )]
        [Route("{id}")]
        [HttpDelete]
        [SwaggerResponse(StatusCodes.Status200OK, "Sikeres törlés.")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "A törölni kívánt elem nem található.")]
        public async Task<ActionResult> Delete(int id)
        {
            await commentService.DeleteAsync(id);
            return Ok();
        }

        [SwaggerOperation(
                Summary = "Komment válaszának megváltoztatása.",
                Description = "Komment válaszának megváltoztatása. Ellenőrzi, hogy a kapott string null vagy üres."
        )]
        [Route("{id}/add-answer")]
        [HttpPatch]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<CommentDto>> AddAnswer(int id, [FromQuery] string answer)
        {
            var comment = await commentService.AddAnswerAsync(id, answer);
            return Ok(answer);
        }
    }
}
