using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CrisisConnect
{
    internal class EmergencyGenerator
    {
        private readonly List<DisasterZone> zones;
        private readonly object zonelock;
        private readonly SystemLogger logger;

        private readonly Random random = new Random();
        private int nextZoneId = 1;

        private readonly string[] disasterTypes =
        {
            "Structural Fire",
            "Earthquake",
            "Flooding",
            "Medical Emergency",
            "Dangerous Fire",
            "Mudslide"
        };

        private readonly string[] locations =
        {
            "Suburbs",
            "Industrial Area",
            "Shopping areas",
            "Highway",
            "Nature Reserve"

        };

        public EmergencyGenerator(List<DisasterZone> zones, object zonelock, SystemLogger logger)
        {
            this.zones = zones;
            this.zonelock = zonelock;
            this.logger = logger;
        }

        public async Task RunAsync(CancellationToken cancellationToken)
        {
            try
            {
                while (!cancellationToken.IsCancellationRequested)
                {
                    await Task.Delay(random.Next(5000, 10000), cancellationToken);

                    string disasterType = disasterTypes[random.Next(disasterTypes.Length)];
                    int severity = random.Next(1, 11);
                    string location = locations[random.Next(locations.Length)];

                    DisasterZone zone = new DisasterZone(nextZoneId++, disasterType, location, severity);

                    bool zoneAdded = false;

                    lock (zonelock)
                    {
                        // Only allow a maximum of 3 unresolved emergencies at a time.
                        int activeZones = zones.Count(z => !z.ResponseDispatched);

                        if (activeZones < 3)
                        {
                            zones.Add(zone);
                            zoneAdded = true;
                        }
                    }

                    if (zoneAdded)
                    {
                        logger.Log($"Emergency generated: {zone}");
                    }

                }
            }
            catch (TaskCanceledException)
            {

            }
            catch (Exception ex)
            {
                logger.Log($"Emergency generator error: {ex.Message}");

                throw new Exception("Emergency generator stopped unexpectedly.", ex);
            }

        }


    }
}
