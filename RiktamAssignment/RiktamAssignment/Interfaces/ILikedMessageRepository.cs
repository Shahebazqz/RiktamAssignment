using RiktamAssignment.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RiktamAssignment.Interfaces
{
    public interface ILikedMessageRepository
    {
        LikedMessage LikeMessage(LikedMessage likedMessage);
        IEnumerable<LikedMessage> GetMessageLikes(int messageId);
        IEnumerable<LikedMessage> GetUserLikes(string username);
        bool DeleteMessageLike(int messageId, string username);
    }
}
