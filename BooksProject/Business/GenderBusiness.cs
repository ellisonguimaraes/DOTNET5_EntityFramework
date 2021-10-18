using System.Linq;
using BooksProject.Business.Interface;
using BooksProject.Models;
using BooksProject.Models.InputModel;
using BooksProject.Models.Pagination;
using BooksProject.Models.ViewModel;
using BooksProject.Repositories.Interface;

namespace BooksProject.Business
{
    public class GenderBusiness : IBusiness<GenderViewModel, GenderInputModel>
    {
        private readonly IRepository<Gender> _repository;

        public GenderBusiness(IRepository<Gender> repository)
        {
            _repository = repository;
        }

        public PagedList<GenderViewModel> GetPaginate(PaginationParameters paginationParameters)
        {
            var genders = _repository.Get(paginationParameters);

            var gendersViewModel = new PagedList<GenderViewModel>(
                genders.Select(g => new GenderViewModel{
                    Id = g.Id,
                    Name = g.Name,
                    Books = g.Books.Select(b => new BookViewModel {
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
                        Authors = b.AuthorBooks.Select(ab => new AuthorViewModel {
                            Id = ab.Author.Id,
                            Name = ab.Author.Name,
                            LastName = ab.Author.LastName,
                            BirthDate = ab.Author.BirthDate
                        }).ToList()
                    }).ToList()
                }),
                genders.CurrentPage,
                genders.TotalPages,
                genders.PageSize,
                genders.TotalCount
            );

            return gendersViewModel;
        }

        public GenderViewModel GetById(int id)
        {
            var gender = _repository.GetById(id);

            GenderViewModel genderViewModel = new GenderViewModel{
                Id = gender.Id,
                Name = gender.Name,
                Books = gender.Books.Select(b => new BookViewModel {
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
                    Authors = b.AuthorBooks.Select(ab => new AuthorViewModel {
                        Id = ab.Author.Id,
                        Name = ab.Author.Name,
                        LastName = ab.Author.LastName,
                        BirthDate = ab.Author.BirthDate
                    }).ToList()
                }).ToList()
            };

            return genderViewModel;
        }

        public GenderViewModel Create(GenderInputModel item)
        {
            Gender gender = _repository.Create(new Gender{
                Name = item.Name
            });

            return new GenderViewModel{
                Id = gender.Id,
                Name = gender.Name
            };
        }

        public GenderViewModel Update(GenderInputModel item)
        {
            Gender gender = _repository.Update(new Gender{
                Id = item.Id,
                Name = item.Name
            });

            return new GenderViewModel{
                Id = gender.Id,
                Name = gender.Name
            };
        }

        public bool Delete(int id)
        {
            return _repository.Delete(id);
        }
    }
}