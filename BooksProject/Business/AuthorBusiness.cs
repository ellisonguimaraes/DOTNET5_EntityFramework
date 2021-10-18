using System.Linq;
using BooksProject.Business.Interface;
using BooksProject.Models;
using BooksProject.Models.InputModel;
using BooksProject.Models.Pagination;
using BooksProject.Models.ViewModel;
using BooksProject.Repositories.Interface;

namespace BooksProject.Business
{
    public class AuthorBusiness : IBusiness<AuthorViewModel, AuthorInputModel>
    {
        private readonly IRepository<Author> _repository;

        public AuthorBusiness(IRepository<Author> repository)
        {
            _repository = repository;
        }
        
        public PagedList<AuthorViewModel> GetPaginate(PaginationParameters paginationParameters)
        {
            var authors = _repository.Get(paginationParameters);

            var authorsViewModel = new PagedList<AuthorViewModel>(
                authors.Select(a => new AuthorViewModel{
                    Id = a.Id,
                    Name = a.Name,
                    LastName = a.LastName,
                    BirthDate = a.BirthDate,
                    Books = a.AuthorBooks.Select(ab => new BookViewModel{
                        Id = ab.Book.Id,
                        Name = ab.Book.Name,
                        Price = ab.Book.Price,
                        PubDate = ab.Book.PubDate,
                        Editor = new EditorViewModel{
                            Id = ab.Book.Editor.Id,
                            Name = ab.Book.Editor.Name
                        },
                        Gender = new GenderViewModel{
                            Id = ab.Book.Gender.Id,
                            Name = ab.Book.Gender.Name
                        },
                        Identifier = new IdentifierViewModel{
                            Id = ab.Book.Identifier.Id,
                            IdentifierNumber = ab.Book.Identifier.IdentifierNumber,
                            IdentifierType = ab.Book.Identifier.IdentifierType
                        }
                    }).ToList()
                }),
                authors.CurrentPage,
                authors.TotalPages,
                authors.PageSize,
                authors.TotalCount
            );

            return authorsViewModel;
        }

        public AuthorViewModel GetById(int id)
        {
            var author = _repository.GetById(id);

            AuthorViewModel authorViewModel = new AuthorViewModel{
                Id = author.Id,
                Name = author.Name,
                LastName = author.LastName,
                BirthDate = author.BirthDate,
                Books = author.AuthorBooks.Select(ab => new BookViewModel{
                    Id = ab.Book.Id,
                    Name = ab.Book.Name,
                    Price = ab.Book.Price,
                    PubDate = ab.Book.PubDate,
                    Editor = new EditorViewModel{
                        Id = ab.Book.Editor.Id,
                        Name = ab.Book.Editor.Name
                    },
                    Gender = new GenderViewModel{
                        Id = ab.Book.Gender.Id,
                        Name = ab.Book.Gender.Name
                    },
                    Identifier = new IdentifierViewModel{
                        Id = ab.Book.Identifier.Id,
                        IdentifierNumber = ab.Book.Identifier.IdentifierNumber,
                        IdentifierType = ab.Book.Identifier.IdentifierType
                    }
                }).ToList()
            };

            return authorViewModel;
        }

        public AuthorViewModel Create(AuthorInputModel item)
        {
            Author author = _repository.Create(new Author{
                Name = item.Name,
                LastName = item.LastName,
                BirthDate = item.BirthDate
            });

            return new AuthorViewModel{
                Id = author.Id,
                Name = author.Name,
                LastName = author.LastName,
                BirthDate = author.BirthDate
            };
        }

        public AuthorViewModel Update(AuthorInputModel item)
        {
            Author author = _repository.Update(new Author{
                Id = item.Id,
                Name = item.Name,
                LastName = item.LastName,
                BirthDate = item.BirthDate
            });

            return new AuthorViewModel{
                Id = author.Id,
                Name = author.Name,
                LastName = author.LastName,
                BirthDate = author.BirthDate
            };
        }
        
        public bool Delete(int id)
        {
            return _repository.Delete(id);
        }
    }
}