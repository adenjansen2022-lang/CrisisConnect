using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrisisConnect
{
    //INHERITANCE AND POLYMORPHISM

    internal class SpecalistTeam:CrisisRescueSquad
    {
        public string Typespecialist {  get; set; }
        public int specialistCount {  get; set; }

        public SpecalistTeam(string tn, int dID, string s, string tv,string l, string sp, int spC) : base(tn, dID, s,tv, l)
        {
            Typespecialist = sp;
            specialistCount = spC;
        }
        //require a specialist team incase we have fire, floods or road accident, clean up squad or rock slides
        public override string getTeamDetail()
        {
            return $"'RESCUE SQUAD' ID: {disasterID} | Specailist : {Typespecialist} | Number of specialsit: {specialistCount} | Status: {status} | Location: {location} | Name: {teamName} ";
        }

        
    }
}
