using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetTennis.Models
{
    public class TieBreak
    {
        public int Play()
        {
            
            Random random = new Random();
            int ScoreTieBreak1 = 0;
            int ScoreTieBreak2 = 0;
            int winningPlayer;


            do
            {

                winningPlayer = random.Next(0, 2);

                if (winningPlayer == 0)
                {
                    ScoreTieBreak1++;
                }
                else
                {
                    ScoreTieBreak2++;
                }
                
            }
            while (!((ScoreTieBreak1 >= 7 && Math.Abs(ScoreTieBreak1 - ScoreTieBreak2) >= 2) || (ScoreTieBreak2 >= 7 && Math.Abs(ScoreTieBreak1 - ScoreTieBreak2) >= 2)));
           
            if (ScoreTieBreak1 > ScoreTieBreak2)
            {
                return 1;
            }
            else
            {
                return 2;
            }

        }
    }
}
