using Microsoft.AspNetCore.Identity.UI.Services;

namespace GameFun.Services
{
    public interface IGameServices
    {
        IEnumerable<Game> GetAll();
        Game? GetById(int id);
        Task Create(CreateGameFormViewModel game);
       Task Edit(EditGameFormViewModel Model);

        bool Delete (int id);
    }
}
