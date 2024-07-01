
namespace TechMarket.Repo
{
    public class CommentRepo : ICommentRepo
    {
        private readonly AppDbContext _appDbContext;
        public CommentRepo(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task Add(Comment comment)
        {
            await _appDbContext.Comment.AddAsync(comment);
            _appDbContext.SaveChanges();
        }

        public async Task<Comment> GetById(int commentId)
        {
            var comment = await _appDbContext.Comment.SingleOrDefaultAsync(c=>c.Id == commentId);
            if (comment == null)
            {
                return null;
            }
            return comment;
        }
        public async Task<int> RemoveComment(int commentId)
        {
            var comment = await GetById(commentId);
            if (comment == null)
            {
                return 0;
            }
            _appDbContext.Comment.Remove(comment);
            return _appDbContext.SaveChanges();

        }
    }
}
