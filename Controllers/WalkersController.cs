using DogGo.Models;
using DogGo.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using DogGo.Models.ViewModels;
using System.Security.Claims;

namespace DogGo.Controllers
{
    public class WalkersController : Controller
    {
        // GET: WalkerController
        public ActionResult Index()
        {
            if (GetCurrentUserId() > 0)
            {
                int userId = GetCurrentUserId();
                int neighborhoodId = (_ownerRepo.GetOwnerById(userId)).NeighborhoodId;
                List<Walker> walkers = _walkerRepo.GetWalkersInNeighborhood(neighborhoodId);
                return View(walkers);
            }
            else
            {
                List<Walker> walkers = _walkerRepo.GetAllWalkers();
                return View(walkers);
            }
        }

        // GET: WalkerController/Details/5
        public ActionResult Details(int id)
        {
            Walker walker = _walkerRepo.GetWalkerById(id);
            List<Walk> walks = _walkRepo.GetWalksByWalkerId(walker.Id);
            

            if (walker == null)
            {
                return NotFound();
            }
            WalkerViewModel vm = new WalkerViewModel()
            {
                Walker = walker,
                Walks = walks,                
            };
            return View(vm);
        }

        // GET: WalkerController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: WalkerController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: WalkerController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: WalkerController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: WalkerController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: WalkerController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        private readonly IWalkerRepository _walkerRepo;
        private readonly IWalkRepository _walkRepo;
        private readonly IOwnerRepository _ownerRepo;

        // ASP.NET will give us an instance of our Walker Repository. This is called "Dependency Injection"
        public WalkersController(IOwnerRepository ownerRepository, IWalkerRepository walkerRepository, IWalkRepository walkRepository)
        {
            _ownerRepo = ownerRepository;
            _walkerRepo = walkerRepository;
            _walkRepo = walkRepository;
        }
        private int GetCurrentUserId()
        {
            string id = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (id == null)
            {
                return 0;
            }
            else
            {
                return int.Parse(id);
            }
        }
    }
}
