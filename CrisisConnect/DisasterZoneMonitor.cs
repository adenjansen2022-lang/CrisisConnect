using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CrisisConnect
{
    internal class DisasterZoneMonitor
    {
        private readonly List<DisasterZone> zones;
        private readonly object zonelock;
        private readonly SystemLogger logger;

        public DisasterZoneMonitor(List<DisasterZone> zones, object zonelock, SystemLogger logger)
        {
            this.zones = zones;
            this.zonelock = zonelock;
            this.logger = logger;
        }

        public async Task RunAsync(CancellationToken cancellationToken)
        {
            try
            {
                while(!cancellationToken.IsCancellationRequested)
                {
                    await Task.Delay(5000, cancellationToken);

                    lock(zonelock)
                    {
                        foreach(DisasterZone zone in zones)
                        {
                            if(!zone.ResponseDispatched && zone.ThreatLevel <100)
                            {
                                zone.IncreaseThreat(5);

                                logger.Log($"Threat increased: Zone {zone.ZoneId}" + $"is now {zone.ThreatLevel}%.");

                                lock (ConsoleLock.LockObject)
                                {
                                    Console.WriteLine($"Threat Update" +
                                    $"===============" +
                                    $"Zone {zone.ZoneId}:" +
                                    $"{zone.ThreatLevel}%"); 

                                }
                            }
                        }
                    }
                }
            }
            catch(TaskCanceledException)
            {
                
            }
            catch(Exception ex)
            {
                logger.Log($"Zone monitor error: {ex.Message}");
                throw new Exception("Disaster Zone Monitor stopped unexpectedly");
            }
        }
    }
}
