using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrisisConnect
{
    //ABSTRACTION AND ENCAPSULATION
    abstract class CrisisRescueSquad
    {
        
        public string teamName {  get; set; }
        public int disasterTeamID { get; set; }
        public string LevelOfSeverity { get; set; }//ergency: low, medium or high proority
        public string status { get; set; }//deployed, active, maintenance, in training
        public string TypeOfVehicle { get; set; }
        public string location {  get; set; }

        protected CrisisRescueSquad(string tn, int dID,string los, string s, string tv,string l)
        {
            teamName = tn;
            disasterTeamID = dID;
            LevelOfSeverity = los;  
            status = s;
            TypeOfVehicle = tv;
            location = l;
        }

        public abstract string getTeamDetail();
    }
}
