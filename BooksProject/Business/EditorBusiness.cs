using System.Linq;
using BooksProject.Business.Interface;
using BooksProject.Models;
using BooksProject.Models.Pagination;
using BooksProject.Models.ViewModel;
using BooksProject.Models.InputModel;
using BooksProject.Repositories.Interface;

namespace BooksProject.Business
{
    public class EditorBusiness : IBusiness<EditorViewModel, EditorInputModel>
    {
        private readonly IRepository<Editor> _repository;

        public EditorBusiness(IRepository<Editor> repository)
        {
            _repository = repository;
        }
        
        public PagedList<EditorViewModel> GetPaginate(PaginationParameters paginationParameters)
        {
            PagedList<Editor> editors = _repository.Get(paginationParameters);

            PagedList<EditorViewModel> editorsViewModel = new PagedList<EditorViewModel>(
                editors.Select(e => new EditorViewModel{
                    Id = e.Id,
                    Name = e.Name,
                    Books = e.Books.Select(b => new BookViewModel {
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
                editors.CurrentPage,
                editors.TotalPages,
                editors.PageSize,
                editors.TotalCount
            );

            return editorsViewModel;
        }

        public EditorViewModel GetById(int id)
        {
            Editor editor = _repository.GetById(id);

            EditorViewModel editorViewModel = new EditorViewModel {
                Id = editor.Id,
                Name = editor.Name,
                Books = editor.Books.Select(b => new BookViewModel {
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

            return editorViewModel;
        }

        public EditorViewModel Create(EditorInputModel item)
        {
            Editor editor = _repository.Create(new Editor {
                Name = item.Name
            });

            return new EditorViewModel {
                Id = editor.Id,
                Name = editor.Name
            };
        }

        public EditorViewModel Update(EditorInputModel item)
        {
            Editor editor = _repository.Update(new Editor {
                Id = item.Id,
                Name = item.Name
            });

            return new EditorViewModel {
                Id = editor.Id,
                Name = editor.Name
            };
        }

        public bool Delete(int id)
        {
            return _repository.Delete(id);
        }
    }
}