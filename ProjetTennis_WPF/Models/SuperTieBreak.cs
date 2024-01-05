using ProjetTennis.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetTennis.Models
{
    public class SuperTieBreak : Sets
    {
        public void Play()
        {
            SetsDAO setsDAO = new SetsDAO();
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

            }
           
            while (!(ScoreOp1 >= 10 && Math.Abs(ScoreOp1 - ScoreOp2) >= 2) && !(ScoreOp2 >= 10 && Math.Abs(ScoreOp1 - ScoreOp2) >= 2));

            if (ScoreOp1 > ScoreOp2)
            {
                WinnerOpponent = Match.Opponent1;
                Match.ScoreOp1++;
            }
            else
            {
                WinnerOpponent = Match.Opponent2;
                Match.ScoreOp2++;
            }

            setsDAO.InsertSets(this);
            this.Id_Set = setsDAO.GetIdSet(this);

        }
    }
}
