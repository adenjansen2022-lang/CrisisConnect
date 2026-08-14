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
        public DispatchProcessor(SystemLogger logger)
        {
            this.logger = logger;
        }

        public async Task DispatchAsync(CrisisRescueSquad team, DisasterZone zone, CancellationToken cancellationToken)
        {
            try
            {
                lock(ConsoleLock.LockObject)
                {
                    Console.WriteLine($"Dispatch Processor" +
                                      $"==================" +
                                      $"{team.teamName} is travelling to"+
                                      $"{zone.Location}");
                }
                logger.Log($"Dispatch started: {team.teamName} to {zone.Location}");

                await Task.Delay(5000,cancellationToken); 

                zone.MarkAsResponded();

                lock (ConsoleLock.LockObject)
                {
                    Console.WriteLine($"Dispatch Processor" +
                                      $"==================" +
                                      $"{team.teamName} has arrived at" +
                                      $"{zone.Location}");
                }

                logger.Log($"Dispatch completed: {team.teamName} has arrived at {zone.Location}");
            }
            catch(TaskCanceledException)
            {
                logger.Log($"Dispatch cancelled for {team.teamName}.");
            }
            catch(Exception ex)
            {
                logger.Log($"Dispatch error:{ex.Message}");

            }
        }
    }
}
