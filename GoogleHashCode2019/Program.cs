using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HashCode2019
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            /*
             *  Important Variables
             */
            int photoCountFromFile;
            List<Slide> finalSlideShow = new List<Slide>();
            List<Photo> allPhotos = new List<Photo>();
            List<Photo> verticalPhotos = new List<Photo>();

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
                    //Console.WriteLine(s);
                }

                // the first line is giving us indication on how the issue should be solved
                // extract it to own variables
                int[] inputParameters = fullInput[0].Split(' ').Select(x => int.Parse(x)).ToArray();

                // Read first line parameters
                photoCountFromFile = inputParameters[0];

                // Read all photos
                for (int i = 1; i <= photoCountFromFile; i++)
                {
                    string[] photoParameters = fullInput[i].Split(' ');
                    List<string> _tags = new List<string>();
                    for (int j = 2; j < photoParameters.Length; j++)
                    {
                        _tags.Add(photoParameters[j]);
                    }

                    bool _isHorizontal = true;

                    if (photoParameters[0] == "V") _isHorizontal = false;

                    Photo photo = new Photo(id: i - 1, isHorizontal: _isHorizontal, tags: _tags);

                    allPhotos.Add(photo);
                }


                List<Slide> unsortedSlides = new List<Slide>();

                unsortedSlides = GenerateSlides(allPhotos);

                finalSlideShow = ReorderSlides(unsortedSlides);

                WriteToFile(finalSlideShow, args[0] + ".out");
            }
        }


        public static List<Slide> ReorderSlides(List<Slide> unsortedSlides)
        {
            List<Slide> sortedSlides = new List<Slide>();

            sortedSlides.Add(unsortedSlides[0]);
            unsortedSlides.RemoveAt(0);

            while (unsortedSlides.Count > 0)
            {
                Slide prevSlide = sortedSlides.Last();
                int unsortedIdWithMaxValue = 0;
                int maxValue = 0;

                for (int i = 0; i < unsortedSlides.Count && i < 2000; i++)
                {
                    Slide tmpSlide = unsortedSlides[i];
                    int tmpValue = CalculatePoints(prevSlide, tmpSlide);
                    if (tmpValue >= maxValue)
                    {
                        maxValue = tmpValue;
                        unsortedIdWithMaxValue = i;
                    }
                }

                sortedSlides.Add(unsortedSlides[unsortedIdWithMaxValue]);
                unsortedSlides.RemoveAt(unsortedIdWithMaxValue);
            }

            return sortedSlides;

        }

        public static List<Slide> GenerateSlides(List<Photo> photos)
        {

            List<Slide> slides = new List<Slide>();
            List<Photo> verticalPhotos = new List<Photo>();

            foreach (var p in photos)
            {
                if (p.isHorizontal)
                {
                    slides.Add(new Slide(p));
                }
                else
                {
                    verticalPhotos.Add(p);
                    if (verticalPhotos.Count == 2)
                    {
                        slides.Add(new Slide(verticalPhotos[0], verticalPhotos[1]));
                        verticalPhotos.Clear();
                    }
                }
            }

            return slides;
        }

        public static void WriteToFile(List<Slide> slideShow, string path)
        {
            string outputFile = path;
            if (!File.Exists(outputFile))
            {
                var myFile = File.Create(outputFile);
                myFile.Close();
            }

            StreamWriter sr = new StreamWriter(outputFile);

            sr.WriteLine(slideShow.Count);

            // create outputfile as defined in the example
            foreach (var slide in slideShow)
            {
                string outStr = "";
                foreach (var id in slide.ids)
                {
                    outStr = outStr + id.ToString() + " ";
                }
                sr.WriteLine(outStr);
            }

            sr.Close();
        }

        public static int CalculatePoints(Slide prevSlide, Slide newSlide)
        {
            int countOnlyPrev = 0;
            int countOnlyNew = 0;
            int countBoth = 0;

            countOnlyPrev = prevSlide.tags.Except(newSlide.tags).Count();
            countBoth = prevSlide.tags.Count - countOnlyPrev;
            countOnlyNew = newSlide.tags.Count - countBoth;

            if (countBoth < countOnlyNew && countBoth < countOnlyPrev) return countBoth;
            if (countOnlyNew < countOnlyPrev) return countOnlyNew;
            return countOnlyPrev;
        }
    }
}