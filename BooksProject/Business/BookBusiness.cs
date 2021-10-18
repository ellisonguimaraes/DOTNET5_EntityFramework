using System.Data.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using BooksProject.Business.Interface;
using BooksProject.Models;
using BooksProject.Models.InputModel;
using BooksProject.Models.Pagination;
using BooksProject.Models.ViewModel;
using BooksProject.Repositories.Interface;

namespace BooksProject.Business
{
    public class BookBusiness : IBusiness<BookViewModel, BookInputModel>
    {
        private readonly IRepository<Book> _bookRepository;
        private readonly IRepository<Editor> _editorRepository;
        private readonly IRepository<Gender> _genderRepository;
        private readonly IRepository<Identifier> _identifierRepository;
        private readonly IRepository<Author> _authorRepository;
        private readonly IAuthorBookRepository _authorBookRepository;

        public BookBusiness(
            IRepository<Book> bookRepository,
            IRepository<Editor> editorRepository,
            IRepository<Gender> genderRepository,
            IRepository<Identifier> identifierRepository,
            IRepository<Author> authorRepository,
            IAuthorBookRepository authorBookRepository)
        {
            _bookRepository = bookRepository;
            _editorRepository = editorRepository;
            _genderRepository = genderRepository;
            _identifierRepository = identifierRepository;
            _authorRepository = authorRepository;
            _authorBookRepository = authorBookRepository;
        }

        public PagedList<BookViewModel> GetPaginate(PaginationParameters paginationParameters)
        {
            var books = _bookRepository.Get(paginationParameters);

            var booksViewModel = new PagedList<BookViewModel>(
                books.Select(
                    b => new BookViewModel{
                        Id = b.Id,
                        Name = b.Name,
                        Price = b.Price,
                        PubDate = b.PubDate,
                        Editor = new EditorViewModel{
                            Id = b.Editor.Id,
                            Name = b.Editor.Name
                        },
                        Gender = new GenderViewModel{
                            Id = b.Gender.Id,
                            Name = b.Gender.Name
                        },
                        Identifier = new IdentifierViewModel{
                            Id = b.Identifier.Id,
                            IdentifierNumber = b.Identifier.IdentifierNumber,
                            IdentifierType = b.Identifier.IdentifierType
                        },  
                        Authors = b.AuthorBooks.Select(
                            ab => new AuthorViewModel{
                                Id = ab.Author.Id,
                                Name = ab.Author.Name,
                                LastName = ab.Author.LastName,
                                BirthDate = ab.Author.BirthDate
                            }
                        ).ToList()
                    }
                ).ToList(),
                books.CurrentPage,
                books.TotalPages,
                books.PageSize,
                books.TotalCount
            );

            return booksViewModel;
        }

        public BookViewModel GetById(int id)
        {
            var book = _bookRepository.GetById(id);

            return new BookViewModel{
                Id = book.Id,
                Name = book.Name,
                Price = book.Price,
                PubDate = book.PubDate,
                Editor = new EditorViewModel{
                    Id = book.Editor.Id,
                    Name = book.Editor.Name
                },
                Gender = new GenderViewModel{
                    Id = book.Gender.Id,
                    Name = book.Gender.Name
                },
                Identifier = new IdentifierViewModel{
                    Id = book.Identifier.Id,
                    IdentifierNumber = book.Identifier.IdentifierNumber,
                    IdentifierType = book.Identifier.IdentifierType
                },  
                Authors = book.AuthorBooks.Select(
                    ab => new AuthorViewModel{
                        Id = ab.Author.Id,
                        Name = ab.Author.Name,
                        LastName = ab.Author.LastName,
                        BirthDate = ab.Author.BirthDate
                    }
                ).ToList()
            };
        }

