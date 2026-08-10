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
        public int disasterID { get; set; }
        public string status { get; set; }//ergency: low, medium or high proority
        public string TypeOfVehicle { get; set; }
        public string location {  get; set; }

        protected CrisisRescueSquad(string tn, int dID, string s, string tv,string l)
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
