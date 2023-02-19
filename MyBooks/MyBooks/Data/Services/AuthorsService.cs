﻿using MyBooks.Data.Models;
using MyBooks.Data.ViewModels;
using System.Linq;

namespace MyBooks.Data.Services
{
	public class AuthorsService
	{
		private readonly AppDbContext _context;

		public AuthorsService(AppDbContext context)
		{
			_context = context;
		}

		public void AddAuthor(AuthorVM book)
		{
			var _author = new Author()
			{
				FullName = book.FullName
			};
			_context.Authors.Add(_author);
			_context.SaveChanges();

		}

		public AuthorWithBooksVM GetAuthorWithBooks(int authorId)
		{
			var _author = _context.Authors.Where(n => n.Id == authorId).Select(n => new AuthorWithBooksVM()
			{
				FullName = n.FullName,
				BookTitle = n.Book_Authors.Select(n => n.Book.Title).ToList(),
			}).FirstOrDefault();
			return _author;
		}
	}
}
