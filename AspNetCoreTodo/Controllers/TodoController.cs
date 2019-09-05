using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AspNetCoreTodo.Services;
using AspNetCoreTodo.Models;

namespace AspNetCoreTodo.Controllers
{
    public class TodoController : Controller
    {
        private ITodoItemService _todoItemService;
        public TodoController(ITodoItemService todoItemService)
        {
            _todoItemService = todoItemService;
        }

        public async Task<IActionResult> Index()
        {
            // 从数据库获取 to-do 条目
            var items = await _todoItemService.GetIncompleteItemsAsync();
            // 把条目置于 model 中
            var model = new TodoViewModel()
            {
                Items = items
            };
            // 使用 model 渲染视图
            return View(model);
        }
    }
}