using ProjetTennis.DAO;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetTennis.Models
{
    public class Sets
    {
        public int Id_Set { get; set; }
        public int ScoreOp1 { get; set; }
        public int ScoreOp2 { get; set; }
        public Opponent WinnerOpponent { get; set; }
        public Match Match { get; set; }

       
        public void Play()
        {
            SetsDAO setsDAO = new SetsDAO();
            TieBreak tieBreak = new TieBreak();
            Random random = new Random();
            int winningPlayer;
            ScoreOp1 = 0;
            ScoreOp2=0;
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
                if (ScoreOp1 == 6 && ScoreOp2 == 6)
                {
                    if (tieBreak.Play() == 1)
                    {
                        ScoreOp1++;
                    }
                    else
                    {
                        ScoreOp2++;
                    }
                }
                if ((ScoreOp1 == 6 || ScoreOp2 == 6) && Math.Abs(ScoreOp1 - ScoreOp2) >= 2)
                {
                    if (ScoreOp1 > ScoreOp2)
                    {
                        WinnerOpponent = Match.Opponent1;
                    }
                    else
                    {
                        WinnerOpponent=Match.Opponent2;
                    }
                    break;
                }
                else if (ScoreOp1 == 7 || ScoreOp2 == 7)
                {
                    if (ScoreOp1 > ScoreOp2)
                    {
                        WinnerOpponent = Match.Opponent1;
                    }
                    else
                    {
                        WinnerOpponent = Match.Opponent2;
                    }
                    break;
                }

            } while (true);
           
            setsDAO.InsertSets(this);
            this.Id_Set=setsDAO.GetIdSet(this);
        }
    }
}





