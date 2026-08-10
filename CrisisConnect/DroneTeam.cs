using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CrisisConnect
{
    //INHERITANCE AND POLYMORPHISM AND ENCAPSULATION
    internal class DroneTeam:CrisisRescueSquad, iDispatchAndRecall, iStatusReport
    {
        private int droneCount;
        public int DroneCount 
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

        private int droneBatteryLevelPercentage;//private because we should not be able to change the battery life as is drains on its own

        public int DroneBatteryPercent
        {
            get { return droneBatteryLevelPercentage; }
            set
            {
                if (value > 0 || value < 100)
                {
                    throw new ArgumentException("Bettery percentage level is invlaide enter a number between 0 and 100");
                }
                droneBatteryLevelPercentage = value;
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

        public void dispatch(string targetLocation)
        {
            if(droneBatteryLevelPercentage < 20) 
            {
                throw new InvalidOperationException($"Cannot dispatch Drone Team '{teamName}'. Battery too low ({droneBatteryLevelPercentage}%).");
            }
            status = "Deployed";
            location = targetLocation;
            Console.WriteLine($"'DISPATCH' Drone Team for '{teamName}' launched toward {targetLocation}.");
        }

        public void recall()
        {
            status = "Available";
            Console.WriteLine($"'RECALL' Drone Team for '{teamName}' returning to charging dock.");
        }

        public string Report()
        {
            return $"Drone Team '{teamName}' - Drones Active: {droneCount} | Battery: {droneBatteryLevelPercentage}%";
        }
    }
}
