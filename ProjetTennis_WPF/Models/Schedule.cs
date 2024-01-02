using ProjetTennis.DAO;
using ProjetTennis.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ProjetTennis.Models.Schedule;

namespace ProjetTennis.Models
{
    public class Schedule
    {
        public int Id_Schedule { get; set; }
        public enum ScheduleType { GentlemanSingle, LadiesSingle, GentlemanDouble, LadiesDouble, MixedDouble }
        public ScheduleType Type { get; set; }
        public int ActualRound { get; set; }
        public Tournament Tournament { get; set; }
        public List<Match> Matches { get; set; }
        List<Opponent> winners = new List<Opponent>();

        
        public void Play(Schedule schedule)
        {
            this.Tournament = schedule.Tournament;
            this.Type = schedule.Type;
            this.ActualRound = schedule.ActualRound;
            ScheduleDAO scheduleDAO = new ScheduleDAO();
            scheduleDAO.InsertSchedule(this);
            this.Id_Schedule = scheduleDAO.GetScheduleId(this);

            if (ActualRound < 6)  // On ne dépasse pas les 6 rounds 
            {

                PlayNextRoundAsync();
                Console.WriteLine(Matches.Count);

                winners = GetWinners();
                ActualRound++;

                if (ActualRound == 6)
                {
                    Console.WriteLine("Tournoi terminé. Vainqueur : " + winners[0].Player1.Lastname);
                }

            }
        }


         public void CreateMatch()
        {
            MatchDAO matchDAO = new MatchDAO();
            Matches = new List<Match>();
            PlayerDAO playerDAO = new PlayerDAO();
            OpponentDAO opponentDAO = new OpponentDAO();
            List<Player> players = null;
            // Affichage de la valeur de l'énumération
            Console.WriteLine($"Le type de match est : {Type}");
            // Utilisation de l'énumération dans un switch
            switch (Type)
            {
                case ScheduleType.GentlemanSingle:
                    players = playerDAO.GetPlayers64Male();
                    break;
                case ScheduleType.LadiesSingle:
                    players = playerDAO.GetPlayers64Female();
                    break;
                case ScheduleType.GentlemanDouble:
                    players = playerDAO.GetPlayers128Male();
                    break;
                case ScheduleType.LadiesDouble:
                    players = playerDAO.GetPlayers128Female();
                    break;
                case ScheduleType.MixedDouble:
                    players = playerDAO.GetPlayers128Mixed();
                    break;
                default:
                    Console.WriteLine("Type de match inconnu !");
                    break;
            }

            // Utilisez un objet Random pour générer des indices aléatoires
            Random random = new Random();

            // Mélangez la liste des joueurs de manière aléatoire
            players = players.OrderBy(x => random.Next()).ToList();

            // Assurez-vous qu'il y a au moins deux joueurs dans la liste
            if (players.Count < 2)
            {
                Console.WriteLine("Il n'y a pas suffisamment de joueurs pour jouer.");
                return;
            }
            Opponent opponent1 = new Opponent();
            Opponent opponent2 = new Opponent();
            foreach (var player in players)
            {
                Console.WriteLine(player);
            }
            // Parcourez la liste en prenant deux joueurs à la fois pour les faire jouer l'un contre l'autre
            if (Type == ScheduleType.GentlemanSingle)
            {
                for (int i = 0; i < players.Count; i += 2)
                {
                    opponent1 = new Opponent(); // Create new opponent for each iteration
                    opponent2 = new Opponent(); // Create new opponent for each iteration
                    opponent1.Player1 = players[i];
                    opponent2.Player1 = players[i + 1];
                    Match match = new Match(DateTime.Now, 5, this, opponent1, opponent2);
                    Matches.Add(match);
                    opponentDAO.InsertOpponentSingle(opponent1);
                    opponentDAO.InsertOpponentSingle(opponent2);
                    opponent1.Id_Opponent = opponentDAO.GetIdOpponentSingle(opponent1);
                    opponent2.Id_Opponent = opponentDAO.GetIdOpponentSingle(opponent2);
                }
            }
            else if (Type == ScheduleType.LadiesSingle)
            {
                for (int i = 0; i < players.Count; i += 2)
                {
                    opponent1 = new Opponent(); // Create new opponent for each iteration
                    opponent2 = new Opponent(); // Create new opponent for each iteration
                    opponent1.Player1 = players[i];
                    opponent2.Player1 = players[i + 1];
                    Match match = new Match(DateTime.Now, 3, this, opponent1, opponent2);
                    Matches.Add(match);
                    opponentDAO.InsertOpponentSingle(opponent1);
                    opponentDAO.InsertOpponentSingle(opponent2);
                    opponent1.Id_Opponent = opponentDAO.GetIdOpponentSingle(opponent1);
                    opponent2.Id_Opponent = opponentDAO.GetIdOpponentSingle(opponent2);
                }
            }
            else if (Type == ScheduleType.GentlemanDouble || Type == ScheduleType.LadiesDouble)
            {
                for (int i = 0; i < players.Count; i += 4)
                {
                    opponent1 = new Opponent(); // Create new opponent for each iteration
                    opponent2 = new Opponent(); // Create new opponent for each iteration
                    opponent1.Player1 = players[i];
                    opponent1.Player2 = players[i + 1];
                    opponent2.Player1 = players[i + 2];
                    opponent2.Player2 = players[i + 3];
                    Match match = new Match(DateTime.Now, 3, this, opponent1, opponent2);
                    Matches.Add(match);
                    opponentDAO.InsertOpponentDouble(opponent1);
                    opponentDAO.InsertOpponentDouble(opponent2);
                    opponent1.Id_Opponent = opponentDAO.GetIdOpponentDouble(opponent1);
                    opponent2.Id_Opponent = opponentDAO.GetIdOpponentDouble(opponent2);
                }
            }
            else if (Type == ScheduleType.MixedDouble)
            {
                List<Player> playersM = new List<Player>();
                List<Player> playersF = new List<Player>();
                foreach (var player in players)
                {
                    if (player.Gender == "Male")
                    {
                        playersM.Add(player);
                    }
                    else
                    {
                        playersF.Add(player);
                    }
                }
                for (int i = 0; i < 64; i += 2)
                {
                    opponent1 = new Opponent(); // Create new opponent for each iteration
                    opponent2 = new Opponent(); // Create new opponent for each iteration
                    opponent1.Player1 = playersM[i];
                    opponent1.Player2 = playersF[i];
                    opponent2.Player1 = playersM[i + 1];
                    opponent2.Player2 = playersF[i + 1];
                    Match match = new Match(DateTime.Now, 3, this, opponent1, opponent2);
                    Matches.Add(match);
                    opponentDAO.InsertOpponentDouble(opponent1);
                    opponentDAO.InsertOpponentDouble(opponent2);
                    opponent1.Id_Opponent = opponentDAO.GetIdOpponentDouble(opponent1);
                    opponent2.Id_Opponent = opponentDAO.GetIdOpponentDouble(opponent2);

                }
            }
        }
        public List<Opponent> GetWinners()
        {
            winners.Clear();
            foreach (var match in Matches)
            {
                Opponent winnerOpponent = match.WinnerOpponent;
                if (winnerOpponent != null)
                {
                    winners.Add(winnerOpponent);
                }
            }

            return winners;
        }
        private async Task PlayMatchAsync(Match match)
        {
            // Ajoutez une marque de temps avant le match
            DateTime startTime = DateTime.Now;
            Trace.WriteLine($"Match {Matches.IndexOf(match)} - Début : {startTime}");

            // Logique pour jouer un match de manière asynchrone
            await match.PlayAsync();

            // Ajoutez une marque de temps après le match
            DateTime endTime = DateTime.Now;
            Trace.WriteLine($"Match {Matches.IndexOf(match)} - Fin : {endTime}");

            // Comparez les marques de temps pour voir si le match a été joué simultanément
            TimeSpan duration = endTime - startTime;
            Trace.WriteLine($"Durée du match {Matches.IndexOf(match)} : {duration.TotalSeconds} secondes");
        }

