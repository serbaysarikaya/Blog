﻿using Blog.Entities.Concrete;
using Blog.Mvc.Areas.Admin.Models;
using Blog.Mvc.Models;
using Blog.Services.Abstract;
using Blog.Shared.Utilities.Results.ComplexTypes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Blog.Mvc.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin,Editor")]
    public class HomeController : Controller
    {
        private readonly ICategoryService _categoryService;
        private readonly IArticleService _articleService;
        private readonly ICommentService _commentService;
        private readonly UserManager<User> _userManager;
        public HomeController(ICategoryService categoryService, IArticleService articleService, ICommentService commentService, UserManager<User> userManager)
        {
            _categoryService = categoryService;
            _articleService = articleService;
            _commentService = commentService;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var categoryCountResult = await _categoryService.CountByNonDeletedAsync();
            var articlesCountResult = await _articleService.CountByNonDeletedAsync();
            var commentsCountResult = await _commentService.CountByNonDeletedAsync();
            var usersCount = await _userManager.Users.CountAsync();
            var articlesResult =await _articleService.GetAllAsync();
            if (categoryCountResult.ResultStatus == ResultStatus.Success && articlesCountResult.ResultStatus == ResultStatus.Success && commentsCountResult.ResultStatus == ResultStatus.Success && articlesResult.ResultStatus == ResultStatus.Success && usersCount>-1)
            {
                return View(new DashboardViewModel
                {
                     CatoriesCount =categoryCountResult.Data,
                     ArticlesCount=articlesCountResult.Data,
                     CommentCount=commentsCountResult.Data,
                     UsersCount=usersCount,
                     Articles =articlesResult.Data
                });
            }

            return NotFound();
        }


    }
}