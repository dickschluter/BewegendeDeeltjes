using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BewegendeDeeltjes
{
    class Botsing
    // representeert een botsing tussen een deeltje en een ander deeltje of wand
    {
        public static bool operator <(Botsing botsing1, Botsing botsing2)
        {
            return botsing1.Tijd < botsing2.Tijd;
        }

        public static bool operator >(Botsing botsing1, Botsing botsing2)
        {
            return botsing1.Tijd > botsing2.Tijd;
        }

        public float Tijd { get; private set; }
        public Deeltje Deeltje { get; private set; }
        public DockStyle WandType { get; private set; }
        public Deeltje AnderDeeltje { get; private set; }
        public Obstakel Obstakel { get; private set; }
        public ContentAlignment Orientatie { get; private set; }
        
        public int AantalBotsingenDeeltje { get; private set; }
        public int AantalBotsingenAnderDeeltje { get; private set; }

        public bool IsGeldig
        {
            get
            {
                if (Deeltje.AantalBotsingen > AantalBotsingenDeeltje)
                    return false;
                if (AnderDeeltje != null && AnderDeeltje.AantalBotsingen > AantalBotsingenAnderDeeltje)
                    return false;
                return true;
            }
        }

        public Botsing(float tijd, Deeltje deeltje, DockStyle wandtype)
        {
            Tijd = tijd;
            Deeltje = deeltje;
            WandType = wandtype;
            AantalBotsingenDeeltje = deeltje.AantalBotsingen;
        }

        public Botsing(float tijd, Deeltje deeltje, Deeltje anderDeeltje)
        {
            Tijd = tijd;
            Deeltje = deeltje;
            AnderDeeltje = anderDeeltje;
            AantalBotsingenDeeltje = deeltje.AantalBotsingen;
            AantalBotsingenAnderDeeltje = anderDeeltje.AantalBotsingen;
        }

        public Botsing(float tijd, Deeltje deeltje, Obstakel obstakel, ContentAlignment orientatie)
        {
            Tijd = tijd;
            Deeltje = deeltje;
            Obstakel = obstakel;
            Orientatie = orientatie;
            AantalBotsingenDeeltje = deeltje.AantalBotsingen;
        }
    }
}
