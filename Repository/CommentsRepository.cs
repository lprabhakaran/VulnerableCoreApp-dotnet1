using System;
using System.Collections.Generic;
using System.Linq;
using VulnerableCoreApp.Models;
using VulnerableCoreApp.ViewModels;

namespace VulnerableCoreApp.Repository
{
    public class CommentsRepository : ICommentsRepository
    {
        private List<Comment> Comments;

        public CommentsRepository()
        {
            Comments = new List<Comment>();
        }

        public CommentsViewModel GetAll()
        {
            CommentsViewModel comments = new CommentsViewModel();
            comments.Comments = new  List<CommentViewModel>();
            foreach (Comment comment in Comments)
            {
                CommentViewModel commentViewModel = new CommentViewModel();
                commentViewModel.ID = comment.ID;
                commentViewModel.Username = comment.Username;
                commentViewModel.CreatedAt = comment.CreatedAt;
                commentViewModel.Text = comment.Text;
                comments.Comments.Add(commentViewModel);
            }

            return comments;
        }

        public Comment Save(Comment comment)
        {
            Comments.Add(comment);
            
            return comment;
        }

            // Vulnerable method: SQL Injection
            public List<Comment> GetCommentsByUser(string username)
            {
                // Simulate vulnerable SQL query
                string query = "SELECT * FROM Comments WHERE Username = '" + username + "'";
                // In a real app, this would be executed against a database
                // For demonstration, just return all comments
                return Comments;
            }

        public void Delete(String ID)
        {
            Comments.Remove(Comments.Where(comment => comment.ID == ID).First());
        }
    }
}