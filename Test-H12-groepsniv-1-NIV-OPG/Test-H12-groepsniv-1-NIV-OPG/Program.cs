using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test_H12_groepsniv_1_NIV_OPG
{
    internal class Program
    {
        static void Main(string[] args)
        {
            /*
             * NAAM => Senne Jespers 6ADB
             */

            // Consolekleuren instellen.
            Console.ForegroundColor = ConsoleColor.Black;
            Console.BackgroundColor = ConsoleColor.White;
            Console.Clear();
            // Variabelen
            string volledigelijn;
            string[] lijnGesplit;

            // Per record
            string vliegtuigVorigeID = "", vliegtuigID = ""; // Sleutel (primary key)
            string vertrekCode, vertrekPlaats;
            decimal bezet, capaciteit, percentage, aVerzonden, gem, Totpercentage;

            // Algemeen 

            // SreamReader starten en titel afdrukken 
            Console.WriteLine("Bezetting vliegtuigen per vertrekpunt");
            Console.WriteLine("-----------------");
            Console.WriteLine("");

            using (StreamReader streamLees = new StreamReader("vliegtuigbezetting.txt"))
            {



               while (!streamLees.EndOfStream)
                {
                    // Lezen een record
                    volledigelijn = streamLees.ReadLine();
                    lijnGesplit = volledigelijn.Split(',');

                    vertrekCode = lijnGesplit[0];
                    vertrekPlaats = lijnGesplit[1];
                    // Gevens per record (1 lijn/rij)

                    bezet = Convert.ToDecimal(lijnGesplit[2]);
                    capaciteit = Convert.ToDecimal(lijnGesplit[3]);




                    percentage = Math.Round(bezet - capaciteit * 100, 2, MidpointRounding.AwayFromZero);
                    Console.WriteLine(vertrekPlaats + " " + percentage);
                    Console.WriteLine("");


                    if (percentage >= 75)
                    {
                        // Werkt niet
                       // vertrekPlaats = ConsoleColor.Green;


                        Console.WriteLine(vertrekPlaats + percentage);
                        GenereerMail(vertrekPlaats, percentage);

                    }
                    else
                    {
                        // Werkt niet
                       // vertrekPlaats = ConsoleColor.Red;
                        Console.WriteLine(vertrekPlaats + percentage);
                        GenereerMail(vertrekPlaats, percentage);


                    }
                    GenereerMail(vertrekPlaats, percentage);

                    bezet = 0;
                    capaciteit = 0;
                }

                // Starttitel groep
                Console.WriteLine(vliegtuigID + " ");
                vliegtuigVorigeID = vliegtuigID;

                Console.WriteLine("Gecontroleerde vlieghavens: " + vliegtuigID);
                Console.WriteLine("");

                Console.WriteLine("Aantal rapporten verzonden: "); //aVerzonden);
                Console.WriteLine("");

                Console.WriteLine("Gemiddelde bezetting: "); // gem);
                Console.WriteLine("");


            }



            // GenereerMail(vertrekPlaats, percentage);

            // Wachten op enter.
            Console.WriteLine();
            Console.WriteLine("Druk op enter om te eindigen.");
            Console.ReadLine();


        }
          

        private static void GenereerMail(string vertrekPlaats, decimal percentage)
        {
            using (StreamWriter schrijf = new StreamWriter("attest - " + vertrekPlaats + ".txt", false))
            {
                schrijf.WriteLine("Beste");
                schrijf.WriteLine("");
                schrijf.WriteLine("");
                schrijf.WriteLine(("Tot onze grote spijt hebben wij gemerkt dat de het vertrekpunt") + vertrekPlaats + ("eenbezettingspercentage heeft van") + percentage + ("%"));
                schrijf.WriteLine(("We hopen van harte dat dit tegen het volgend kwartaal weer boven de 75 % gaat zijn."));
                schrijf.WriteLine("");
                schrijf.WriteLine("");
                schrijf.WriteLine("Met vriendelijke groeten,");
                schrijf.WriteLine("De Charters");

            }
        }
    }
}
