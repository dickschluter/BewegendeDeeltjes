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
    public partial class Form1 : Form
    {
        Veld veld;
        List<RadioButton> radioButtons = new List<RadioButton>();
        int selectie; // geselecteerde radioButton
        
        public Form1()
        {
            InitializeComponent();

            veld = new Veld(panelAnimatie.Width, panelAnimatie.Height);
            panelAnimatie.Controls.Add(veld);

            string[] opties = { "Random", "Groot en klein", "Diffusie", "Semipermeabel", "Poolafstoot" };
            for (int i = 0; i < 5; i++)
            {
                RadioButton radioButton = new RadioButton();
                radioButton.Location = new Point(80, 180 + 30 * i);
                radioButton.Tag = i;
                radioButton.Text = opties[i];
                radioButton.CheckedChanged += new EventHandler(radioButton_CheckedChanged);
                radioButtons.Add(radioButton);
                panelControls.Controls.Add(radioButton);
            }
            radioButtons[0].Checked = true;

            SizeChanged += new EventHandler(Form1_SizeChanged);
        }

        void radioButton_CheckedChanged(object sender, EventArgs e)
        {
            foreach (RadioButton radioButton in radioButtons)
                if (radioButton.Checked)
                    selectie = (int)radioButton.Tag;
            panelParameters.Enabled = selectie == 0;
        }

        void Form1_SizeChanged(object sender, EventArgs e)
        {
            veld.Stop();
            Veld.Breedte = panelAnimatie.Width;
            Veld.Hoogte = panelAnimatie.Height;
        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            Tuple<List<Obstakel>, List<Deeltje>> objecten = null;

            switch (selectie)
            {
                case 1:
                    objecten = ObjectenGenerator.GrootEnKlein();
                    veld.Start(objecten);
                    break;
                case 2:
                    objecten = ObjectenGenerator.Diffusie();
                    veld.Start(objecten);
                    break;
                case 3:
                    objecten = ObjectenGenerator.SemiPermeabel();
                    veld.Start(objecten);
                    break;
                case 4:
                    objecten = ObjectenGenerator.PoolAfstoot();
                    veld.Start(objecten, true);
                    break;
                default:
                    objecten = ObjectenGenerator.Random(
                        (int)numericUpDownAantal.Value, (int)numericUpDownRadius.Value, comboBoxKleur.SelectedIndex,
                        (int)numericUpDownAantal2.Value, (int)numericUpDownRadius2.Value, comboBoxKleur2.SelectedIndex);
                    veld.Start(objecten);
                    break;
            }
        }

        private void buttonBevriezen_Click(object sender, EventArgs e)
        {
            veld.Bevriezen();
        }

        private void buttonStop_Click(object sender, EventArgs e)
        {
            veld.Stop();
        }

        private void checkBoxSlowMotion_CheckedChanged(object sender, EventArgs e)
        {
            veld.veranderInterval(checkBoxSlowMotion.Checked);
        }
    }
}
