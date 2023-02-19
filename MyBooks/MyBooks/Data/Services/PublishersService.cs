using MyBooks.Data.Models;
using MyBooks.Data.ViewModels;
using System.Linq;

namespace MyBooks.Data.Services
{
	public class PublishersService
	{
		private readonly AppDbContext _context;
		public PublishersService(AppDbContext context)
		{
			_context = context;
		}

		public void AddPublisher(PublisherVM publisher)
		{
			var _publisher = new Publisher()
			{
				Name = publisher.Name,
			};
			_context.Publishers.Add(_publisher);
			_context.SaveChanges();
		}

		public PublisherWithBooksAndAuthorsVM GetPublisherData(int publisherId)
		{
			var _publisherData = _context.Publishers.Where(n => n.Id == publisherId)
				.Select(n => new PublisherWithBooksAndAuthorsVM()
				{
					Name = n.Name,
					BookAuthors = n.Books.Select(n => new BookAuthorVM()
					{
						BookName = n.Title,
						BookAuthors = n.Book_Authors.Select(n => n.Author.FullName).ToList()
					}).ToList()
				}).FirstOrDefault();
			return _publisherData;
		}

		public void DeletePublisherById(int id)
		{
			var _publisher = _context.Publishers.FirstOrDefault(n => n.Id == id);
			if (_publisher != null)
			{
				_context.Publishers.Remove(_publisher);
				_context.SaveChanges();
			}
		}
	}
}