        public BookViewModel Create(BookInputModel item)
        {
            int editorId, genderId, identifierId;

            editorId = _editorRepository.GetById(item.Editor?.Id ?? 0)?.Id ?? -1;
            if (editorId < 0)
            {
                editorId = _editorRepository.Create(new Editor { Name = item.Editor.Name }).Id;
            }

            genderId = _genderRepository.GetById(item.Gender?.Id ?? 0)?.Id ?? -1;
            if (genderId < 0)
            {
                genderId = _genderRepository.Create(new Gender { Name = item.Gender.Name }).Id;
            }

            identifierId = _identifierRepository.Create(new Identifier { 
                IdentifierNumber = item.Identifier.IdentifierNumber,
                IdentifierType = item.Identifier.IdentifierType
            }).Id;

            Book book = _bookRepository.Create(new Book{
                Name = item.Name,
                Price = item.Price,
                PubDate = item.PubDate,
                EditorId = editorId,
                GenderId = genderId,
                IdentifierId = identifierId,
            });

            foreach (var authorInputModel in item.Authors)
            {
                Author author = _authorRepository.GetById(authorInputModel.Id) ?? 
                                _authorRepository.Create(new Author{
                                    Name = authorInputModel.Name,
                                    LastName = authorInputModel.LastName,
                                    BirthDate = authorInputModel.BirthDate
                                });
                
                _authorBookRepository.Create(new AuthorBook{
                    AuthorId = author.Id,
                    BookId = book.Id
                });
            }

            return new BookViewModel{
                Id = book.Id,
                Name = book.Name,
                Price = book.Price,
                PubDate = book.PubDate,
                Editor = new EditorViewModel{
                    Id = book.Editor.Id,
                    Name = book.Editor.Name
                },
                Gender = new GenderViewModel{
                    Id = book.Gender.Id,
                    Name = book.Gender.Name
                },
                Identifier = new IdentifierViewModel{
                    Id = book.Identifier.Id,
                    IdentifierNumber = book.Identifier.IdentifierNumber,
                    IdentifierType = book.Identifier.IdentifierType
                },  
                Authors = book.AuthorBooks?.Select(
                    ab => new AuthorViewModel{
                        Id = ab.Author.Id,
                        Name = ab.Author.Name,
                        LastName = ab.Author.LastName,
                        BirthDate = ab.Author.BirthDate
                    }
                ).ToList()
            };
        }

        public BookViewModel Update(BookInputModel item)
        {
            int editorId, genderId, identifierId;

            editorId = _editorRepository.GetById(item.Editor?.Id ?? 0)?.Id ?? -1;
            if (editorId < 0)
            {
                editorId = _editorRepository.Create(new Editor { Name = item.Editor.Name }).Id;
            }

            genderId = _genderRepository.GetById(item.Gender?.Id ?? 0)?.Id ?? -1;
            if (genderId < 0)
            {
                genderId = _genderRepository.Create(new Gender { Name = item.Gender.Name }).Id;
            }

            identifierId = _identifierRepository.Update(new Identifier { 
                Id = item.Identifier.Id,
                IdentifierNumber = item.Identifier.IdentifierNumber,
                IdentifierType = item.Identifier.IdentifierType
            }).Id;

            Book book = _bookRepository.GetById(_bookRepository.Update(new Book{
                Id = item.Id,
                Name = item.Name,
                Price = item.Price,
                PubDate = item.PubDate,
                EditorId = editorId,
                GenderId = genderId,
                IdentifierId = identifierId,
            }).Id); 

            System.Console.WriteLine("Book é nulo: " + book == null);
            System.Console.WriteLine("Id :" + book?.Id);
            System.Console.WriteLine("Name :" + book?.Name);
            System.Console.WriteLine("Price :" + book?.Price);
            System.Console.WriteLine("PubDate :" + book?.PubDate);

            System.Console.WriteLine("Tam AuthorBook: "+ book?.AuthorBooks.Count());

            foreach (var authorBook in book.AuthorBooks.ToList())
            {
                _authorBookRepository.Delete(authorBook);
            }

            foreach (var authorInputModel in item.Authors)
            {
                Author author = _authorRepository.GetById(authorInputModel.Id) ?? 
                                _authorRepository.Create(new Author{
                                    Name = authorInputModel.Name,
                                    LastName = authorInputModel.LastName,
                                    BirthDate = authorInputModel.BirthDate
                                });
                
                _authorBookRepository.Create(new AuthorBook{
                    AuthorId = author.Id,
                    BookId = book.Id
                });
            }

            return new BookViewModel{
                Id = book.Id,
                Name = book.Name,
                Price = book.Price,
                PubDate = book.PubDate,
                Editor = new EditorViewModel{
                    Id = book.Editor.Id,
                    Name = book.Editor.Name
                },
                Gender = new GenderViewModel{
                    Id = book.Gender.Id,
                    Name = book.Gender.Name
                },
                Identifier = new IdentifierViewModel{
                    Id = book.Identifier.Id,
                    IdentifierNumber = book.Identifier.IdentifierNumber,
                    IdentifierType = book.Identifier.IdentifierType
                },  
                Authors = book.AuthorBooks?.Select(
                    ab => new AuthorViewModel{
                        Id = ab.Author.Id,
                        Name = ab.Author.Name,
                        LastName = ab.Author.LastName,
                        BirthDate = ab.Author.BirthDate
                    }
                ).ToList()
            };
        }

        public bool Delete(int id)
        {
            Book book = _bookRepository.GetById(id);

            if (book == null) return false; 

            // Delete AuthorBooks
            foreach (var authorBook in book.AuthorBooks.ToList())
                _authorBookRepository.Delete(authorBook);

            // Delete Book
            var result = _bookRepository.Delete(id);

            // Delete Identifier
            _identifierRepository.Delete(book.Identifier.Id);

            return result;
        }
    }
}