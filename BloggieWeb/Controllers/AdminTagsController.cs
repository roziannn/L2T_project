using BloggieWeb.Data;
using BloggieWeb.Models.Domain;
using BloggieWeb.Models.ViewModels;
using BloggieWeb.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BloggieWeb.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminTagsController : Controller
    {
        private readonly BloggieDbContext bloggieDbContext;
        private readonly ITagRepository tagRepository;

        public AdminTagsController(ITagRepository tagRepository)
        {
            this.tagRepository = tagRepository;
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [ActionName("Add")]
        public async Task<IActionResult> Add(AddTagRequest addTagRequest)
        {

            ValidateTagRequest(addTagRequest);

            if (ModelState.IsValid == false)
            {
                return View();
            }
            // added 1/3/24
            // mapping the addTagRequest to Tag domain model
            var tag = new Tag
            {
                Name = addTagRequest.Name,
                DisplayName = addTagRequest.DisplayName
            };

            await tagRepository.AddAsync(tag);

            return RedirectToAction("List");
        }

        [HttpGet]
        [ActionName("List")]
        public async Task<IActionResult> List() {

            // added 29/2/24
            //use dbcontext to read the tags
            var tags = await tagRepository.GetAllAsync();

            return View(tags);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var tag = await tagRepository.GetAsync(id);

            if(tag != null)
            {
                var editTagRequest = new EditTagRequest
                {
                    Id = tag.Id,
                    Name = tag.Name,
                    DisplayName = tag.DisplayName
                };

                return View(editTagRequest);
            }

            return View(null);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditTagRequest editTagRequest) //update method
        {
            var tag = new Tag
            {
                Id = editTagRequest.Id,
                Name = editTagRequest.Name,
                DisplayName = editTagRequest.DisplayName
            };

            var updatedTag = await tagRepository.UpdateAsync(tag);

            if (updatedTag != null)
            {
                // show success notification
            } else
            {
                // show error notification
            }

            return RedirectToAction("Edit", new { id = editTagRequest.Id});
        }

        [HttpPost]
        public async Task<IActionResult> Delete(EditTagRequest editTagRequest) 
        {
            var deletedTag = await tagRepository.DeleteAsync(editTagRequest.Id);
            if(deletedTag != null)
            {
                // show a success notification
                return RedirectToAction("List");
            }

            // show an error notifications
            return RedirectToAction("Edit", new {id = editTagRequest.Id});
        }

        private void ValidateTagRequest(AddTagRequest request)
        {
            if (request.Name is not null && request.DisplayName is not null) //cek jika input field diisi/blm
            {
                if (request.Name == request.DisplayName) // kalau input name sm displayName sama, show error
                {
                    ModelState.AddModelError("DisplayName", "Name cannont be the same as DisplayName");
                }
            }
        }
    }
}
