using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardGame.BAL
{
    public interface ICardReader
    {
        public string ParseCardList(string cardList);
    }
}
