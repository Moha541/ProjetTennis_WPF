using ProjetTennis.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetTennis.Models
{
    public class Match
    {
        public int Id_Match { get; set; }
        public DateTime DateMatch { get; set; }
        public TimeSpan Duration { get; set; }
        public int Round { get; set; }
        public Referee Referee { get; set; }
        public Court Court { get; set; }
        public Schedule Schedule { get; set; }
        public List<Sets> Sets { get; set; }
        public Opponent Opponent1 { get; set; }
        public Opponent Opponent2 { get; set; }
        public Opponent WinnerOpponent { get; set; }
        public int ScoreOp1 { get; set; }
        public int ScoreOp2 { get; set; }


        




        public Match(DateTime DateMatch, int round, Schedule schedule, Opponent opponent1, Opponent opponent2)
        {
            this.DateMatch = DateMatch;
            this.Round = round;
            this.Schedule = schedule;
            this.Opponent1 = opponent1;
            this.Opponent2 = opponent2;
            this.ScoreOp1 = ScoreOp1;
            this.ScoreOp2 = ScoreOp2;
            ScoreOp1 = 0; 
            ScoreOp2 = 0;
        }
        public Match()
        {
            
        }
        public void GetWinner(int Point1, int Point2)
        {
            
            if (Point1 < Point2)
            {
                WinnerOpponent=Opponent2;
            }
            else
            {
                WinnerOpponent=Opponent1;
            }
        }


        
        public void ShowScore()
        {
            if(Schedule.Type== Schedule.ScheduleType.LadiesSingle || Schedule.Type==Schedule.ScheduleType.GentlemanSingle)
            {
                Console.WriteLine($"MATCH SCORE : {Opponent1.Player1.Firstname} {Opponent1.Player1.Lastname} {ScoreOp1} - {ScoreOp2} {Opponent2.Player1.Firstname} {Opponent2.Player1.Lastname}");
            }
            else 
            {
                Console.WriteLine($"MATCH SCORE : {Opponent1.Player1.Firstname} {Opponent1.Player1.Lastname} & {Opponent1.Player2.Firstname} {Opponent1.Player2.Lastname} {ScoreOp1} - {ScoreOp2} {Opponent2.Player1.Firstname} {Opponent2.Player1.Lastname} & {Opponent2.Player2.Firstname} {Opponent2.Player2.Lastname}");
            }
        }
        public void Play()
        {
            RefereeDAO refereeDAO = new RefereeDAO();
            Sets set = new Sets { Match=this};
            List<Referee> referees = Referee.GetReferees();
            CourtDAO courtDAO = new CourtDAO();
            List<Court> courts = Court.GetCourts();
            foreach (Referee referee in referees)
            {
                if (referee.Available())
                {

                    Referee = referee;
                    Referee.IsAvailable = false;
                    refereeDAO.Update(Referee);
                    break;
                }
            }
        
            foreach (Court court in courts)
            {
                if (court.Available())
                {
                    Court = court;
                    Court.IsAvailable = false;
                    courtDAO.Update(Court);
                    break;
                }
            }
            
            
            if(Referee != null )
            {
                DateMatch = DateTime.Now;
                Console.WriteLine($"Arbitre : {Referee.Firstname} {Referee.Lastname}");
                Console.WriteLine($"Joueur 1 : {Opponent1.Player1.Firstname} {Opponent1.Player1.Lastname}");
                Console.WriteLine($"Joueur 2 : {Opponent2.Player1.Firstname} {Opponent2.Player1.Lastname}");
                Console.WriteLine($"Date : {DateMatch}");
                Console.WriteLine($"Round : {Round}");
                Console.WriteLine($"Court  : {Court.Id_Court}, Nombre de spectateurs : {Court.NbSpectators}");

                if (Round == 3)
                {
                    do
                    {
                        set.Play();
                        if (set.WinnerOpponent == Opponent1)
                        {
                            ScoreOp1++;
                        }
                        else
                        {
                            ScoreOp2++;
                        }
                        ShowScore();

                        if (ScoreOp1 == 1 && ScoreOp2 == 1)
                        {
                            SuperTieBreak superTieBreak = new SuperTieBreak { Match=this};
                            if (superTieBreak.Play() == 1)
                            {
                                ScoreOp1++;
                            }
                            else
                            {
                                ScoreOp2++;
                            }
                            ShowScore();
                        }
                    } while (ScoreOp1 < 2 && ScoreOp2 < 2);
                }
                else
                {
                    do
                    {
                        set.Play();
                        if (set.WinnerOpponent == Opponent1)
                        {
                            ScoreOp1++;
                        }
                        else
                        {
                            ScoreOp2++;
                        }
                        ShowScore();

                        if (ScoreOp1 == 2 && ScoreOp2 == 2)
                        {
                            SuperTieBreak superTieBreak = new SuperTieBreak {Match=this };
                            if (superTieBreak.Play() == 1)
                            {
                                ScoreOp1++;
                            }
                            else
                            {
                                ScoreOp2++;
                            }
                            ShowScore();

                        }
                    } while (ScoreOp1 < 3 && ScoreOp2 < 3);

                }
                Duration = TimeSpan.FromMinutes((ScoreOp1 + ScoreOp2) *35);
                Console.WriteLine($"Score final : {ScoreOp1} - {ScoreOp2}");
                GetWinner(ScoreOp1, ScoreOp2);
                if(Schedule.Type== Schedule.ScheduleType.LadiesSingle || Schedule.Type==Schedule.ScheduleType.GentlemanSingle)
                {
                    Console.WriteLine($"Vainqueur : {WinnerOpponent.Player1.Firstname} {WinnerOpponent.Player1.Lastname}");
                }
                else
                {
                    Console.WriteLine($"Vainqueur : {WinnerOpponent.Player1.Firstname } {WinnerOpponent.Player1.Lastname} & {WinnerOpponent.Player2.Firstname ?? "Aucun"} {WinnerOpponent.Player2.Lastname}");
                }
                Console.WriteLine($"Durée : {Duration}");
            }
            else
            {
                Console.WriteLine("aucun arbitre disponible");
            }
            Referee.Release();
            Court.Release();
        }

    }

}
