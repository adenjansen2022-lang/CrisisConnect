using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrisisConnect
{
    internal class DisasterZone
    {
        public int ZoneId { get; private set; }
        public string DisasterType { get; private set; }
        public string Location { get; private set; }
        public int Severity { get; private set; }   
        public int ThreatLevel { get; private set; } 
        public bool ResponseDispatched { get; private set; }    
        public DateTime CreatedAt { get; private set; } 

        public DisasterZone(int zoneId, string disasterType, string location, int severity)
        {
            ZoneId = zoneId;
            DisasterType = disasterType;
            Location = location;
            Severity = severity;
            ThreatLevel = severity * 10;
            ResponseDispatched = false;
            CreatedAt = DateTime.Now;

        }

        public void IncreaseThreat(int amount)
        {
            ThreatLevel += amount;  

            if (ThreatLevel > 100)
            {
                ThreatLevel = 100;
            }
        }

        public void MarkAsResponded()
        {
            ResponseDispatched = true;
        }

        public override string ToString()
        {
            return $"Zone {ZoneId} | {DisasterType}" +
                   $"Location: {Location} | Severity: {Severity}/10" +
                   $"Threat: {ThreatLevel}" +
                   $"Responded: {ResponseDispatched}";
            
        }
    }
}
