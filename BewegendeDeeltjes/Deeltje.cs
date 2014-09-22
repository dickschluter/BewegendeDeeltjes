using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BewegendeDeeltjes
{
    class Deeltje
    {
        public int Radius { get; private set; }
        public int Massa { get; private set; }
        public float Px { get; set; } // positie
        public float Py { get; set; }
        public float Vx { get; set; } // snelheid
        public float Vy { get; set; }
        public float Elasticiteit { get; private set; } // proportie behouden snelheid bij botsing
        public int AantalBotsingen { get; set; }

        // hulpvariabelen t.b.v. tekenen
        private readonly Brush kleurpen;
        private readonly int doorsnede;

        public Deeltje(int radius, float px, float py, float vx, float vy, Brush pen, float elasticiteit = 1)
        {
            Radius = radius;
            Massa = radius * radius * radius;
            Px = px;
            Py = py;
            Vx = vx;
            Vy = vy;
            Elasticiteit = elasticiteit;
            kleurpen = pen;
            doorsnede = 2 * radius;
        }

        public void Tekenen(Graphics g)
        {
            g.FillEllipse(kleurpen, Px - Radius, Py - Radius, doorsnede, doorsnede);
        }

        public void LineaireVerplaatsing()
        {
            Px += Vx;
            Py += Vy;
        }

        public float TijdTotWand(out DockStyle wandtype)
        {
            float dtHorizontaal = float.PositiveInfinity;
            if (Vx < 0)
                dtHorizontaal = (Radius - Px) / Vx;
            else if (Vx > 0)
                dtHorizontaal = (Veld.Breedte - Radius - Px) / Vx;

            float dtVerticaal = float.PositiveInfinity;
            if (Vy < 0)
                dtVerticaal = (Radius - Py) / Vy;
            else if (Vy > 0)
                dtVerticaal = (Veld.Hoogte - Radius - Py) / Vy;

            if (dtHorizontaal < dtVerticaal)
            {
                wandtype = Vx < 0 ? DockStyle.Left : DockStyle.Right;
                return dtHorizontaal;
            }
            wandtype = Vy < 0 ? DockStyle.Top : DockStyle.Bottom;
            return dtVerticaal;
        }

        public void BotstMetWand(DockStyle wandtype, float tijdBotsing, float huidigeTijd)
        {
            switch (wandtype)
            {
                case DockStyle.Left:
                    Px = Radius;
                    Py += Vy * (tijdBotsing - huidigeTijd);
                    Vx = -Vx;
                    break;
                case DockStyle.Right:
                    Px = Veld.Breedte - Radius;
                    Py += Vy * (tijdBotsing - huidigeTijd);
                    Vx = -Vx;
                    break;
                case DockStyle.Top:
                    Py = Radius;
                    Px += Vx * (tijdBotsing - huidigeTijd);
                    Vy = -Vy;
                    break;
                case DockStyle.Bottom:
                    Py = Veld.Hoogte - Radius;
                    Px += Vx * (tijdBotsing - huidigeTijd);
                    Vy = -Vy;
                    break;
            }

            if (Elasticiteit < 1)
            {
                Vx *= Elasticiteit;
                Vy *= Elasticiteit;
            }

            AantalBotsingen++;
        }

        public float TijdTotAnderDeeltje(Deeltje ander)
        {
            if (this == ander)
                return float.PositiveInfinity;
            float dx = ander.Px - Px;
            float dy = ander.Py - Py;
            float dvx = ander.Vx - Vx;
            float dvy = ander.Vy - Vy;
            float dvdp = dx * dvx + dy * dvy;
            if (dvdp > 0)
                return float.PositiveInfinity;
            float dvdv = dvx * dvx + dvy * dvy;
            float dpdp = dx * dx + dy * dy;
            float sigma = Radius + ander.Radius;
            float d = dvdp * dvdp - dvdv * (dpdp - sigma * sigma);
            if (d < 0)
                return float.PositiveInfinity;

            return -(dvdp + (float)Math.Sqrt(d)) / dvdv;
        }

        public void BotstMetDeeltje(Deeltje ander, float tijdBotsing, float huidigeTijd)
        {
            // terug naar positie bij botsing
            float dt = tijdBotsing - huidigeTijd;
            Px += Vx * dt;
            Py += Vy * dt;
            ander.Px += ander.Vx * dt;
            ander.Py += ander.Vy * dt;
            // snelheden berekenen
            float dx = ander.Px - Px;
            float dy = ander.Py - Py;
            float dvx = ander.Vx - Vx;
            float dvy = ander.Vy - Vy;
            float dvdp = dx * dvx + dy * dvy;
            float sigma = Radius + ander.Radius;
            float J = 2 * Massa * ander.Massa * dvdp / ((Massa + ander.Massa) * sigma);
            float Jx = J * dx / sigma;
            float Jy = J * dy / sigma;
            Vx += Jx / Massa;
            Vy += Jy / Massa;
            ander.Vx -= Jx / ander.Massa;
            ander.Vy -= Jy / ander.Massa;
            if (Elasticiteit < 1)
            {
                Vx *= Elasticiteit;
                Vy *= Elasticiteit;
                ander.Vx *= Elasticiteit;
                ander.Vy *= Elasticiteit;
            }
            // tellers verhogen
            AantalBotsingen++;
            ander.AantalBotsingen++;
        }

        public float TijdTotObstakel(Obstakel obstakel, out ContentAlignment orientatie)
        {
            if (Radius <= obstakel.Permeabiliteit)
            {
                orientatie = ContentAlignment.MiddleCenter;
                return float.PositiveInfinity;
            }

            // criteria voor Px en Py zijn 1 radius te ruim om ontsnappingen door rekenfouten te voorkomen
            if (Vx > 0 && Px < obstakel.X1) // linker wand
            {
                float dt = (obstakel.X1 - Radius - Px) / Vx;
                float yBotsing = Py + Vy * dt;
                if (yBotsing < obstakel.Y1)
                {
                    dt = TijdTotHoek(obstakel.HoekLinksBoven);
                    if (dt < float.PositiveInfinity)
                    {
                        orientatie = ContentAlignment.TopLeft;
                        return dt;
                    }
                }
                else if (yBotsing > obstakel.Y2)
                {
                    dt = TijdTotHoek(obstakel.HoekLinksOnder);
                    if (dt < float.PositiveInfinity)
                    {
                        orientatie = ContentAlignment.BottomLeft;
                        return dt;
                    }
                }
                else
                {
                    orientatie = ContentAlignment.MiddleLeft;
                    return dt;
                }
            }
            else if (Vx < 0 && Px > obstakel.X2) // rechter wand
            {
                float dt = (obstakel.X2 + Radius - Px) / Vx;
                float yBotsing = Py + Vy * dt;
                if (yBotsing < obstakel.Y1)
                {
                    dt = TijdTotHoek(obstakel.HoekRechtsBoven);
                    if (dt < float.PositiveInfinity)
                    {
                        orientatie = ContentAlignment.TopRight;
                        return dt;
                    }
                }
                else if (yBotsing > obstakel.Y2)
                {
                    dt = TijdTotHoek(obstakel.HoekRechtsOnder);
                    if (dt < float.PositiveInfinity)
                    {
                        orientatie = ContentAlignment.BottomRight;
                        return dt;
                    }
                }
                else
                {
                    orientatie = ContentAlignment.MiddleRight;
                    return dt;
                }
            }

            if (Vy > 0 && Py < obstakel.Y1) // bovenwand
            {
                float dt = (obstakel.Y1 - Radius - Py) / Vy;
                float xBotsing = Px + Vx * dt;
                if (xBotsing < obstakel.X1)
                {
                    dt = TijdTotHoek(obstakel.HoekLinksBoven);
                    if (dt < float.PositiveInfinity)
                    {
                        orientatie = ContentAlignment.TopLeft;
                        return dt;
                    }
                }
                else if (xBotsing > obstakel.X2)
                {
                    dt = TijdTotHoek(obstakel.HoekRechtsBoven);
                    if (dt < float.PositiveInfinity)
                    {
                        orientatie = ContentAlignment.TopRight;
                        return dt;
                    }
                }
                else
                {
                    orientatie = ContentAlignment.TopCenter;
                    return dt;
                }
            }
            else if (Vy < 0 && Py > obstakel.Y2) // onderwand
            {
                float dt = (obstakel.Y2 + Radius - Py) / Vy;
                float xBotsing = Px + Vx * dt;
                if (xBotsing < obstakel.X1)
                {
                    dt = TijdTotHoek(obstakel.HoekLinksOnder);
                    if (dt < float.PositiveInfinity)
                    {
                        orientatie = ContentAlignment.BottomLeft;
                        return dt;
                    }
                }
                else if (xBotsing > obstakel.X2)
                {
                    dt = TijdTotHoek(obstakel.HoekRechtsOnder);
                    if (dt < float.PositiveInfinity)
                    {
                        orientatie = ContentAlignment.BottomRight;
                        return dt;
                    }
                }
                else
                {
                    orientatie = ContentAlignment.BottomCenter;
                    return dt;
                }
            }

            orientatie = ContentAlignment.MiddleCenter;
            return float.PositiveInfinity;
        }

        public float TijdTotHoek(Point hoek)
        {
            float dx = hoek.X - Px;
            float dy = hoek.Y - Py;
            float dvdp = -(dx * Vx + dy * Vy);
            if (dvdp > 0)
                return float.PositiveInfinity;
            float dvdv = Vx * Vx + Vy * Vy;
            float dpdp = dx * dx + dy * dy;
            float sigma = Radius + 1;
            float d = dvdp * dvdp - dvdv * (dpdp - sigma * sigma);
            if (d < 0)
                return float.PositiveInfinity;

            return -(dvdp + (float)Math.Sqrt(d)) / dvdv;
        }

        public void BotstMetObstakel(Obstakel obstakel, ContentAlignment orientatie, float tijdBotsing, float huidigeTijd)
        {
            switch (orientatie)
            {
                case ContentAlignment.MiddleLeft:
                    Px = obstakel.X1 - Radius;
                    Py += Vy * (tijdBotsing - huidigeTijd);
                    Vx = -Vx;
                    break;
                case ContentAlignment.MiddleRight:
                    Px = obstakel.X2 + Radius;
                    Py += Vy * (tijdBotsing - huidigeTijd);
                    Vx = -Vx;
                    break;
                case ContentAlignment.TopCenter:
                    Py = obstakel.Y1 - Radius;
                    Px += Vx * (tijdBotsing - huidigeTijd);
                    Vy = -Vy;
                    break;
                case ContentAlignment.BottomCenter:
                    Py = obstakel.Y2 + Radius;
                    Px += Vx * (tijdBotsing - huidigeTijd);
                    Vy = -Vy;
                    break;
                case ContentAlignment.TopLeft:
                    BotstMetHoek(obstakel.HoekLinksBoven, tijdBotsing, huidigeTijd);
                    break;
                case ContentAlignment.BottomLeft:
                    BotstMetHoek(obstakel.HoekLinksOnder, tijdBotsing, huidigeTijd);
                    break;
                case ContentAlignment.TopRight:
                    BotstMetHoek(obstakel.HoekRechtsBoven, tijdBotsing, huidigeTijd);
                    break;
                case ContentAlignment.BottomRight:
                    BotstMetHoek(obstakel.HoekRechtsOnder, tijdBotsing, huidigeTijd);
                    break;
            }

            if (Elasticiteit < 1)
            {
                Vx *= Elasticiteit;
                Vy *= Elasticiteit;
            }

            AantalBotsingen++;
        }

        public void BotstMetHoek(Point hoek, float tijdBotsing, float huidigeTijd)
        {
            // terug naar positie bij botsing
            float dt = tijdBotsing - huidigeTijd;
            Px += Vx * dt;
            Py += Vy * dt;
            // snelheden berekenen
            float dx = hoek.X - Px;
            float dy = hoek.Y - Py;
            float dvdp = -(dx * Vx + dy * Vy);
            float sigma = Radius + 1;
            float J = 2 * Massa * dvdp / sigma;
            float Jx = J * dx / sigma;
            float Jy = J * dy / sigma;
            Vx += Jx / Massa;
            Vy += Jy / Massa;
        }
    }
}
