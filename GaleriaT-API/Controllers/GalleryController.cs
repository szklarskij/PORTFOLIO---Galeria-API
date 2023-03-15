using System.Collections;
using AutoMapper;
using GaleriaT_API.Entities;
using GaleriaT_API.Models;
using GaleriaT_API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GaleriaT_API.Controllers
{
    [Route("api/gallery")]
    [ApiController]
    [Authorize]
    public class GalleryController : ControllerBase
    {
        private readonly IGalleryService _galleryService;

        public GalleryController(IGalleryService galleryService)
        {
            _galleryService = galleryService;
        }
        [HttpPut("{id}")]
        public ActionResult Update([FromForm] UpdateGalleryPostDto dto, [FromRoute] int id, [FromForm] IFormFile file, IFormFile file2, IFormFile file3)
        {


            _galleryService.Update(id, dto, file, file2, file3); 
          
            return Ok();    
        }

        [HttpPut("{id}/{id2}")]
   

        public ActionResult UpdateOrder([FromRoute] int id, [FromRoute] int id2)
        {


            _galleryService.UpdateOrder(id, id2);

            return Ok();
        }


        [HttpDelete("{id}")]
        public ActionResult Delete([FromRoute] int id)
        {
            _galleryService.Delete(id);

            return NoContent();

        }
        [HttpDelete("deleteall")]
        [AllowAnonymous]

        public ActionResult DeleteAll()
        {
            _galleryService.DeleteAll();

            return NoContent();

        }

        [HttpPost]
        public ActionResult CreateGalleryPost([FromForm] CreateGalleryPostDto dto, [FromForm] IFormFile file, IFormFile file2, IFormFile file3)
        {


            var id = _galleryService.Create(dto, file, file2, file3);

            return Created($"{id}", null);
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult<IEnumerable<GalleryPostDto>> GetAll([FromQuery]GalleryQuery query)
        {
            var galleryDto = _galleryService.GetAll(query);

            return Ok(galleryDto);
        }

        [HttpGet("thumbnails")]
        [AllowAnonymous]
        public ActionResult<IEnumerable<GalleryPostDto>> GetAllThumbnails([FromQuery] GalleryQuery query)
        {
            var galleryDto = _galleryService.GetAllThumbnails(query);

            return Ok(galleryDto);
        }


        [HttpGet("{id}")]
        [AllowAnonymous]
        //[ResponseCache(Duration = 1200, VaryByQueryKeys = new[] {"getById"})]

        public ActionResult<GalleryPost> Get([FromRoute]int id)
        {
             var post = _galleryService.GetById(id);

            return Ok(post);
        }

    }
}
