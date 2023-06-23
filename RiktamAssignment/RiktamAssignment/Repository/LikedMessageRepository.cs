using RiktamAssignment.Interfaces;
using RiktamAssignment.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RiktamAssignment.Repository
{
    public class LikedMessageRepository : ILikedMessageRepository
    {
        private readonly DataContext dataContext;

        public LikedMessageRepository(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }
      
        public IEnumerable<LikedMessage> GetMessageLikes(int messageId)
        {
            return dataContext.LikedMessages.Where(e => e.MessageId == messageId).ToList();
        }

        public IEnumerable<LikedMessage> GetUserLikes(string username)
        {
            return dataContext.LikedMessages.Where(e => e.Username == username);
        }

        public LikedMessage LikeMessage(LikedMessage likedMessage)
        {
            var existingLikedMessage = dataContext.LikedMessages.FirstOrDefault(e => e.MessageId == likedMessage.MessageId && e.Username == likedMessage.Username);
            if (existingLikedMessage == null)
            {
                dataContext.LikedMessages.Add(likedMessage);
                dataContext.SaveChanges();
                return likedMessage;
            }
            return likedMessage;
        }
        public bool DeleteMessageLike(int messageId, string username)
        {
            var likedMessage = dataContext.LikedMessages.FirstOrDefault(e => e.MessageId == messageId && e.Username == username);
            if (likedMessage == null)
                return false;
            dataContext.LikedMessages.Remove(likedMessage);
            dataContext.SaveChanges();
            return true;
        }

    }
}
