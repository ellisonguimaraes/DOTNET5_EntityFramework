using BooksProject.Models.InputModel;
using BooksProject.Models.Pagination;
using BooksProject.Models.ViewModel;

namespace BooksProject.Business.Interface
{
    public interface IBusiness<ViewModel, InputModel> 
        where ViewModel : BaseClassViewModel 
        where InputModel : BaseClassInputModel
    {
        PagedList<ViewModel> GetPaginate(PaginationParameters paginationParameters);
        ViewModel GetById(int id);
        ViewModel Create(InputModel item);
        ViewModel Update(InputModel item);
        bool Delete(int id);
    }
}