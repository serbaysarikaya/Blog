using Blog.Entities.Concrete;

namespace Blog.Mvc.Areas.Admin.Models
{
    public class UserWtihRolesViewModel
    {
        public User User { get; set; }
        public IList<string> Roles { get; set; }
    }
}
