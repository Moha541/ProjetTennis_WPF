using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetTennis.Models
{
    public class SuperTieBreak : Sets
    {
        public int Play()
        {
            Console.WriteLine("SUPER TIE BREAK :");
            Random random = new Random();
            ScoreOp1 = 0;
            ScoreOp2 = 0;
            int winningPlayer;


            do
            {

                winningPlayer = random.Next(0, 2);

                if (winningPlayer == 0)
                {
                    ScoreOp1++;
                }
                else
                {
                    ScoreOp2++;
                }
                ShowScore();

            }
           
            while (!(ScoreOp1 >= 10 && Math.Abs(ScoreOp1 - ScoreOp2) >= 2) && !(ScoreOp2 >= 10 && Math.Abs(ScoreOp1 - ScoreOp2) >= 2));

            if (ScoreOp1 > ScoreOp2)
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
