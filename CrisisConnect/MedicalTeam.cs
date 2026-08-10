using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CrisisConnect
{
    //INHERITANCE AND POLYMORPHISM AND ENCAPSULATION
    internal class MedicalTeam:CrisisRescueSquad,iDispatchAndRecall
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

        public MedicalTeam(string tn, int dID, string s,string tv, string l, int ND, int NA) : base(tn, dID, s,tv, l)
        {
            numDoc = NA;
            numAmb = ND;
        }

        public override string getTeamDetail()
        {
            return $"'MEDICAL TEAM'  ID: {disasterID} | Doctors : {numDoc} | Ambulances: {numAmb} |  Status: {status} | Location: {location} | Name: {teamName} | Type of vehicle: {TypeOfVehicle} ";
        }

        public void dispatch(string targetLocation)
        {

        }

        //recall is not being used but is still needed else it will throw an error if the method isnt called
        public void recall()
        {

        }
    }
}
