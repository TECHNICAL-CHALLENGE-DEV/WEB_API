using Cirkula.API.Data;
using Cirkula.API.Data.Models;
using Cirkula.API.DTO.Request.Store;
using Cirkula.API.DTO.Response;
using Cirkula.API.UseCase;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Globalization;
using System.Net.Mime;

namespace Cirkula.API.Controllers
{
    [ApiController]
    [Route("stores")]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    public class StoreController : BaseController
    {
        private CirkulaContext _context;

        public StoreController(CirkulaContext context)
        {
            this._context = context;
        }

        private IQueryable<Store> PrepareQuery() => _context.Stores
        .AsQueryable();

        // GET: StoreController
        [HttpGet]
        [ProducesResponseType(typeof(DefaultResponse<List<StoreResponse>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(DefaultResponse<>), StatusCodes.Status500InternalServerError)]
        public IActionResult Contacts([FromQuery] GetStoreRequest request)
        {
            try
            {
                var query = PrepareQuery().AsQueryable();
                var dtos = StoreResponse.Builder.From(query).BuildAll().ToList();

                if (request.Latitude != null && request.Longitude != null) {
                    dtos = StoreUseCase.OrderByPosition(request.Latitude.Value, request.Longitude.Value, dtos);
                }

                return OkResult("", dtos);
            }
            catch (Exception e)
            {
                return BadRequestResult(e.Message);
            }
        }

        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(typeof(DefaultResponse<StoreResponse>), StatusCodes.Status200OK)]
        public IActionResult Get(int id)
        {
            try
            {
                var query = PrepareQuery().SingleOrDefault(x => x.Id == id);
                if (query is null)
                    return NotFoundResult("Tienda no encontrada.");
                var dto = StoreResponse.Builder.From(query).Build();
                return OkResult("", dto);
            }
            catch (Exception e)
            {
                return BadRequestResult(e.Message);
            }
        }


        [HttpPut]
        [Route("{id}")]
        [ProducesResponseType(typeof(DefaultResponse<StoreResponse>), StatusCodes.Status200OK)]
        public IActionResult Put(int id, [FromBody] PutStoreRequest request)
        {
            try
            {
                var transaction = default(IDbContextTransaction);
                var store = PrepareQuery().SingleOrDefault(x => x.Id == id);
                if (store is null)
                    return NotFoundResult("Tienda no encontrada.");

                transaction = _context.Database.BeginTransaction();

                store.Name = request.Name;
                store.BannerUrl = request.BannerUrl;
                _context.SaveChanges();
                transaction.Commit();

                var query = PrepareQuery().Single(x => x.Id == id);
                var dto = StoreResponse.Builder.From(query).Build();
                return OkResult("", dto);
            }
            catch (Exception e)
            {
                return BadRequestResult(e.Message);
            }
        }

        [HttpPost]
        [ProducesResponseType(typeof(DefaultResponse<StoreResponse>), StatusCodes.Status200OK)]
        public IActionResult Post([FromBody] PostStoreRequest request)
        {
            try
            {
                var transaction = default(IDbContextTransaction);

                transaction = _context.Database.BeginTransaction();

                var data = new Store
                {
                    Name = request.Name,
                    BannerUrl = request.BannerUrl,  
                    Latitude = request.Latitude,
                    Longitude = request.Longitude,
                    OpenTime = TimeSpan.Parse(request.OpenTime),
                    CloseTime = TimeSpan.Parse(request.CloseTime),
                };

                _context.Stores.Add(data);
                _context.SaveChanges();

                transaction.Commit();

                var query = PrepareQuery().Single(x => x.Id == data.Id);
                var dto = StoreResponse.Builder.From(query).Build();
                return OkResult("", dto);
            }
            catch (Exception e)
            {
                return BadRequestResult(e.Message);
            }
        }

    }
}
