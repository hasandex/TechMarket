using NuGet.Packaging.Signing;

namespace TechMarket.Repo.IRepo
{
    public interface ICommentRepo
    {
        Task Add(Comment comment);
        //Task<int> IncreaseLikes(int commentId);
        //Task<int> DecreaseLikes(int commentId);
        Task<Comment> GetById(int commentId);
        Task<int> RemoveComment(int commentId);
    }
}
