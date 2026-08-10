using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrisisConnect
{
    //INHERITANCE AND POLYMORPHISM AND ENCAPSULATION
    internal class SpecalistTeam:CrisisRescueSquad, iDispatchAndRecall
    {
        public string Typespecialist {  get; set; }
        private int specCount;
        public int specialistCount 
        {
            get { return specCount; }
            set
            {
                if (value < 0 || value > 10)
                {
                    throw new ArgumentException("[Error], entered an invlaid number of specialists, you cant have negative number of specialists and no more than 10 speclailists deployed");
                }
                specCount = value;
            }
        }

        public SpecalistTeam(string tn, int dID,string los, string s, string tv,string l, string sp, int spC) : base(tn, dID,los, s,tv, l)
        {
            Typespecialist = sp;
            specialistCount = spC;
        }
        //require a specialist team incase we have fire, floods or road accident, clean up squad or rock slides
        public override string getTeamDetail()
        {
            return $"'RESCUE SQUAD' ID: {disasterTeamID} | Specailist : {Typespecialist} | Number of specialsit: {specialistCount} | Status: {status} | Severity:{LevelOfSeverity} | Location: {location} | Name: {teamName} ";
        }

        public void dispatch(string targetLocation)
        {
            
            status = "Deployed";
            location = targetLocation;
            Console.WriteLine($"'DISPATCH' {Typespecialist} Specailist Team  '{teamName}' launched toward {targetLocation}.");
        }

        public void recall()
        {
            status = "Available";
            Console.WriteLine($"'RECALL' {Typespecialist} Specalist Team '{teamName}' returning to charging dock.");
        }
    }
}
