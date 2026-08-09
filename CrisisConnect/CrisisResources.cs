using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrisisConnect
{
    abstract class CrisisResources
    {
        public string name {  get; set; }
        public int disasterID { get; set; }
        public string status { get; set; }//is the sqaud availabel or are they being deployed are they busy with maintenace
        public string location {  get; set; }

        public CrisisResources(string n, int dID, string s, string l)
        {
            name = n;
            disasterID = dID;
            status = s;
            location = l;
        }

        public abstract string getTeamDetail();
    }
}
