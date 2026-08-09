using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CrisisConnect
{
    internal class MedicalTeam:CrisisResources
    {
        public int NumDoctors {  get; set; }
        public int NumAmbulances {  get; set; }

        public MedicalTeam(string n, int dID, string s, string l, int ND, int NA) : base(n, dID, s, l)
        {
            NumAmbulances = NA;
            NumDoctors = ND;
        }

        public override string getTeamDetail()
        {
            return $"Medical team ID: {disasterID} | Status: {status} | Location: {location} | Name: {name} | Doctors : {NumDoctors} | Ambulances: {NumAmbulances}";
        }
    }
}
