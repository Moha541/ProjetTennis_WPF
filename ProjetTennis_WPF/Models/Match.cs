using ProjetTennis.DAO;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
                WinnerOpponent = Opponent2;
            }
            else
            {
                WinnerOpponent = Opponent1;
            }
        }



        public async Task PlayAsync()
        {
            MatchDAO matchDAO = new MatchDAO();
         
            if (Schedule.Tournament.availableReferees.Count > 0)
            {

                Referee = Schedule.Tournament.availableReferees.Dequeue();
                Referee.IsAvailable = false;
                RefereeDAO refereeDAO = new RefereeDAO();
                refereeDAO.Update(Referee);

                // Utilisez un court seulement s'il est disponible
                if (Schedule.Tournament.availableCourts.Count > 0)
                {
                    Court = Schedule.Tournament.availableCourts.Dequeue();
                    Court.IsAvailable = false;
                    CourtDAO courtDAO = new CourtDAO();
                    courtDAO.Update(Court);

                }
                else
                {
                    // Libérez l'arbitre s'il n'y a pas de court disponible
                    Referee.Release();
                    Schedule.Tournament.availableReferees.Enqueue(Referee);
                    Console.WriteLine("Aucun court disponible");
                }
            }





            if (Referee != null)
            {
                matchDAO.InsertMatch(this);
                Id_Match = matchDAO.GetIdMatch(this);
                Trace.WriteLine($"Match : {Id_Match}");
                Sets set = new Sets { Match = this };
                DateMatch = DateTime.Now;

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

                        if (ScoreOp1 == 1 && ScoreOp2 == 1)
                        {
                            SuperTieBreak superTieBreak = new SuperTieBreak { Match = this };
                            if (superTieBreak.Play() == 1)
                            {
                                ScoreOp1++;
                            }
                            else
                            {
                                ScoreOp2++;
                            }
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

                        if (ScoreOp1 == 2 && ScoreOp2 == 2)
                        {
                            SuperTieBreak superTieBreak = new SuperTieBreak { Match = this };
                            if (superTieBreak.Play() == 1)
                            {
                                ScoreOp1++;
                            }
                            else
                            {
                                ScoreOp2++;
                            }

                        }
                    } while (ScoreOp1 < 3 && ScoreOp2 < 3);

                }

                
                Duration = TimeSpan.FromMinutes((ScoreOp1 + ScoreOp2) * 35);
                matchDAO.UpdateMatch(this);
                GetWinner(ScoreOp1, ScoreOp2);
                Referee.Release();
                Schedule.Tournament.availableReferees.Enqueue(Referee);
                Court.Release();
                Schedule.Tournament.availableCourts.Enqueue(Court);
                await Task.Delay(1000);
            }
            else
            {
                Trace.WriteLine("aucun arbitre disponible");
            }
            
        }

    }

}




