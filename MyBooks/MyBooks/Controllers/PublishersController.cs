using Microsoft.AspNetCore.Mvc;
using MyBooks.Data.Services;
using MyBooks.Data.ViewModels;

namespace MyBooks.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class PublishersController : ControllerBase
	{
		private readonly PublishersService _publishersService;
		public PublishersController(PublishersService publishersService)
		{
			_publishersService = publishersService;
		}

		[HttpPost("add-publisher")]
		public IActionResult AddAuthor([FromBody] PublisherVM publisher)
		{
			_publishersService.AddPublisher(publisher);
			return Ok();
		}

		[HttpGet("get-publisher-books-with-authors/{id}")]
		public IActionResult GetPublisherData(int id)
		{
			var response = _publishersService.GetPublisherData(id);
			return Ok(response);
		}

		[HttpDelete("delete-publisher-id")]
		public IActionResult DeletePublisherById(int id)
		{
			_publishersService.DeletePublisherById(id);
			return Ok();
		}

	}
}
