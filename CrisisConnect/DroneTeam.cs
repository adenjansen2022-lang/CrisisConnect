using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrisisConnect
{
    internal class DroneTeam:CrisisRescueSquad
    {
        public int droneCount {  get; set; }
        public int droneBatteryLevelPercentage { get; set; }

        public DroneTeam(string tn, int dID, string s, string tv, string l,int dc, int dblp):base(tn, dID, s, tv, l)
        {
            droneCount = dc;
            droneBatteryLevelPercentage = dblp;
        }

        public override string getTeamDetail()
        {
            return $"'DRONE TEAM' ID: {disasterID} | Number of drones : {droneCount} | drone battery %: {droneBatteryLevelPercentage} | Status: {status} | Location: {location} | Name: {teamName}";
        }
    }
}
