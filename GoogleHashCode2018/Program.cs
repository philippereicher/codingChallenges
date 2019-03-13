using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HashCode
{
    class Program
    {
        public static int bonus = 0;

        public static int maxVehicles = 0;

        public static List<Ride> allRides = new List<Ride>();
        public static List<Vehicle> allVehicles = new List<Vehicle>();

        static void Main(string[] args)
        {
            /*
             *  Important Variables
             */
            int maxRows;
            int maxColumns;
            int maxRides;
            int maxSteps;

            /*
             *  Read not fantastic stuff out of file
             */
            var inputFile = "";
            if (args.Length == 0)
            {
                Console.WriteLine("Missing parameter...");
                Console.ReadKey();
                Environment.Exit(0);
            }
            else
            {
                inputFile = args[0];
                Console.WriteLine($"Input Path = {inputFile}");
            }

            if (File.Exists(inputFile))
            {
                //read full input file
                string[] fullInput = File.ReadAllLines(inputFile);

                foreach (var s in fullInput)
                {
                    Console.WriteLine(s);
                }

                // the first line is giving us indication on how the issue should be solved
                // extract it to own variables
                int[] inputParameters = fullInput[0].Split(' ').Select(x => int.Parse(x)).ToArray();

                // Read first line parameters
                maxRows = inputParameters[0];
                maxColumns = inputParameters[1];
                maxVehicles = inputParameters[2];
                maxRides = inputParameters[3];
                bonus = inputParameters[4];
                maxSteps = inputParameters[5];

                // Read all rides
                for (int i = 1; i <= maxRides; i++)
                {
                    int[] tmpIn = fullInput[i].Split(' ').Select(x => int.Parse(x)).ToArray();
                    Ride tmpRide = new Ride(tmpIn[0], tmpIn[2], tmpIn[1], tmpIn[3], tmpIn[4], tmpIn[5],i-1);
                    allRides.Add(tmpRide);
                }

                // create all vehicles
                for (int i = 0; i < maxVehicles; i++)
                {
                    Vehicle tmpVehicle = new Vehicle(0,0,0);
                    allVehicles.Add(tmpVehicle);
                }



                /*
                 * Do fantastic Stuff
                 */

                int normalPoints;
                int sortedPoints;
                //normalPoints = doCrazyStuffNormal();
                //resetVehicles();
                sortedPoints = doCrazyStuffSorted();
                //if (normalPoints > sortedPoints)
                //{
                //resetVehicles();
                //doCrazyStuffNormal();
                //}


                /*
                 * Write fantastic stuff to file
                 */
                WriteToFile(allVehicles, args[0]+".out");
            }
        }


        public static void WriteToFile(List<Vehicle> vehicles, string path)
        {
            string outputFile = path;
            if (!File.Exists(outputFile))
            {
                var myFile = File.Create(outputFile);
                myFile.Close();
            }

            StreamWriter sr = new StreamWriter(outputFile);


            // create outputfile as defined in the example
            // {ridecount} {rideIndex1} {rideIndex2} ....
            foreach (var vehicle in vehicles)
            {
                string outStr = vehicle.rides.Count + " ";
                foreach (var vehicleRide in vehicle.rides)
                {
                    outStr = outStr + vehicleRide.ToString() + " ";
                }
                sr.WriteLine(outStr);
            }



            //foreach (var s in data)
            //{
            //    sr.WriteLine(s);
            //}
            sr.Close();
        }

        public static int doCrazyStuffNormal()
        {
            int points = 0;
            foreach (Ride r in allRides)
            {
                List<Vehicle> markedVehicles = new List<Vehicle>();
                int minTime = int.MaxValue;
                foreach (Vehicle v in allVehicles)
                {
                    // ToDo: wastedTime auswerten
                    int tmpTime = v.CalcRide(r);
                    if (tmpTime >= 0 && tmpTime <= minTime)
                    {
                        if (tmpTime < minTime)
                        {
                            markedVehicles.Clear();
                        }
                        markedVehicles.Add(v);
                    }
                }


                if (markedVehicles.Count > 0)
                {
                    int minRideCounts = int.MaxValue;
                    Vehicle markedVehicle = null;
                    foreach (Vehicle v in markedVehicles)
                    {
                        int tmpRideCounts = v.rides.Count;
                        if (tmpRideCounts < minRideCounts)
                        {
                            minRideCounts = tmpRideCounts;
                            markedVehicle = v;
                        }
                    }

                    points += markedVehicle.DoRide(r);
                }
            }
            return points;
        }

        public static int doCrazyStuffSorted()
        {
            // ToDo: Reihenfolge variieren
            // ToDo: Score ermitteln und evtl. benutzen
            List<Ride> sortedRides = allRides.OrderBy(x => x.timeStart).OrderBy(x => x.xPosStart).ToList();

            int pointsSorted = 0;
            foreach (Ride r in sortedRides)
            {
                List<Vehicle> markedVehicles = new List<Vehicle>();
                int minTime = int.MaxValue;
                foreach (Vehicle v in allVehicles)
                {
                    // ToDo: wastedTime auswerten
                    int tmpTime = v.CalcRide(r);
                    if (tmpTime >= 0 && tmpTime <= minTime)
                    {
                        if (tmpTime < minTime)
                        {
                            markedVehicles.Clear();
                        }
                        markedVehicles.Add(v);
                    }
                }


                if (markedVehicles.Count > 0)
                {
                    int minRideCounts = int.MaxValue;
                    Vehicle markedVehicle = null;
                    foreach (Vehicle v in markedVehicles)
                    {
                        int tmpRideCounts = v.rides.Count;
                        if (tmpRideCounts < minRideCounts)
                        {
                            minRideCounts = tmpRideCounts;
                            markedVehicle = v;
                        }
                    }

                    pointsSorted += markedVehicle.DoRide(r);
                }
            }
            return pointsSorted;
        }

        public static void resetVehicles()
        {
            allVehicles.Clear();
            for (int i = 0; i < maxVehicles; i++)
            {
                Vehicle tmpVehicle = new Vehicle(0, 0, 0);
                allVehicles.Add(tmpVehicle);
            }
        }
    }
}
