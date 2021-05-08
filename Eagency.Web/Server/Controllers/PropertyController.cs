

using Azure.Identity;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Eagency.BLL.Services.Interfaces;
using Eagency.Web.Shared.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;

namespace Eagency.Web.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PropertyController : ControllerBase
    {
        private readonly IPropertyService propertyService;
        private readonly string _azureConnectionString;

        public PropertyController(IPropertyService propertyService)
        {
            this.propertyService = propertyService;

            ConfigurationBuilder builder = new ConfigurationBuilder();
            builder.AddAzureKeyVault(new Uri(@"https://festivall-keyvault.vault.azure.net/"), new DefaultAzureCredential());

            IConfiguration configuration = builder.Build();
            _azureConnectionString = configuration["AzureBlobStorageConnectionString"];
        }

        [SwaggerOperation(
                Summary = "Ingatlan lekérdezése id alapján.",
                Description = "Ha nem létezik a kapott id-val, akkor hibát dob, egyébként visszaadja a lekérdezett elemeket."
        )]
        [Route("{id}")]
        [HttpGet]
        [SwaggerResponse(StatusCodes.Status200OK, "Az ingatlan sikeresen lekérdezve.")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Nincs ilyen elem az adatbázisban.")]
        public async Task<ActionResult<PropertyDto>> GetById(int id)
        {
            var property = await propertyService.GetByIdAsync(id);
            return Ok(property);
        }

        [SwaggerOperation(
                Summary = "Az összes ingatlan lekérdezése.",
                Description = "Az összes ingatlan lekérdezése, ami visszaadja a listát."
        )]
        [HttpGet]
        [SwaggerResponse(StatusCodes.Status200OK, "Az ingatlan sikeresen lekérdezve.")]
        public async Task<ActionResult<IEnumerable<PropertyDto>>> GetAll()
        {
            var properties = await propertyService.GetAllAsync();
            return Ok(properties);
        }

        [SwaggerOperation(
                Summary = "Ingatlanok lekérdezése oldalazva.",
                Description = "Ha nem érvényes az oldalszám és az oldalméret akkor hibát dob, egyébként visszaadja az oldalazott listát."
        )]
        [Route("page")]
        [HttpGet]
        [SwaggerResponse(StatusCodes.Status200OK, "Az ingatlanok sikeresen lekérdezve.")]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Az oldalszám vagy az oldalméret értéke nem érvényes.")]
        public async Task<ActionResult<PagedResult<PropertyDto>>> GetAllPaged([FromQuery] int pagesize, [FromQuery] int pagenumber)
        {
            var properties = await propertyService.GetAllPagedAsync(pagesize, pagenumber);
            return Ok(properties);
        }

        [SwaggerOperation(
               Summary = "Elérhető ingatlanok lekérdezése oldalazva.",
               Description = "Ha nem érvényes az oldalszám és az oldalméret akkor hibát dob, egyébként visszaadja az oldalazott listát."
       )]
        [Route("available/page")]
        [HttpGet]
        [SwaggerResponse(StatusCodes.Status200OK, "Az ingatlanok sikeresen lekérdezve.")]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Az oldalszám vagy az oldalméret értéke nem érvényes.")]
        public async Task<ActionResult<PagedResult<PropertyDto>>> GetAllAvailablePaged([FromQuery] int pagesize, [FromQuery] int pagenumber)
        {
            var properties = await propertyService.GetAllAvailablePagedAsync(pagesize, pagenumber);
            return Ok(properties);
        }

        [SwaggerOperation(
                Summary = "Ingatlan létrehozása.",
                Description = "A létrehozás előtt ellenőrzi a paramétereket, hogy megfelelnek e a követelményeknek. Ha igen akkor létrehozza az új entitást."
        )]
        [HttpPost]
        [SwaggerResponse(StatusCodes.Status200OK, "Az ingatlan sikeresen létrehozva.")]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Az ingatlan paraméterei között volt olyan, ami nem felel meg a követelményeknek.")]
        public async Task<ActionResult<PropertyDto>> Create([FromBody] PropertyDto propertyDto)
        {
            var property = await propertyService.CreateAsync(propertyDto);
            return Ok(property);
        }

        [HttpPost("upload")]
        public async Task<IActionResult> Upload()
        {
            try
            {
                var formCollection = await Request.ReadFormAsync();
                var file = formCollection.Files.First();
                if (file.Length > 0)
                {
                    var container = new BlobContainerClient(_azureConnectionString, "eagency-images");
                    var createResponse = await container.CreateIfNotExistsAsync();
                    if (createResponse != null && createResponse.GetRawResponse().Status == 201)
                        await container.SetAccessPolicyAsync(Azure.Storage.Blobs.Models.PublicAccessType.Blob);
                    var blob = container.GetBlobClient(file.FileName);
                    await blob.DeleteIfExistsAsync(DeleteSnapshotsOption.IncludeSnapshots);
                    using (var fileStream = file.OpenReadStream())
                    {
                        await blob.UploadAsync(fileStream, new BlobHttpHeaders { ContentType = file.ContentType });
                    }
                    return Ok(blob.Uri.ToString());
                }
                return BadRequest();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }

        [SwaggerOperation(
                Summary = "Ingatlan törlése.",
                Description = "Törlés előtt ellenőrzi, hogy létezik-e a kapott id-val elem az adatbázisban. Ha igen, akkor kitörli, egyébként hibát dob."
        )]
        [Route("{id}")]
        [HttpDelete]
        [SwaggerResponse(StatusCodes.Status200OK, "Az ingatlan sikeresen törölve.")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Nincs ilyen elem az adatbázisban.")]
        public async Task<ActionResult> Delete(int id)
        {
            await propertyService.DeleteAsync(id);
            return Ok();
        }

        [SwaggerOperation(
               Summary = "Ingatlan módosítása.",
               Description = "Módosítás előtt ellenőrzi, hogy érvényesek-e a kapott entitás paraméterei. Ha érvényes, akkor ellenőrzi, hogy ilyen id-val rendelkezik-e elem az adatbázisban" +
            " Ha minden rendben van, akkor módosítja."
        )]
        [Route("{id}")]
        [HttpPut]
        [SwaggerResponse(StatusCodes.Status200OK, "Az ingatlan sikeresen módosítva.")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Nincs ilyen elem az adatbázisban.")]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Az ingatlan paraméterei között volt olyan, ami nem felel meg a követelményeknek.")]
        public async Task<ActionResult<PropertyDto>> Modify(int id, [FromBody] PropertyDto propertyDto)
        {
            var property = await propertyService.ModifyAsync(id, propertyDto);
            return Ok(property);
        }
    }
}
