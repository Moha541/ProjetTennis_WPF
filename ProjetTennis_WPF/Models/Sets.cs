using ProjetTennis.DAO;
using System;
using System.Collections.Generic;
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

        public void ShowScore()
        {
            if (Match.Schedule.Type == Schedule.ScheduleType.LadiesSingle || Match.Schedule.Type == Schedule.ScheduleType.GentlemanSingle)
            { 
                Console.WriteLine($"SET SCORE : {Match.Opponent1.Player1} {ScoreOp1} - {ScoreOp2} {Match.Opponent2.Player1}");
            }
            else 
            {
                Console.WriteLine($"SET SCORE : {Match.Opponent1.Player1} & {Match.Opponent1.Player2} {ScoreOp1} - {ScoreOp2} {Match.Opponent2.Player1} & {Match.Opponent2.Player2}");
            }       
        }
        public int TieBreak()
        {
            Console.WriteLine("TIE BREAK");
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
            } while (!((ScoreOp1 >= 7 && Math.Abs(ScoreOp1 - ScoreOp2) >= 2) || (ScoreOp2 >= 7 && Math.Abs(ScoreOp1 - ScoreOp2) >= 2)));


            if (ScoreOp1 > ScoreOp2)
            {
                return 1;
            }
            else
            {
                return 2;
            }
        }
        public void Play()
        {
            SetsDAO setsDAO = new SetsDAO();
            TieBreak tieBreak = new TieBreak {Match = Match };
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
                ShowScore();
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
                    ShowScore();
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
            if (ScoreOp1 > ScoreOp2)
            {
                ScoreOp1++;
            }
            else
            {
                ScoreOp2++;
            }
            setsDAO.InsertSets(this);
            this.Id_Set=setsDAO.GetIdSet(this);
        }
    }
}





