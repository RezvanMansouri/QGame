/*
* Programmed by: Rezvan Rose Mansouri
* Revision History
* projected created: Assignment 2
* Rose Mansouri , 2021.25.10: Created
* Rose Mansouri , 2021.26.10  : Added code
* Rose Mansouri , 2021.26.10: Debugging complete
* Rose Mansouri , 2021.27.10: Comments added
*/



using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RMansouriQGame
{
    /// <summary>
    /// A  class that acts as a control panel.
    /// </summary>
    public partial class ControlPanelForm : Form
    {
        /// <summary>
        /// Default constructor of the MainForm class
        /// </summary>
        public ControlPanelForm()
        {
            InitializeComponent();
        }

        private void btnDesingForm_Click(object sender, EventArgs e)
        {
            DesignerForm newDesign = new DesignerForm();
            newDesign.Show();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnPlayForm_Click(object sender, EventArgs e)
        {
            PlayForm newPaly = new PlayForm();
            newPaly.Show();

        }
    }
}
