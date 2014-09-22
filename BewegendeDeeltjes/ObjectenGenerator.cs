using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace BewegendeDeeltjes
{
    class ObjectenGenerator
    {
        private static Random random = new Random();
        private static Brush[] brushes = { Brushes.Black, Brushes.Red, Brushes.Green, Brushes.Blue };

        private static void maakRandomDeeltjes(List<Deeltje> deeltjes, int n, int xLaag, int yLaag, int xHoog, int yHoog, int radius, int kleur) // kleur == 4 => vier kleuren
        {
            // middelpunt deeltje minimaal een radius + 1 binnen de grenzen
            xLaag += radius + 1;
            yLaag += radius + 1;
            xHoog -= radius + 1;
            yHoog -= radius + 1;

            for (int i = 0; i < n; i++)
            {
                int px = 0, py = 0;
                bool overlap = true; // true als omhullende vierkant een afstand < 1 tot ander object heeft
                while (overlap)
                {
                    // kies nieuwe random positie
                    px = random.Next(xLaag, xHoog);
                    py = random.Next(yLaag, yHoog);
                    overlap = false;

                    // check op overlaps
                    foreach (Deeltje deeltje in deeltjes)
                    {
                        int somRadius = deeltje.Radius + radius + 1;
                        if (Math.Abs(deeltje.Px - px) < somRadius && Math.Abs(deeltje.Py - py) < somRadius)
                            overlap = true;
                    }
                }

                deeltjes.Add(new Deeltje(radius, px, py, random.Next(-4, 5), random.Next(-4, 5),
                    brushes[kleur == 4 ? i % 4 : kleur]));
            }
        }

        public static Tuple<List<Obstakel>, List<Deeltje>> Random(int n, int r, int kleur, int n2, int r2, int kleur2)
        {
            List<Obstakel> obstakels = new List<Obstakel>();
            List<Deeltje> deeltjes = new List<Deeltje>(200);
            maakRandomDeeltjes(deeltjes, n, 0, 0, Veld.Breedte, Veld.Hoogte, r, kleur);
            maakRandomDeeltjes(deeltjes, n2, 0, 0, Veld.Breedte, Veld.Hoogte, r2, kleur2);

            return Tuple.Create(obstakels, deeltjes);
        }

        public static Tuple<List<Obstakel>, List<Deeltje>> GrootEnKlein()
        {
            List<Obstakel> obstakels = new List<Obstakel>();
            List<Deeltje> deeltjes = new List<Deeltje>(201);

            deeltjes.Add(new Deeltje(20, Veld.Breedte / 2, Veld.Hoogte / 2, 0, 0, Brushes.Black));
            maakRandomDeeltjes(deeltjes, 200, 0, 0, Veld.Breedte, Veld.Hoogte, 4, 4);

            return Tuple.Create(obstakels, deeltjes);
        }

        public static Tuple<List<Obstakel>, List<Deeltje>> Diffusie()
        {
            List<Obstakel> obstakels = new List<Obstakel>();
            obstakels.Add(new Obstakel(Veld.Breedte / 2 - 10, 0, 20, Veld.Hoogte / 2 - 40));
            obstakels.Add(new Obstakel(Veld.Breedte / 2 - 10, Veld.Hoogte / 2 + 40, 20, Veld.Hoogte / 2 - 40));

            List<Deeltje> deeltjes = new List<Deeltje>(200);
            maakRandomDeeltjes(deeltjes, 100, 0, 0, Veld.Breedte / 2 - 10, Veld.Hoogte, 4, 1);
            maakRandomDeeltjes(deeltjes, 100, Veld.Breedte / 2 + 10, 0, Veld.Breedte, Veld.Hoogte, 4, 3);

            return Tuple.Create(obstakels, deeltjes);
        }

        public static Tuple<List<Obstakel>, List<Deeltje>> SemiPermeabel()
        {
            List<Obstakel> obstakels = new List<Obstakel>();
            obstakels.Add(new Obstakel(Veld.Breedte / 2 - 10, 0, 20, Veld.Hoogte, 6));

            List<Deeltje> deeltjes = new List<Deeltje>(200);
            maakRandomDeeltjes(deeltjes, 100, 0, 0, Veld.Breedte / 2 - 10, Veld.Hoogte, 6, 0);
            maakRandomDeeltjes(deeltjes, 100, Veld.Breedte / 2 + 10, 0, Veld.Breedte, Veld.Hoogte, 12, 1);

            return Tuple.Create(obstakels, deeltjes);
        }

        public static Tuple<List<Obstakel>, List<Deeltje>> PoolAfstoot()
        {
            List<Obstakel> obstakels = new List<Obstakel>();
            List<Deeltje> deeltjes = new List<Deeltje>(16);

            Point hoofd = new Point(Veld.Breedte / 4, Veld.Hoogte / 2);
            deeltjes.Add(new Deeltje(11, hoofd.X, hoofd.Y + random.Next(3), 60, random.Next(-2, 3), Brushes.Black, 0.8f));

            Point voet = new Point(Veld.Breedte * 3 / 4, Veld.Hoogte / 2);
            for (int i = 0; i < 5; i++)
                for (int j = 0; j < i + 1; j++)
                    deeltjes.Add(new Deeltje(11, voet.X + 18 * i + random.Next(3), voet.Y - 18 * i + 36 * j + random.Next(3), 0, 0, Brushes.Black, 0.8f));

            return Tuple.Create(obstakels, deeltjes);
        }
    }
}
