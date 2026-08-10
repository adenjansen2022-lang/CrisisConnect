using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrisisConnect
{
    abstract class CrisisRescueSquad
    {
        public string teamName {  get; set; }
        public int disasterID { get; set; }
        public string status { get; set; }//is the sqaud availabel or are they being deployed are they busy with maintenace
        public string TypeOfVehicle { get; set; }
        public string location {  get; set; }

        public CrisisRescueSquad(string tn, int dID, string s, string tv,string l)
        {
            teamName = tn;
            disasterID = dID;
            status = s;
            TypeOfVehicle = tv;
            location = l;
        }

        public abstract string getTeamDetail();
    }
}
