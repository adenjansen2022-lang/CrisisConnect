using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CrisisConnect
{
    internal class Program
    {
        static void Main(string[] args)
        {
            /*
            RESOURCE SQUAD:
                team name
                disaster id
                severity
                status
                vehicle type
                location
                    methods:
                        getTeamDetail
                        dispath
                        recall
                        

            Medical team:
                Number of doctors
                Number of ambulances
                    methods:
                        getTeamDetail
                        dispath
                        recall
                        report

            Specalist team:
                type of speclaist
                number of specailist
                    methods:
                        getTeamDetail
                        dispath
                        recall

            Drone team:
                Drone count
                number of drones
                    methods:
                        getTeamDetail
                        dispath
                        recall
                        report
         */

            List<CrisisRescueSquad> team = new List<CrisisRescueSquad>();

            List<DisasterZone> zones = new List<DisasterZone>();

            object zoneLock = new object();
            SystemLogger logger = new SystemLogger();
            CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();

            EmergencyGenerator emergencyGenerator = new EmergencyGenerator(zones, zoneLock, logger);

            DisasterZoneMonitor zoneMonitor = new DisasterZoneMonitor(zones, zoneLock, logger);

            DispatchProcessor dispatchProcessor = new DispatchProcessor(logger);

            // EVENT SUBSCRIPTIONS
            zoneMonitor.OnCriticalAlertTriggered += HandleCriticalAlert;
            dispatchProcessor.OnMissionCompleted += HandleMissionCompleted;

            Task emergencyTask = emergencyGenerator.RunAsync(cancellationTokenSource.Token);

            Task monitoringTask = zoneMonitor.RunAsync(cancellationTokenSource.Token);

            bool running = true; //this will continue to display the menu until the user exits

            while (running)
            {
                Console.Clear();
                Console.WriteLine("\n===============================================================");
                Console.WriteLine("                      CrisisConnect Menu                       ");
                Console.WriteLine("===============================================================");
                Console.WriteLine("Enter 1 to add a team (CREATE): ");
                Console.WriteLine("Enter 2 to view all teams (READ): ");
                Console.WriteLine("Enter 3 to upadate a team (UPDATE): ");
                Console.WriteLine("Enter 4 to remove a team (DELETE): ");
                Console.WriteLine("Enter 5 to dispatch or recall a team (INTERFACE: iDispatchAndRecall): ");
                Console.WriteLine("Enter 6 to generate team status reports (INTERFACE: iStatusReport): ");
                Console.WriteLine("Enter 7 to view active disaster zones");
                Console.WriteLine("Enter 8 to exit system: ");


                try
                {
                    int menuinput = Convert.ToInt32(Console.ReadLine());

                    switch (menuinput)
                    {
                        case 1:
                            AddTeam(team);
                            break;
                        case 2:
                            ReviewTeam(team);
                            break;
                        case 3:
                            UpdateTeam(team);
                            break;
                        case 4:
                            DeleteTeam(team);
                            break;
                        case 5:
                            DispatchOrRecallTeam(
                                team,
                                zones,
                                zoneLock,
                                dispatchProcessor,
                                cancellationTokenSource.Token
                            );
                            break;
                        case 6:
                            GenerateStatusReports(team);
                            break;
                        case 7:
                            DisplayDisasterZones(zones, zoneLock);
                            break;
                        case 8:
                            running = false;
                            cancellationTokenSource.Cancel();

                            try
                            {
                                Task.WaitAll(new Task[] { emergencyTask, monitoringTask });
                            }
                            catch (AggregateException)
                            {

                            }

                            logger.Log("CrisisConnect Shutdown");
                            break;
                        default:
                            Console.WriteLine("TRY AGAIN!!, You have entered an invlaid number that is not specified in the menu");
                            break;
                    }
                }
                catch (FormatException)
                {
                    Console.WriteLine("'ERROR: input a numeric value");
                    Console.WriteLine("\nPress enter to try again");
                    Console.ReadLine();
                }
            }

            try
            {
                Task.WaitAll(new Task[] { emergencyTask, monitoringTask });
            }
            catch (AggregateException)
            {

            }
        }

        static void DisplayDisasterZones(List<DisasterZone> zones, object zoneLock)
        {
            Console.Clear();
            Console.WriteLine("================================Active Disaster Zones================================");

            lock (zoneLock)
            {
                if (zones.Count == 0)
                {
                    Console.WriteLine("No active disaster zones at the moment.");
                }
                else
                {
                    foreach (DisasterZone zone in zones)
                    {
                        Console.WriteLine(zone);
                    }
                }
            }

            Console.WriteLine("Press enter to return to the main menu");
            Console.ReadLine();
        }





        static void AddTeam(List<CrisisRescueSquad> team)
        {
            Console.Clear();
            Console.WriteLine("Enter 1 to add a medical team: ");
            Console.WriteLine("Enter 2 to add a Specailist sqaud: ");
            Console.WriteLine("Enter 3 to add a Drone team: ");

            try
            {
                int addinput = Convert.ToInt32(Console.ReadLine());

                if (addinput == 1)
                {
                    Console.WriteLine("Enter the Medical team name:");
                    string MeidcalTeamName = Console.ReadLine();

                    Console.WriteLine("Enter the Medical team id:");
                    int MeidcalTeamid = Convert.ToInt32(Console.ReadLine());
                    if (team.Any(t => t.disasterTeamID == MeidcalTeamid))//this arrow function makes sure that when ids are entred there arent any duplicates
                    {
                        throw new ArgumentException($"[ERROR] A team with ID '{MeidcalTeamid}' already exists in CrisisConnect!");
                    }

                    Console.WriteLine("Enter level of severity [low, medium or high]:");
                    string MeidcalTeamseverity = Console.ReadLine();

                    Console.WriteLine("Enter the Medical team status [depolyed, active, maintenance or in training]:");
                    string MeidcalTeamStatus = Console.ReadLine();

                    Console.WriteLine("Enter the Medical team way of transportation:");
                    string MeidcalTeamVehicle = Console.ReadLine();

                    Console.WriteLine("Enter the crisis location:");
                    string MeidcalTeamlocation = Console.ReadLine();

                    Console.WriteLine("Enter the number of doctors (0-20):");
                    int MeidcalTeamNumDoc = Convert.ToInt32(Console.ReadLine());

                    Console.WriteLine("Enter the number of ambulances (0-30):");
                    int MeidcalTeamNumAmb = Convert.ToInt32(Console.ReadLine());

                    MedicalTeam medicalTeam = new MedicalTeam(MeidcalTeamName, MeidcalTeamid, MeidcalTeamseverity, MeidcalTeamStatus, MeidcalTeamVehicle, MeidcalTeamlocation, MeidcalTeamNumDoc, MeidcalTeamNumAmb);
                    team.Add(medicalTeam);

                    Console.WriteLine("\n'SUCCESS' Medical Team created successfully!");
                    Console.WriteLine(medicalTeam.getTeamDetail());

                }
                else if (addinput == 2)
                {
                    Console.WriteLine("Enter the Specailaist sqaud name:");
                    string SpecialistSquadName = Console.ReadLine();

                    Console.WriteLine("Enter the Specailaist sqaud id:");
                    int SpecialistSquadid = Convert.ToInt32(Console.ReadLine());
                    if (team.Any(t => t.disasterTeamID == SpecialistSquadid))//this arrow function makes sure that when ids are entred there arent any duplicates
                    {
                        throw new ArgumentException($"[ERROR] A team with ID '{SpecialistSquadid}' already exists in CrisisConnect!");
                    }

                    Console.WriteLine("Enter level of severity [low, medium or high]:");
                    string SpecialistSquadSeverity = Console.ReadLine();

                    Console.WriteLine("Enter the Specailaist sqaud status [depolyed, active, maintenance or in training]:");
                    string SpecialistSquadStatus = Console.ReadLine();

                    Console.WriteLine("Enter the Specailaist sqaud way of transportation:");
                    string SpecialistSquadVehicle = Console.ReadLine();

                    Console.WriteLine("Enter the crisis location:");
                    string SpecialistSquadlocation = Console.ReadLine();

                    Console.WriteLine("Enter the type of Specailaists [e.g fire, floods, road accident, clean up or rock slides]:");
                    string SpecialistSquadTypeOfSpecailist = Console.ReadLine();

                    Console.WriteLine("Enter the number of Specailaists (0-10):");

                    int SpecialistSquadNumSpecailist = Convert.ToInt32(Console.ReadLine());
                    SpecalistTeam specalist = new SpecalistTeam(SpecialistSquadName, SpecialistSquadid, SpecialistSquadSeverity, SpecialistSquadStatus, SpecialistSquadVehicle, SpecialistSquadlocation, SpecialistSquadTypeOfSpecailist, SpecialistSquadNumSpecailist);
                    team.Add(specalist);

                    Console.WriteLine("\n'SUCCESS' Specialist Squad created successfully!");
                    Console.WriteLine(specalist.getTeamDetail());
                }
                else if (addinput == 3)
                {
                    Console.WriteLine("Enter the Drone team name:");
                    string DroneTeamName = Console.ReadLine();

                    Console.WriteLine("Enter the Drone team id:");
                    int DroneTeamid = Convert.ToInt32(Console.ReadLine());
                    if (team.Any(t => t.disasterTeamID == DroneTeamid))
                    {
                        throw new ArgumentException($"[ERROR] A team with ID '{DroneTeamid}' already exists in CrisisConnect!");
                    }

                    Console.WriteLine("Enter level of severity [low, medium or high]:");
                    string DroneTeamseverity = Console.ReadLine();

                    Console.WriteLine("Enter the Drone team status [depolyed, active, maintenance or in training]:");
                    string DroneTeamStatus = Console.ReadLine();

                    string DroneTeamVehicle = "";

                    Console.WriteLine("Enter the crisis location:");
                    string DroneTeamlocation = Console.ReadLine();

                    Console.WriteLine("Enter the number of drones:");
                    int DroneTeamNumDrones = Convert.ToInt32(Console.ReadLine());

                    Console.WriteLine("Enter the drone battery percentage:");
                    int DroneTeamBatteryPercentage = Convert.ToInt32(Console.ReadLine());

                    DroneTeam drone = new DroneTeam(DroneTeamName, DroneTeamid, DroneTeamseverity, DroneTeamStatus, DroneTeamVehicle, DroneTeamlocation, DroneTeamNumDrones, DroneTeamBatteryPercentage);
                    team.Add(drone);

                    Console.WriteLine("\n'SUCCESS' Drone Team created successfully!");
                    Console.WriteLine(drone.getTeamDetail());

                }
                else
                {
                    Console.WriteLine("not team can be added, Not a valid input");
                }
            }
            catch (FormatException)//Catches when uses enter an non numeric value on the Int32
            {
                Console.WriteLine("\nINPUT ERROR: Numeric values need to be enter");
            }
            catch (ArgumentException exc)//catches when a number is given but its not withint the given range
            {
                Console.WriteLine($"VALIDATION ERROR: {exc.Message}");
            }
            catch (Exception exc)//gets caught wiith unexpected errors like runnig out of system memory or if there is a null reference from a class(Mainly ensure no matter what happens the system wont crash)
            {
                Console.WriteLine($"SYSTEM ERROR: {exc.Message}");
            }
            Console.WriteLine("\nPress enter to return to menu...");
            Console.ReadLine();
        }

        static void ReviewTeam(List<CrisisRescueSquad> team)
        {
            Console.Clear();
            try
            {
                if (team.Count == 0)
                {
                    Console.WriteLine("No rescue teams are curretly registered");
                }
                else
                {
                    Console.WriteLine("Number of teams:");
                    Console.WriteLine(team.Count);
                    for (int i = 0; i < team.Count; i++)
                    {
                        Console.WriteLine($"Index:[{i}]: {team[i].getTeamDetail()}");
                    }
                }
            }
            catch (Exception exc)
            {
                Console.WriteLine($"'ERROR' Failed to display teams: {exc.Message}");
            }
            Console.WriteLine("\nPress enter to return to menu...");
            Console.ReadLine();
        }

        static void UpdateTeam(List<CrisisRescueSquad> team)
        {
            Console.Clear();
            Console.WriteLine("========================Update a Team============================");
            foreach (CrisisRescueSquad t in team)
            {
                Console.WriteLine(t.getTeamDetail());
            }
            Console.WriteLine("\nEnter Disater team ID to update: ");

            try
            {
                int searchID = Convert.ToInt32(Console.ReadLine());
                var foundTeam = team.FirstOrDefault(t => t.disasterTeamID == searchID);

                if (foundTeam == null)
                {
                    Console.WriteLine($"'ERROR' Team with ID '{searchID}' was not found.");
                }
                else
                {
                    Console.WriteLine($"\n found: {foundTeam.getTeamDetail()}");
                    Console.WriteLine("Press enter to keep existing values");

                    Console.WriteLine($"Enter New name for [{foundTeam.teamName}]:");
                    string newTeamName = Console.ReadLine();
                    if (!string.IsNullOrWhiteSpace(newTeamName)) //this returns a bool true or false stating if it is emtpy or not
                    {
                        foundTeam.teamName = newTeamName;
                    }

                    Console.WriteLine($"Enter New severity for [{foundTeam.LevelOfSeverity}]:");
                    string newSeverity = Console.ReadLine();
                    if (!string.IsNullOrWhiteSpace(newSeverity)) //this returns a bool true or false stating if it is emtpy or not
                    {
                        foundTeam.LevelOfSeverity = newSeverity;
                    }

                    Console.WriteLine($"Enter New status for [{foundTeam.status}]:");
                    string newTeamStatus = Console.ReadLine();
                    if (!string.IsNullOrWhiteSpace(newTeamStatus)) //this returns a bool true or false stating if it is emtpy or not
                    {
                        foundTeam.status = newTeamStatus;
                    }

                    Console.WriteLine($"Enter New vehicle for [{foundTeam.TypeOfVehicle}]:");
                    string newVehicle = Console.ReadLine();
                    if (!string.IsNullOrWhiteSpace(newVehicle)) //this returns a bool true or false stating if it is emtpy or not
                    {
                        foundTeam.TypeOfVehicle = newVehicle;
                    }

                    Console.WriteLine($"Enter New location for [{foundTeam.location}]:");
                    string newLocation = Console.ReadLine();
                    if (!string.IsNullOrWhiteSpace(newLocation)) //this returns a bool true or false stating if it is emtpy or not
                    {
                        foundTeam.location = newLocation;
                    }
                }
            }
            catch (FormatException)
            {
                Console.WriteLine("'ERROR', ID must be a valid numeric value");
            }
            catch (Exception exc)
            {
                Console.WriteLine($"'ERROR', upadte has failed: {exc.Message}");
            }
            Console.WriteLine("\nPress enter to return to menu...");
            Console.ReadLine();
        }

        static void DeleteTeam(List<CrisisRescueSquad> team)
        {
            Console.Clear();
            Console.WriteLine("========================Delete Team============================");
            foreach (CrisisRescueSquad t in team)
            {
                Console.WriteLine(t.getTeamDetail());
            }
            Console.Write("\nEnter Disaster Team ID to remove: ");

            try
            {
                int searchId = Convert.ToInt32(Console.ReadLine());
                var foundTeam = team.FirstOrDefault(t => t.disasterTeamID == searchId);//searches through all the teams for matching IDs get the first matching ID

                if (foundTeam == null)
                {
                    Console.WriteLine($"'ERROR' Team with ID '{searchId}' was not found.");
                }
                else if (foundTeam.status.Equals("deployed", StringComparison.OrdinalIgnoreCase))
                {
                    // Domain rule protection you cannot remove a team if it has already been deployed
                    Console.WriteLine($"'DENIED' Cannot remove '{foundTeam.teamName}' while actively deployed in the field!");
                }
                else
                {
                    team.Remove(foundTeam);
                    Console.WriteLine($"'SUCCESS' Team '{foundTeam.teamName}' (ID: {searchId}) has been removed.");
                }
            }
            catch (FormatException)
            {
                Console.WriteLine("'ERROR' ID must be a valid number.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"'ERROR' Removal failed: {ex.Message}");
            }

            Console.WriteLine("\nPress Enter to return to menu...");
            Console.ReadLine();

        }

        static void DispatchOrRecallTeam(
            List<CrisisRescueSquad> team,
            List<DisasterZone> zones,
            object zoneLock,
            DispatchProcessor dispatchProcessor,
            CancellationToken cancellationToken)
        {
            Console.Clear();

            Console.WriteLine(
                "========================Dispatch/Recall============================"
            );

            try
            {
                if (team.Count == 0)
                {
                    Console.WriteLine(
                        "No rescue teams are currently registered."
                    );
                }
                else
                {
                    foreach (CrisisRescueSquad t in team)
                    {
                        Console.WriteLine(t.getTeamDetail());
                    }

                    Console.Write("\nEnter Disaster Team ID: ");

                    int searchId = Convert.ToInt32(Console.ReadLine());

                    CrisisRescueSquad foundTeam =
                        team.FirstOrDefault(
                            t => t.disasterTeamID == searchId
                        );

                    if (foundTeam == null)
                    {
                        Console.WriteLine(
                            $"'ERROR' Team with ID '{searchId}' was not found."
                        );
                    }
                    else if (foundTeam is iDispatchAndRecall dispatchableTeam)
                    {
                        Console.WriteLine();
                        Console.WriteLine("Choose Action:");
                        Console.WriteLine("Enter 1 to Dispatch Team");
                        Console.WriteLine("Enter 2 to Recall Team");
                        Console.Write("Choice: ");

                        int userChoice =
                            Convert.ToInt32(Console.ReadLine());

                        if (userChoice == 1)
                        {
                            // Block teams that are not currently available.
                            if (foundTeam.status.Equals(
                                    "maintenance",
                                    StringComparison.OrdinalIgnoreCase) ||
                                foundTeam.status.Equals(
                                    "deployed",
                                    StringComparison.OrdinalIgnoreCase))
                            {
                                throw new ResourceUnavailableException(
                                    $"Team '{foundTeam.teamName}' cannot be dispatched " +
                                    $"because its current status is '{foundTeam.status}'."
                                );
                            }

                            DisasterZone selectedZone = null;

                            lock (zoneLock)
                            {
                                if (zones.Count == 0)
                                {
                                    Console.WriteLine(
                                        "No active disaster zones are currently available."
                                    );
                                }
                                else
                                {
                                    Console.WriteLine();
                                    Console.WriteLine(
                                        "=========== Active Disaster Zones ==========="
                                    );

                                    foreach (DisasterZone zone in zones)
                                    {
                                        if (!zone.ResponseDispatched)
                                        {
                                            Console.WriteLine(zone);
                                        }
                                    }

                                    Console.Write(
                                        "\nEnter the Zone ID to respond to: "
                                    );

                                    int zoneId =
                                        Convert.ToInt32(Console.ReadLine());

                                    selectedZone =
                                        zones.FirstOrDefault(
                                            z => z.ZoneId == zoneId &&
                                                 !z.ResponseDispatched
                                        );
                                }
                            }

                            if (selectedZone == null)
                            {
                                Console.WriteLine(
                                    "'ERROR' Active disaster zone was not found."
                                );
                            }
                            else
                            {
                                // High-severity incidents require a Specialist Team.
                                if (selectedZone.Severity >= 8 &&
                                    !(foundTeam is SpecalistTeam))
                                {
                                    throw new ResourceUnavailableException(
                                        $"Zone {selectedZone.ZoneId} has severity " +
                                        $"{selectedZone.Severity}/10. " +
                                        "A Specialist Team is required for high-severity incidents."
                                    );
                                }

                                // Medical emergencies require a Medical Team.
                                if (selectedZone.DisasterType.Equals(
                                        "Medical Emergency",
                                        StringComparison.OrdinalIgnoreCase) &&
                                    !(foundTeam is MedicalTeam))
                                {
                                    throw new ResourceUnavailableException(
                                        $"Zone {selectedZone.ZoneId} is a medical emergency. " +
                                        "A Medical Team must be dispatched."
                                    );
                                }

                                Console.WriteLine(
                                    $"\nDispatching '{foundTeam.teamName}' " +
                                    $"to Zone {selectedZone.ZoneId}..."
                                );

                                Task.Run(
                                    () => dispatchProcessor.DispatchAsync(
                                        foundTeam,
                                        selectedZone,
                                        cancellationToken
                                    )
                                );
                            }
                        }
                        else if (userChoice == 2)
                        {
                            dispatchableTeam.recall();
                        }
                        else
                        {
                            Console.WriteLine(
                                "'ERROR' Invalid input. Enter either 1 or 2."
                            );
                        }
                    }
                    else
                    {
                        Console.WriteLine(
                            $"'INFO' Team '{foundTeam.teamName}' " +
                            "does not support dispatch/recall."
                        );
                    }
                }
            }
            catch (ResourceUnavailableException ex)
            {
                Console.WriteLine();
                Console.WriteLine(
                    $"'RESOURCE UNAVAILABLE' {ex.Message}"
                );
            }
            catch (FormatException)
            {
                Console.WriteLine(
                    "'ERROR' IDs and choices must be numeric values."
                );
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine(
                    $"'DISPATCH DENIED' {ex.Message}"
                );
            }
            catch (Exception ex)
            {
                Console.WriteLine(
                    $"'ERROR' Command failed: {ex.Message}"
                );
            }

            Console.WriteLine(
                "\nPress Enter to return to menu..."
            );

            Console.ReadLine();
        }

        static void GenerateStatusReports(List<CrisisRescueSquad> team)
        {
            Console.Clear();
            Console.WriteLine("========================System Status Report============================");

            try
            {
                if (team.Count == 0)
                {
                    Console.WriteLine("No rescue teams are currently registered.");
                }
                else
                {
                    bool foundReportable = false;

                    foreach (CrisisRescueSquad squad in team)
                    {
                        if (squad is iStatusReport reportableUnit)
                        {
                            Console.WriteLine($"[REPORT] {reportableUnit.Report()}");
                            foundReportable = true;
                        }
                    }

                    if (!foundReportable)
                    {
                        Console.WriteLine(
                            "None of the registered teams support status reporting."
                        );
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(
                    $"'ERROR' Failed to generate reports: {ex.Message}"
                );
            }

            Console.WriteLine("\nPress Enter to return to menu...");
            Console.ReadLine();
        }

        static void HandleCriticalAlert(
            int zoneId,
            string disasterType,
            string location,
            int threatLevel)
        {
            lock (ConsoleLock.LockObject)
            {
                Console.WriteLine();
                Console.WriteLine("!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
                Console.WriteLine("         CRITICAL EMERGENCY ALERT");
                Console.WriteLine("!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
                Console.WriteLine($"Zone ID: {zoneId}");
                Console.WriteLine($"Disaster: {disasterType}");
                Console.WriteLine($"Location: {location}");
                Console.WriteLine($"Threat Level: {threatLevel}%");
                Console.WriteLine("Immediate response required!");
                Console.WriteLine("!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
            }
        }

        static void HandleMissionCompleted(
            int teamId,
            string teamName,
            int zoneId,
            string status)
        {
            lock (ConsoleLock.LockObject)
            {
                Console.WriteLine();
                Console.WriteLine("======================================");
                Console.WriteLine("         MISSION EVENT RECEIVED");
                Console.WriteLine("======================================");
                Console.WriteLine($"Team ID: {teamId}");
                Console.WriteLine($"Team: {teamName}");
                Console.WriteLine($"Zone: {zoneId}");
                Console.WriteLine($"Status: {status}");
                Console.WriteLine("======================================");
            }
        }
    }
}
