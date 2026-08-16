using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CrisisConnect
{
    class DispatchProcessor
    {
        private readonly SystemLogger logger;
        public event MissionStatusHandler OnMissionCompleted;
        public DispatchProcessor(SystemLogger logger)
        {
            this.logger = logger;
        }

        public async Task DispatchAsync(CrisisRescueSquad team, DisasterZone zone, CancellationToken cancellationToken)
        {
            try
            {
                // Use the interface contract to dispatch the selected team.
                if (team is iDispatchAndRecall dispatchableTeam)
                {
                    dispatchableTeam.dispatch(zone.Location);
                }

                lock (ConsoleLock.LockObject)
                {
                    Console.WriteLine();
                    Console.WriteLine("======================================");
                    Console.WriteLine("          DISPATCH PROCESSOR");
                    Console.WriteLine("======================================");
                    Console.WriteLine(
                        $"{team.teamName} is travelling to {zone.Location}"
                    );
                }

                logger.Log(
                    $"Dispatch started: {team.teamName} to {zone.Location}"
                );

                // Simulates travel/mission time without blocking the console.
                await Task.Delay(5000, cancellationToken);

                // Disaster has now received a response.
                zone.MarkAsResponded();

                // Mission has been completed, therefore unit becomes available again.
                team.status = "Available";

                lock (ConsoleLock.LockObject)
                {
                    Console.WriteLine();
                    Console.WriteLine("======================================");
                    Console.WriteLine("          MISSION COMPLETED");
                    Console.WriteLine("======================================");
                    Console.WriteLine(
                        $"{team.teamName} completed the response at {zone.Location}"
                    );
                }

                logger.Log(
                    $"Dispatch completed: {team.teamName} completed mission at {zone.Location}"
                );

                // EVENT TRIGGER:
                // Notify every subscriber that the mission has completed.
                OnMissionCompleted?.Invoke(
                    team.disasterTeamID,
                    team.teamName,
                    zone.ZoneId,
                    "Mission Completed"
                );
            }
            catch (TaskCanceledException)
            {
                logger.Log($"Dispatch cancelled for {team.teamName}.");
            }
            catch (Exception ex)
            {
                logger.Log($"Dispatch error: {ex.Message}");
            }
        }
    }
}
