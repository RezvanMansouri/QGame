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
using System.IO;
using System.Collections;

namespace RMansouriQGame
{
    /// <summary>
    /// A Simple class that creates the Design of the game .
    /// </summary>
    public partial class DesignerForm : Form
    {
        // Declaring class variables 

        private Image greenBox = RMansouriQGame.Properties.Resources.green_box;
        private Image redBox = RMansouriQGame.Properties.Resources.red_box;
        private Image greenDoor = RMansouriQGame.Properties.Resources.green_door;
        private Image redDoor = RMansouriQGame.Properties.Resources.red_door;
        private Image wall = RMansouriQGame.Properties.Resources.Wall;

        private Image selectedImage;
        private int selectedImageNumber;

        const int WALL = 1;
        const int RED_DOOR = 2;
        const int RED_BOX = 4;
        const int GREEN_DOOR = 3;
        const int GREEN_BOX = 5;

        const int INIT_LEFT = 210;
        const int INIT_TOP = 128;
        const int WIDTH = 77;
        const int HEIGHT = 77;
        const int NEWX = 80;

        private int totalWalls = 0;
        private int totalDoors = 0;
        private int totalBoxes = 0;

        private int rows = 0;
        private int columns = 0;

        private ArrayList Tiles = new ArrayList();

        /// <summary>
        /// Default constructor of the MainForm class
        /// </summary>
        public DesignerForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// To add up the related elements of the game to report after saving
        /// </summary>
        void finalReport()
        {
            totalWalls = 0; totalDoors = 0; totalBoxes = 0;
            foreach (Tile t in Tiles)
            {
                if (t.Type == WALL)
                    totalWalls++;
                else if (t.Type == RED_DOOR)
                    totalDoors++;
                else if (t.Type == GREEN_DOOR)
                    totalDoors++;
                else if (t.Type == RED_BOX)
                    totalBoxes++;
                else if (t.Type == GREEN_BOX)
                    totalBoxes++;
            }
        }

        /// <summary>
        /// To save game information to the file
        /// </summary>
        /// <param name="fileName"> the name of chosen file for saving</param>
        /// <param name="numberOfRows"> the number of the rows in the game</param>
        /// <param name="numberOfCol"> the number of the Columns in the game </param>
        private void save(string fileName, int numberOfRows, int numberOfCol)
        {
            StreamWriter writer = new StreamWriter(fileName);

            // writing number of rows and columns
            writer.WriteLine(numberOfRows);
            writer.WriteLine(numberOfCol);
            // writing row, column, type for each tile 
            foreach (Tile t in Tiles)
            {
                writer.WriteLine(t.Row);
                writer.WriteLine(t.Column);
                writer.WriteLine(t.Type);
            }
            writer.Close();
        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            try
            {
                //get the rows and columns from inputs
                rows = int.Parse(txbRows.Text);
                columns = int.Parse(txbColumns.Text);
            }
            catch (Exception)
            {
                MessageBox.Show("Please provide valid data for rows and columns (Both must be integer)", "QGame", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            //starting point to put tiles
            int startX = INIT_LEFT;
            int startY = INIT_TOP;

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    //instead of each picture box add a new tile which is a child of a Picturebox
                    Tile tileBox = new Tile();

                    tileBox.Left = startX;
                    tileBox.Top = startY;
                    tileBox.Width = WIDTH;
                    tileBox.Height = HEIGHT;
                    tileBox.BorderStyle = BorderStyle.Fixed3D;

                    //Add to the design
                    this.Controls.Add(tileBox);

                    //save the information for each tilebox inside it's properties
                    tileBox.Row = i;
                    tileBox.Column = j;

                    //add an event handler to each tilebox
                    tileBox.Click += tileBox_Click;

                    //add the tilebox inside an arraylist
                    Tiles.Add(tileBox);

                    startX += NEWX;
                }
                startY += NEWX;
                startX = INIT_LEFT;
            }
        }

        private void tileBox_Click(object sender, EventArgs e)
        {
            Tile tileBox = (Tile)sender;

            //put the selectedImage from toolbar inside the tilebox image property
            tileBox.Image = selectedImage;
            tileBox.SizeMode = PictureBoxSizeMode.StretchImage;
            //put the related number inside tilebox Type property
            tileBox.Type = selectedImageNumber;
        }

        private void btn_Action(object sender, EventArgs e)
        {
            if (sender is Button)
            {
                Button selectedButton = (Button)sender;
                //a switch to show which tools has been clicked and assine related Image and number to it
                switch (selectedButton.Name)
                {
                    case "btnWall":
                        selectedImage = wall;
                        selectedImageNumber = WALL;
                        break;
                    case "btnRedDoor":
                        selectedImage = redDoor;
                        selectedImageNumber = RED_DOOR;
                        break;
                    case "btnGreenDoor":
                        selectedImage = greenDoor;
                        selectedImageNumber = GREEN_DOOR;
                        break;
                    case "btnRedBox":
                        selectedImage = redBox;
                        selectedImageNumber = RED_BOX;
                        break;
                    case "btnGreenBox":
                        selectedImage = greenBox;
                        selectedImageNumber = GREEN_BOX;
                        break;
                    case "btnNone":
                        selectedImage = null;
                        selectedImageNumber = 0;
                        break;
                    default:
                        break;
                }
            }
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dlgSave.DefaultExt = "qGame";
            DialogResult dialogResult = dlgSave.ShowDialog();

            switch (dialogResult)
            {
                case DialogResult.None:
                    break;
                case DialogResult.OK:
                    try
                    {
                        //get the file name from dialoge box
                        string fileName = dlgSave.FileName;
                        //save the information inside the file
                        this.save(fileName, rows, columns);

                        //add to the final Report
                        finalReport();

                        MessageBox.Show($"File saved successfully.\nTotal number of walls: " +
                            $"{totalWalls}\nTotal number of doors: {totalDoors}\nTotal number of boxes: {totalBoxes}"
                            , "QGame", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Error in saving file");
                    }
                    break;
                case DialogResult.Cancel:
                    break;
                case DialogResult.Abort:
                    break;
                case DialogResult.Retry:
                    break;
                case DialogResult.Ignore:
                    break;
                case DialogResult.Yes:
                    break;
                case DialogResult.No:
                    break;
                default:
                    break;
            }
        }
    }
}
