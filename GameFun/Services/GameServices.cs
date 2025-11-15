using GameFun.Data;
using GameFun.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Threading.Tasks;

namespace GameFun.Services
{
    public class GameServices:IGameServices
    {
        private readonly ApplicationDbContext _context;

        public GameServices(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task Create(CreateGameFormViewModel model)
        {

            string CoverName = null;
            if (model.Cover != null) {
                var uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Covers");
               Directory.CreateDirectory(uploadPath);


                CoverName = Guid.NewGuid().ToString() + Path.GetExtension(model.Cover.FileName);
                var FilePath=Path.Combine(uploadPath, CoverName);
                using (var strem = new FileStream(FilePath, FileMode.Create)) { 
                await model.Cover.CopyToAsync(strem);    
                }
            
            }

            var newGame = new Game
            {
                Name = model.Name,
                Description = model.Description,
                CategoryId = model.CategoryId,
                Cover = CoverName,
                Devices=model.SelectedDevices
                .Select(did => new GameDevice { DeviceId = did })
                .ToList()
            };

            _context.Games.Add(newGame);
            _context.SaveChanges();

        }

        public IEnumerable<Game> GetAll()
        {
            return  _context.Games
                 .Include(g => g.Category)
      .Include(g => g.Devices)
    .ThenInclude(gd => gd.Device)
              .AsNoTracking()
                .ToList();

          
        }




        public Game? GetById(int id)
        {

            return _context.Games
         .Include(g => g.Category)
.Include(g => g.Devices)
.ThenInclude(gd => gd.Device)
      .AsNoTracking()
      .SingleOrDefault(g=>g.Id==id);
        

        }





        public async Task Edit(EditGameFormViewModel model)
        {
            var gameFromDb = _context.Games
                .Include(g => g.Devices)
                .SingleOrDefault(g => g.Id == model.Id);

            if (gameFromDb == null)
                return;

            string coverName = gameFromDb.Cover;

            if (model.Cover != null)
            {
                if (!string.IsNullOrEmpty(gameFromDb.Cover))
                {
                    var oldPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Covers", gameFromDb.Cover);
                    if (File.Exists(oldPath))
                        File.Delete(oldPath);
                }

                var uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Covers");
                Directory.CreateDirectory(uploadPath);

                coverName = Guid.NewGuid().ToString() + Path.GetExtension(model.Cover.FileName);
                var filePath = Path.Combine(uploadPath, coverName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await model.Cover.CopyToAsync(stream);
                }
            }

            
            gameFromDb.Name = model.Name;
            gameFromDb.Description = model.Description;
            gameFromDb.CategoryId = model.CategoryId;
            gameFromDb.Cover = coverName;

            gameFromDb.Devices.Clear();
            gameFromDb.Devices = model.SelectedDevices
                .Select(did => new GameDevice { DeviceId = did })
                .ToList();

            await _context.SaveChangesAsync();
        }


        public bool Delete(int id)
        {
            var game = _context.Games
                .Include(g => g.Devices)
                .SingleOrDefault(g => g.Id == id);

            if (game == null)
                return false;

            game.Devices.Clear();

            if (!string.IsNullOrEmpty(game.Cover))
            {
                var oldPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Covers", game.Cover);
                if (File.Exists(oldPath))
                    File.Delete(oldPath);
            }

            _context.Games.Remove(game);
            _context.SaveChanges();

            return true;
        }
    }


      







    }

