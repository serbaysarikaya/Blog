﻿using Blog.Entities.Concrete;
using Blog.Entities.Dtos;

namespace Blog.Mvc.Areas.Admin.Models
{
    public class DashboardViewModel
    {
        public int CatoriesCount { get; set; }
        public int ArticlesCount { get; set; }
        public int CommentCount { get; set; }
        public int UsersCount { get; set; }
        public ArticleListDto Articles { get; set; }
    }
}
