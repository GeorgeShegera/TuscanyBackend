using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tuscany.Utility
{
    public static class DefaultAvatars
    {
        public static readonly List<string> _avatars = new()
        {
            "https://i.imgur.com/KihN1LR.jpeg",
            "https://i.imgur.com/7Zdjuqa.jpg",
            "https://i.imgur.com/axvyoIE.jpeg",
            "https://i.imgur.com/i9T8Qni.jpg"
        };

        public static string GetRandomImg()
        {
            Random rnd = new Random();
            return _avatars[rnd.Next(0, _avatars.Count)];
        }
    }
}
