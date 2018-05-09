using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TopsyTurvyCakes.Models;

namespace TopsyTurvyCakes.Pages.Admin
{
    public class AddEditRecipeModel : PageModel
    {
        private readonly IRecipesService _service;

        [FromRoute]
        public long? Id { get; set; }

        public bool IsNewRecipe => Id == null;

        [BindProperty]
        public Recipe Recipe { get; set; }

        public AddEditRecipeModel(IRecipesService service)
        {
            _service = service;
        }

        public async Task OnGetAsync()
        {            
            Recipe = await _service.FindAsync(Id.GetValueOrDefault()) ?? new Recipe();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            Recipe.Id = Id.GetValueOrDefault();

            await _service.SaveAsync(Recipe);

            return RedirectToPage("/Recipe", new { id = Recipe.Id });            
        }

        public async Task<IActionResult> OnPostDelete()
        {
            await _service.DeleteAsync(Id.Value);
            return RedirectToPage("/Index");
        }
    }
}