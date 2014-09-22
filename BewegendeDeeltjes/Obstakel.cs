using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace BewegendeDeeltjes
{
    class Obstakel
    {
        public int X1 { get; private set; }
        public int Y1 { get; private set; }
        public int X2 { get; private set; }
        public int Y2 { get; private set; }
        public int Permeabiliteit { get; private set; } // max radius van deeltje dat kan passeren

        // hoek heeft fictieve radius = 1
        public Point HoekLinksBoven { get; private set; }
        public Point HoekLinksOnder { get; private set; }
        public Point HoekRechtsBoven { get; private set; }
        public Point HoekRechtsOnder { get; private set; }

        //hulpvariabelen t.b.v. tekenen
        private readonly int breedte;
        private readonly int hoogte;

        public Obstakel(int x1, int y1, int breedte, int hoogte, int permeabiliteit = 0)
        {
            X1 = x1;
            Y1 = y1;
            X2 = x1 + breedte;
            Y2 = y1 + hoogte;
            Permeabiliteit = permeabiliteit;

            HoekLinksBoven = new Point(X1 + 1, Y1 + 1);
            HoekLinksOnder = new Point(X1 + 1, Y2 - 1);
            HoekRechtsBoven = new Point(X2 - 1, Y1 + 1);
            HoekRechtsOnder = new Point(X2 - 1, Y2 - 1);

            this.breedte = breedte;
            this.hoogte = hoogte;
        }

        public void Tekenen(Graphics g)
        {
            g.FillRectangle(Brushes.DarkGray, X1, Y1, breedte, hoogte);
        }
    }
}
