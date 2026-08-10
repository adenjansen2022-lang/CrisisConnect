using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrisisConnect
{
    internal class DroneTeam:CrisisRescueSquad
    {
        public int droneCount 
        {
            get { return droneCount; }
            set
            {
                if(droneCount < 0 || droneCount > 100)
                {
                    throw new ArgumentException("[Error], entered an invlaid number of drones, you cant have negative number of drones and no more than 100 drones deployed");
                }
                droneCount = value;
            }
        }
        private int droneBatteryLevelPercentage;

        public int DoneBatteryPercent
        {
            get { return droneBatteryLevelPercentage; }
            set
            {
                if( droneBatteryLevelPercentage > 0 || droneBatteryLevelPercentage < 100)
                {
                    throw new ArgumentException("Bettery percentage level is invlaide enter a number between 0 and 100");
                }
            }
        }

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
