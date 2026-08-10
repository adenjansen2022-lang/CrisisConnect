using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CrisisConnect
{
    internal class MedicalTeam:CrisisRescueSquad
    {
        public int NumDoctors {  get; set; }
        public int NumAmbulances {  get; set; }

        public MedicalTeam(string tn, int dID, string s,string tv, string l, int ND, int NA) : base(tn, dID, s,tv, l)
        {
            NumAmbulances = NA;
            NumDoctors = ND;
        }

        public override string getTeamDetail()
        {
            return $"'MEDICAL TEAM'  ID: {disasterID} | Doctors : {NumDoctors} | Ambulances: {NumAmbulances} |  Status: {status} | Location: {location} | Name: {teamName} | Type of vehicle: {TypeOfVehicle} ";
        }
    }
}
