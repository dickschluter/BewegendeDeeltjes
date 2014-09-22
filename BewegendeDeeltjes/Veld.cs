using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BewegendeDeeltjes
{
    class Veld : UserControl
    {
        public static int Breedte { get; set; }
        public static int Hoogte { get; set; }

        List<Obstakel> lijstObstakels = new List<Obstakel>();
        List<Deeltje> lijstDeeltjes = new List<Deeltje>();
        MinPriorityQueue minPQ = new MinPriorityQueue(1000);
        Timer timerAnimatie = new Timer();

        int huidigeTijd; // aantal verlopen timertikken

        public Veld(int breedte, int hoogte)
        {
            Breedte = breedte;
            Hoogte = hoogte;

            Dock = DockStyle.Fill;
            DoubleBuffered = true;
            this.Paint += Veld_Paint;

            timerAnimatie.Interval = 30;
            timerAnimatie.Tick += new EventHandler(timer_Tick);
        }

        public void Start(Tuple<List<Obstakel>, List<Deeltje>> objecten, bool startInterval = false)
        {
            huidigeTijd = 0;

            lijstObstakels = objecten.Item1;
            lijstDeeltjes = objecten.Item2;
            
            minPQ.Legen();
            foreach (Deeltje deeltje in lijstDeeltjes)
                voorspelBotsingen(deeltje, null, null);

            Refresh();
            if (startInterval)
                System.Threading.Thread.Sleep(1000);
            timerAnimatie.Start();
        }

        private void voorspelBotsingen(Deeltje deeltje, Deeltje uitgeslotenDeeltje, Obstakel uitgeslotenObstakel)
            // zojuist geraakte deeltje of obstakel uitsluiten bij voorspelling
        {
            DockStyle wandtype;
            float dtWand = deeltje.TijdTotWand(out wandtype);
            if (dtWand < 1 << 16) // als deeltje stilstaat niet toevoegen
                minPQ.Invoegen(new Botsing(huidigeTijd + dtWand, deeltje, wandtype));

            foreach (Deeltje overigDeeltje in lijstDeeltjes)
            {
                if (overigDeeltje == deeltje || overigDeeltje == uitgeslotenDeeltje)
                    continue;
                float dt = deeltje.TijdTotAnderDeeltje(overigDeeltje);
                if (dt < dtWand) // tijden na botsing met wand niet relevant
                    minPQ.Invoegen(new Botsing(huidigeTijd + dt, deeltje, overigDeeltje));
            }

            ContentAlignment orientatie;
            foreach (Obstakel obstakel in lijstObstakels)
            {
                if (obstakel == uitgeslotenObstakel)
                    continue;
                float dtObstakel = deeltje.TijdTotObstakel(obstakel, out orientatie);
                if (dtObstakel < dtWand) // tijden na botsing met wand niet relevant
                    minPQ.Invoegen(new Botsing(huidigeTijd + dtObstakel, deeltje, obstakel, orientatie));
            }
        }

        public void Bevriezen()
        {
            if (timerAnimatie.Enabled)
                timerAnimatie.Stop();
            else
                timerAnimatie.Start();
        }

        public void Stop()
        {
            timerAnimatie.Stop();
            lijstObstakels.Clear();
            lijstDeeltjes.Clear();
            Refresh();
        }

        public void veranderInterval(bool slowMotion)
        {
            timerAnimatie.Interval = slowMotion ? 500 : 30;
        }

        void timer_Tick(object sender, EventArgs e)
            // mogelijke bron van fouten:
            // deeltjes betrokken bij botsing worden teruggebracht naar positie tijdens botsing,
            // andere deeltjes worden over hele tijdeenheid verplaatst
        {
            huidigeTijd++;
            
            // botsingvrije verplaatsingen afhandelen
            foreach (var deeltje in lijstDeeltjes)
                deeltje.LineaireVerplaatsing();
            // botsingen afhandelen
            Botsing botsing;
            while (minPQ.BevatItem && minPQ.Minimum <= huidigeTijd)
            {
                botsing = minPQ.WisMinimum();
                if (botsing.IsGeldig)
                {
                    Deeltje deeltje = botsing.Deeltje;

                    if (botsing.AnderDeeltje == null && botsing.Obstakel == null) // botsing met wand
                    {
                        deeltje.BotstMetWand(botsing.WandType, botsing.Tijd, huidigeTijd);
                        voorspelBotsingen(deeltje, null, null);
                    }

                    else if (botsing.Obstakel == null) // botsing met ander deeltje
                    {
                        deeltje.BotstMetDeeltje(botsing.AnderDeeltje, botsing.Tijd, huidigeTijd);
                        voorspelBotsingen(deeltje, botsing.AnderDeeltje, null);
                        voorspelBotsingen(botsing.AnderDeeltje, deeltje, null);
                    }

                    else // botsing met obstakel
                    {
                        deeltje.BotstMetObstakel(botsing.Obstakel, botsing.Orientatie, botsing.Tijd, huidigeTijd);
                        voorspelBotsingen(deeltje, null, botsing.Obstakel);
                    }
                }
            }

            Refresh();
        }

        void Veld_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.Clear(Color.White);
            foreach (var obstakel in lijstObstakels)
                obstakel.Tekenen(e.Graphics);
            foreach (var deeltje in lijstDeeltjes)
                deeltje.Tekenen(e.Graphics);
        }
    }
}
