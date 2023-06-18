using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using lab7.Entities;

namespace lab7.Pages._3
{
    public class IndexModel : PageModel
    {
        private readonly DBContext _dbContext;
        public List<ToDoItem> ToDoItems { get; set; }


        public IndexModel(DBContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void OnGet()
        {
            ToDoItems = _dbContext.ToDoItems.ToList();
        }
        public IActionResult OnPostAdd(string itemName)
        {
            var newItem = new ToDoItem
            {
                Name = itemName,
                IsComplete = false
            };

            _dbContext.ToDoItems.Add(newItem);
            _dbContext.SaveChanges();

            return RedirectToPage();
        }

        //public IActionResult OnPostDelete(long id)
        //{
        //    var itemToDelete = _dbContext.ToDoItems.Find(id);

        //    if (itemToDelete != null)
        //    {
        //        _dbContext.ToDoItems.Remove(itemToDelete);
        //        _dbContext.SaveChanges();
        //    }

        //    return RedirectToPage();
        //}
        public IActionResult OnPostToggleComplete(long id)
        {
            var itemToToggle = _dbContext.ToDoItems.Find(id);

            if (itemToToggle != null)
            {
                itemToToggle.IsComplete = !itemToToggle.IsComplete;
                _dbContext.SaveChanges();
            }

            return RedirectToPage();
        }



    }
}
