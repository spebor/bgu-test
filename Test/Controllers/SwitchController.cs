using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Test.ViewModels.Switch;

namespace Test.Controllers
{
    public class SwitchController : Controller
    {
        private readonly ApplicationDbContext _dbContext;

        public SwitchController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IActionResult> Index(IndexViewModel.FilterModel filter, int pageIndex = 1, int pageSize = 5)
        {
            var query = _dbContext.Switches
                .Where(sw => filter.InstallationFloor == null || sw.InstallationFloor == filter.InstallationFloor)
                .Where(sw => filter.Query == null || sw.ModelName.Contains(filter.Query) || sw.SerialNumber.Contains(filter.Query) || sw.InventoryNumber.Contains(filter.Query))
                .OrderBy(sw => sw.Id).Select(sw => new IndexViewModel.Item
                {
                    Id = sw.Id,
                    ModelName = sw.ModelName,
                    InstallationFloor = sw.InstallationFloor
                });

            var vm = new IndexViewModel
            {
                Filter = filter,
                Items = await PaginatedList<IndexViewModel.Item>.CreateAsync(query.AsNoTracking(), pageIndex, pageSize)
            };

            return View(vm);
        }


        [HttpGet]
        public IActionResult Create()
        {
            var vm = new CreateViewModel
            {
                ManagementVlan = 1,
                PurchaseDate = DateTime.Today
            };
            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var sw = new Models.Switch
                {
                    ModelName = vm.ModelName,
                    IpAddress = vm.IpAddress,
                    MacAddress = vm.MacAddress,
                    ManagementVlan = vm.ManagementVlan,
                    SerialNumber = vm.SerialNumber,
                    InventoryNumber = vm.InventoryNumber,
                    PurchaseDate = vm.PurchaseDate,
                    InstallationDate = vm.InstallationDate,
                    InstallationFloor = vm.InstallationFloor,
                    Comment = vm.Comment
                };

                var errorMessages = Validate(sw);
                if (errorMessages.Any())
                {
                    foreach (var err in errorMessages) ModelState.AddModelError(err.Key, err.Value);
                }
                else
                {
                    await _dbContext.AddAsync(sw);
                    await _dbContext.SaveChangesAsync();

                    return RedirectToAction(nameof(Index));
                }
            }

            return View(vm);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var vm = await _dbContext.Switches.Where(sw => sw.Id == id).Select(sw => new EditViewModel
            {
                Id = sw.Id,
                ModelName = sw.ModelName,
                IpAddress = sw.IpAddress,
                MacAddress = sw.MacAddress,
                ManagementVlan = sw.ManagementVlan,
                SerialNumber = sw.SerialNumber,
                InventoryNumber = sw.InventoryNumber,
                PurchaseDate = sw.PurchaseDate,
                InstallationDate = sw.InstallationDate,
                InstallationFloor = sw.InstallationFloor,
                Comment = sw.Comment
            }).SingleOrDefaultAsync();

            if (vm == null) return NotFound();

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var sw = await _dbContext.Switches.FindAsync(vm.Id);

                if (sw == null) return NotFound();

                sw.ModelName = vm.ModelName;
                sw.IpAddress = vm.IpAddress;
                sw.MacAddress = vm.MacAddress;
                sw.ManagementVlan = vm.ManagementVlan;
                sw.SerialNumber = vm.SerialNumber;
                sw.InventoryNumber = vm.InventoryNumber;
                sw.PurchaseDate = vm.PurchaseDate;
                sw.InstallationDate = vm.InstallationDate;
                sw.InstallationFloor = vm.InstallationFloor;
                sw.Comment = vm.Comment;

                var errorMessages = Validate(sw);
                if (errorMessages.Any())
                {
                    foreach (var err in errorMessages) ModelState.AddModelError(err.Key, err.Value);
                }
                else
                {
                    await _dbContext.SaveChangesAsync();

                    return RedirectToAction(nameof(Index));
                }
            }

            return View(vm);
        }

        public async Task<IActionResult> Remove(int id)
        {
            var sw = await _dbContext.Switches.FindAsync(id);
            if (sw != null)
            {
                _dbContext.Remove(sw);

                await _dbContext.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        private Dictionary<string, string> Validate(Models.Switch sw)
        {
            var errors = new Dictionary<string, string>();

            if (sw.InstallationDate != null && sw.PurchaseDate > sw.InstallationDate)
            {
                errors.Add(string.Empty, $"Дата установки больше даты покупки");
                return errors;
            }

            if (sw.PurchaseDate > DateTime.Today)
            {
                errors.Add(string.Empty, "Дата покупки больше текущей даты");
                return errors;
            }

            return errors;
        }

        public static IEnumerable<int> Floors => Enumerable.Range(1, 5);
    }
}
