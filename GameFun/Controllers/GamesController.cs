using GameFun.Data;
using GameFun.Services;
using Microsoft.AspNetCore.Mvc;



namespace GameFun.Controllers
{
    public class GamesController : Controller
    {
        private readonly ApplicationDbContext _context;
       private readonly ICategoriesServies _categoriesServies;
        private readonly IDevicesServices _devicesServies;
        private readonly IGameServices _gameServices;

        public GamesController(ApplicationDbContext context, ICategoriesServies categoriesServies, IDevicesServices devicesServies,IGameServices gameServices)
        {
            _context = context;
            _categoriesServies = categoriesServies;
            _devicesServies = devicesServies;
            _gameServices = gameServices;
        }
        public IActionResult Index()
        {
            var games = _gameServices.GetAll();
            return View(games);
        }


        [HttpGet]
        public IActionResult Create()
        {
            CreateGameFormViewModel ViewModel = new()
            {
                Categories = _categoriesServies.GetSelectList(),
              
                
                Devices = _devicesServies.GetSelectList(),





            };

            return View(ViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateGameFormViewModel Model)
        {
            if (!ModelState.IsValid) { 
            Model.Categories = _categoriesServies.GetSelectList();
            Model.Devices = _devicesServies.GetSelectList();
            return View(Model);
            }

            await _gameServices.Create(Model);

            return RedirectToAction(nameof(Index));
        }


        public IActionResult Details(int Id)
        {
            var game= _gameServices.GetById(Id);
            if (game == null)
                return NotFound();

            return View(game);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var game = _gameServices.GetById(id);
            if (game == null)
                return NotFound();


            var vm = new EditGameFormViewModel
            {
                Id = game.Id,
                Name = game.Name,
                Description = game.Description,
                CategoryId = game.CategoryId,
                CurrentCover = game.Cover,
                Categories = _categoriesServies.GetSelectList(), 

                SelectedDevices = game.Devices
           .Select(d => d.DeviceId)
           .ToList(),

                Devices = _devicesServies.GetSelectList(), 
            };


            return View(vm);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(EditGameFormViewModel Model)
        {

            if (!ModelState.IsValid)
            {
                Model.Categories = _categoriesServies.GetSelectList();
                Model.Devices = _devicesServies.GetSelectList();
                return View(Model);
            }
            await _gameServices.Edit(Model);

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            var isDeleted = _gameServices.Delete(id);
            return isDeleted ? Ok() : BadRequest();
        }
       




    }
}
