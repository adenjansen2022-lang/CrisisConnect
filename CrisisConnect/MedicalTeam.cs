using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CrisisConnect
{
    //INHERITANCE AND POLYMORPHISM AND ENCAPSULATION
    internal class MedicalTeam:CrisisRescueSquad,iDispatchAndRecall, iStatusReport
    {
        private int numDoc;
        private int numAmb;
        public int NumDoctors 
        {
            get { return numDoc; }
            set
            {
                if (value < 0 || value > 20)
                {
                    throw new ArgumentException("[Error], entered an invlaid number of doctors, you cant have negative number of doctors and no more than 20 doctors deployed");
                }
                numDoc = value;
            }
        }
        public int NumAmbulances 
        {
            get { return numAmb; }
            set
            {
                if (value < 0 || value > 30)
                {
                    throw new ArgumentException("[Error], entered an invlaid number of ambulnaces, you cant have negative number of ambulances and no more than 50 amblunaces deployed");
                }
                numAmb = value;
            }
        }

        public MedicalTeam(string tn, int dID, string los, string s,string tv, string l, int ND, int NA) : base(tn, dID, los, s,tv, l)
        {
            NumDoctors = NA;
            NumAmbulances = ND;
        }

        public override string getTeamDetail()
        {
            return $"'MEDICAL TEAM'  ID: {disasterTeamID} | Doctors : {NumDoctors} | Ambulances: {NumAmbulances} |  Status: {status} | Severity:{LevelOfSeverity} |Location: {location} | Name: {teamName} | Type of vehicle: {TypeOfVehicle} ";
        }

        public void dispatch(string targetLocation)
        {
            status = "Deployed";
            location = targetLocation;
            Console.WriteLine($"'DISPATCH' Medical Team '{teamName}' dispatched to {targetLocation} with {NumAmbulances} ambulances.");
        }

     
        public void recall()
        {
            status = "Available";
            Console.WriteLine($"'RECALL' Medical Team '{teamName}' has been recalled to base.");
        }

        public string Report()
        {
            return $"Medical Team '{teamName}' at {location} - Status: {status} (Active Personnel: {NumDoctors} Doctors)";
        }
    }
}
