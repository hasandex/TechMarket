namespace TechMarket.Repo.IRepo
{
    public interface IUserRepo
    {
        Task<IEnumerable<UserFormViewModel>> GetUsers();
        int Count();
    }
}
