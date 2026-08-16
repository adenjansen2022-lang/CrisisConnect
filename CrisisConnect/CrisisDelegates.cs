using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrisisConnect
{
    // Delegate used when a disaster zone reaches a critical threat level.
    internal delegate void EmergencyAlertHandler(
        int zoneId,
        string disasterType,
        string location,
        int threatLevel
    );

    // Delegate used when a rescue team's mission is completed.
    internal delegate void MissionStatusHandler(
        int teamId,
        string teamName,
        int zoneId,
        string status
    );
}
