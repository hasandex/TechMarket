

namespace TechMarket.Repo
{
    public class UserRepo : IUserRepo
    {
        private readonly UserManager<ApplicationUser> _userManager;
        public UserRepo(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        async Task<IEnumerable<UserFormViewModel>> IUserRepo.GetUsers()
        {
            var users = await _userManager?.Users.ToListAsync();
            if (!users.Any())
            {
                return null;
            }
            var userFormVM = new List<UserFormViewModel>();
            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);
                var userForm = new UserFormViewModel
                {
                    Id = user.Id,
                    FirstName = user.FName,
                    LastName = user.FName,
                    UserName = user.UserName,
                    Email = user.Email,
                    Roles =  roles,
                };
                userFormVM.Add(userForm);
            }
            return userFormVM;
        }
    }
}
