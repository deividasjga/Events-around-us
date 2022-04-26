using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RenginiaiWebApp.Data;
using RenginiaiWebApp.Interfaces;
using RenginiaiWebApp.Models;
using RenginiaiWebApp.ViewModels;

namespace RenginiaiWebApp.Controllers
{
    public class EventController : Controller
    {
        private readonly IEventRepository _eventRepository;
        private readonly IPhotoService _photoService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public EventController(IEventRepository eventRepository, IPhotoService photoService, IHttpContextAccessor httpContextAccessor)
        {
            _eventRepository = eventRepository;
            _photoService = photoService;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<IActionResult> Index()
        {
            IEnumerable<Event> events = await _eventRepository.GetAll();
            return View(events);
        }
        public async Task<IActionResult> Detail(int id)
        {
            Event events = await _eventRepository.GetByIdAsync(id);   // Event event
             return View(events);  // event
        }

        public IActionResult Create()
        {
            var curUserID = _httpContextAccessor.HttpContext?.User.GetUserId();
            var createEventViewModel = new CreateEventViewModel { AppUserId = curUserID };
            return View(createEventViewModel);
        }

        [HttpPost]
        public async Task<IActionResult>Create(CreateEventViewModel eventsVM)
        {
            if (ModelState.IsValid)
            {
                var result = await _photoService.AddPhotoAsync(eventsVM.Image);

                var events = new Event
                {
                    Title = eventsVM.Title,
                    Description = eventsVM.Description,
                    Image = result.Url.ToString(),
                    AppUserId = eventsVM.AppUserId,
                    Address = new Address
                    {
                        Street = eventsVM.Address.Street,
                        City = eventsVM.Address.City,
                    }
                };
                _eventRepository.Add(events);
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("", "Photo upload failed");
            }
            return View(eventsVM);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var events = await _eventRepository.GetByIdAsync(id);
            if (events == null) return View("Error");
            var eventsVM = new EditEventViewModel
            {
                Title = events.Title,
                Description = events.Description,
                AddressId = events.AddressId,
                Address = events.Address,
                URL = events.Image,
                EventCategory = events.EventCategory
            };
            return View(eventsVM);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, EditEventViewModel eventsVM)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Failed to edit event");
                return View("Edit", eventsVM);
            }

            var userEvent = await _eventRepository.GetByIdAsyncNoTracking(id);

            if (userEvent != null)
            {
                try
                {
                    await _photoService.DeletePhotoAsync(userEvent.Image);
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Could not delete photo");
                    return View(eventsVM);
                }
                var photoResult = await _photoService.AddPhotoAsync(eventsVM.Image);


                var events = new Event
                {
                    Id = id,
                    Title = eventsVM.Title,
                    Description = eventsVM.Description,
                    Image = photoResult.Url.ToString(),
                    AddressId = eventsVM.AddressId,
                    Address = eventsVM.Address,
                };

                _eventRepository.Update(events);

                return RedirectToAction("Index");
            }
            else
            {
                return View(eventsVM);
            }
        }

        public async Task<IActionResult> Delete(int id)
        {
            var eventsDetails = await _eventRepository.GetByIdAsync(id);
            if (eventsDetails == null) return View("Error");
            return View(eventsDetails);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteClub(int id)
        {
            var eventDetails = await _eventRepository.GetByIdAsync(id);
            if (eventDetails == null) return View("Error");

            _eventRepository.Delete(eventDetails);
            return RedirectToAction("Index");
        }

    }
}