        public async Task PlayNextRoundAsync()
        {
            if (Matches == null)
            {
                CreateMatch();

                // Utilisez Task.WhenAll pour exécuter plusieurs matches en parallèle de manière asynchrone
                var playTasks = Matches.Select(match => PlayMatchAsync(match)).ToList();

                // Attendez que tous les matches soient terminés
                await Task.WhenAll(playTasks);

                // Affichez le numéro de chaque match joué
                for (int j = 0; j < Matches.Count; j++)
                {
                    Console.WriteLine(j);
                }
            }
            else
            {
                List<Opponent> winners = GetWinners();
                Matches.Clear();

                var matchCreationTasks = new List<Task>();

                for (int i = 0; i < winners.Count; i += 2)
                {
                    Opponent opponent1 = winners[i];
                    Opponent opponent2 = (i + 1 < winners.Count) ? winners[i + 1] : null;

                    Match match = new Match(DateTime.Now, 3, this, opponent1, opponent2);
                    Matches.Add(match);

                    // Créez une tâche pour jouer chaque match de manière asynchrone
                    matchCreationTasks.Add(PlayMatchAsync(match));
                }

                // Attendez que tous les nouveaux matches soient créés et joués
                await Task.WhenAll(matchCreationTasks);

                Console.WriteLine($"Nouveau tour généré avec {Matches.Count} matchs.");

                for (int j = 0; j < Matches.Count; j++)
                {
                    Console.WriteLine(j);
                }
            }
        }


    }







}




































