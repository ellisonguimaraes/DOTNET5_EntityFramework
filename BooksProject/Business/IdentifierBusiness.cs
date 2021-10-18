using System.Linq;
using BooksProject.Business.Interface;
using BooksProject.Models;
using BooksProject.Models.InputModel;
using BooksProject.Models.Pagination;
using BooksProject.Models.ViewModel;
using BooksProject.Repositories.Interface;

namespace BooksProject.Business
{
    public class IdentifierBusiness : IBusiness<IdentifierViewModel, IdentifierInputModel>
    {
        private readonly IRepository<Identifier> _repository;

        public IdentifierBusiness(IRepository<Identifier> repository)
        {
            _repository = repository;
        }

        public PagedList<IdentifierViewModel> GetPaginate(PaginationParameters paginationParameters)
        {
            var identifiers = _repository.Get(paginationParameters);

            var identifiersViewModel = new PagedList<IdentifierViewModel>(
                identifiers.Select(i => new IdentifierViewModel{
                    Id = i.Id,
                    IdentifierNumber = i.IdentifierNumber,
                    IdentifierType = i.IdentifierType,
                    Book = (i.Book == null)? null : new BookViewModel{
                                                        Id = i.Book.Id,
                                                        Name = i.Book.Name,
                                                        Price = i.Book.Price,
                                                        PubDate = i.Book.PubDate,
                                                        Editor = new EditorViewModel{
                                                            Id = i.Book.Editor.Id,
                                                            Name = i.Book.Editor.Name
                                                        },
                                                        Gender = new GenderViewModel{
                                                            Id = i.Book.Gender.Id,
                                                            Name = i.Book.Gender.Name
                                                        },
                                                        Identifier = new IdentifierViewModel{
                                                            Id = i.Book.Identifier.Id,
                                                            IdentifierNumber = i.Book.Identifier.IdentifierNumber,
                                                            IdentifierType = i.Book.Identifier.IdentifierType
                                                        },
                                                        Authors = i.Book.AuthorBooks.Select(ab => new AuthorViewModel {
                                                            Id = ab.Author.Id,
                                                            Name = ab.Author.Name,
                                                            LastName = ab.Author.LastName,
                                                            BirthDate = ab.Author.BirthDate
                                                        }).ToList()
                    },
                }),
                identifiers.CurrentPage,
                identifiers.TotalPages,
                identifiers.PageSize,
                identifiers.TotalCount
            );

            return identifiersViewModel;
        }

        public IdentifierViewModel GetById(int id)
        {
            var identifier = _repository.GetById(id);

            IdentifierViewModel identifierViewModel = new IdentifierViewModel{
                Id = identifier.Id,
                IdentifierNumber = identifier.IdentifierNumber,
                IdentifierType = identifier.IdentifierType,
                Book = (identifier.Book == null)? null : new BookViewModel{
                                                        Id = identifier.Book.Id,
                                                        Name = identifier.Book.Name,
                                                        Price = identifier.Book.Price,
                                                        PubDate = identifier.Book.PubDate,
                                                        Editor = new EditorViewModel{
                                                            Id = identifier.Book.Editor.Id,
                                                            Name = identifier.Book.Editor.Name
                                                        },
                                                        Gender = new GenderViewModel{
                                                            Id = identifier.Book.Gender.Id,
                                                            Name = identifier.Book.Gender.Name
                                                        },
                                                        Identifier = new IdentifierViewModel{
                                                            Id = identifier.Book.Identifier.Id,
                                                            IdentifierNumber = identifier.Book.Identifier.IdentifierNumber,
                                                            IdentifierType = identifier.Book.Identifier.IdentifierType
                                                        },
                                                        Authors = identifier.Book.AuthorBooks.Select(ab => new AuthorViewModel {
                                                            Id = ab.Author.Id,
                                                            Name = ab.Author.Name,
                                                            LastName = ab.Author.LastName,
                                                            BirthDate = ab.Author.BirthDate
                                                        }).ToList()
                },
            };

            return identifierViewModel;
        }

        public IdentifierViewModel Create(IdentifierInputModel item)
        {
            Identifier identifier = _repository.Create(new Identifier{
                IdentifierNumber = item.IdentifierNumber,
                IdentifierType = item.IdentifierType
            });

            return new IdentifierViewModel{
                Id = identifier.Id,
                IdentifierNumber = item.IdentifierNumber,
                IdentifierType = item.IdentifierType
            };
        }

        public IdentifierViewModel Update(IdentifierInputModel item)
        {
            Identifier identifier = _repository.Update(new Identifier{
                Id = item.Id,
                IdentifierNumber = item.IdentifierNumber,
                IdentifierType = item.IdentifierType
            });

            return new IdentifierViewModel{
                Id = identifier.Id,
                IdentifierNumber = item.IdentifierNumber,
                IdentifierType = item.IdentifierType
            };
        }

        public bool Delete(int id)
        {
            return _repository.Delete(id);
        }
    }
}