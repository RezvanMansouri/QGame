/* PlayForm.cs
* Assignment 3
* Revision History
* Rezvan Rose Mansouri, 2021.11.10: Created
* Rezvan Rose Mansouri, 2021.11.11: Added code
* Rezvan Rose Mansouri, 2021.11.12: Added code
* Rezvan Rose Mansouri, 2021.11.14: Added code
* Rezvan Rose Mansouri, 2021.11.17: Debugging complete
* Rezvan Rose Mansouri, 2021.11.17: Comments added
*
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
    /// A class that play the game designed in Assignment2
    /// </summary>
    public partial class PlayForm : Form
    {
        // Declaring class variables and constants
        Tile[,] tiles;

        private Image greenBox = RMansouriQGame.Properties.Resources.green_box;
        private Image redBox = RMansouriQGame.Properties.Resources.red_box;
        private Image greenDoor = RMansouriQGame.Properties.Resources.green_door;
        private Image redDoor = RMansouriQGame.Properties.Resources.red_door;
        private Image walls = RMansouriQGame.Properties.Resources.Wall;

        const int WALL = 1;
        const int RED_DOOR = 2;
        const int RED_BOX = 4;
        const int GREEN_DOOR = 3;
        const int GREEN_BOX = 5;

        const int INIT_LEFT = 0;
        const int INIT_TOP = 0;
        const int WIDTH = 77;
        const int HEIGHT = 77;

        int numberOfMoves = 0;
        private Tile selectedBox = null;
        bool focusedBox = true;

        /// <summary>
        /// Default constructor of the PlayForm class
        /// </summary>
        public PlayForm()
        {
            InitializeComponent();
            txbNumofMoves.Text = "0";
            txbNumofBoxes.Text = "0";
        }

        /// <summary>
        /// a method that return a tile at the specified row and col 
        /// if tile does not have value return null
        /// </summary>
        /// <param name="row">Row</param>
        /// <param name="col">Coloumn</param>
        /// <returns></returns>

        public Tile getTile(int row, int col)
        {
            if (tiles[row, col] != null)
            {
                Tile tile = tiles[row, col];
                return tile;
            }
            else
                return null;
        }

        /// <summary>
        /// a method to clear panel
        /// </summary>
        public void ResetGame()
        {
            tiles = null;
            txbNumofBoxes.Text = "0";
            txbNumofMoves.Text = "0";
            numberOfMoves = 0;
            panel1.Controls.Clear();
            selectedBox = null;
        }

        /// <summary>
        /// a method that load a file and read the informations and create the create game
        /// </summary>
        /// <param name="fileName"> File Name </param>
        public void LoadGame(string fileName)
        {
            //to make sure if another game loaded everything from last game is reset
            ResetGame();

            int startX = 0;
            int startY = 0;
            int numberOfRows = 0;
            int numberOfCol = 0;

            Tile tileBox;

            StreamReader reader = null;

            try
            {
                reader = new StreamReader(fileName);

                //read the number of rows and coloumns from the first and second line of the code
                numberOfRows = int.Parse(reader.ReadLine());
                numberOfCol = int.Parse(reader.ReadLine());
            }
            catch (Exception)
            {
                //if there is any error while reading the file show a message
                MessageBox.Show("An error accured to loading file, Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            //two dimentional array of tiles
            tiles = new Tile[numberOfRows, numberOfCol];

            //to put information readed inside the web form and array
            for (int i = 0; i < numberOfRows; i++)
            {
                for (int j = 0; j < numberOfCol; j++)
                {
                    //Row
                    reader.ReadLine();
                    //Column
                    reader.ReadLine();
                    //Type
                    int type = int.Parse(reader.ReadLine());

                    if (type != 0)
                    {
                        tileBox = new Tile
                        {
                            Row = i,
                            Column = j,
                            Type = type,
                            Width = WIDTH,
                            Height = HEIGHT,
                            BorderStyle = BorderStyle.FixedSingle,
                            Image = PickImage(type),
                            Left = startX,
                            Top = startY
                        };
                        tiles[i, j] = tileBox;

                        //Add to the design mode
                        panel1.Controls.Add(tileBox);

                        //make the event handler for clicking if they are Boxes
                        if (type == RED_BOX || type == GREEN_BOX)
                            tileBox.Click += TileBox_Click;
                    }
                    startX += WIDTH;
                }
                startY += HEIGHT;
                startX = 0;
            }
            // got throuugh array and write in the textbox  how many box are there
            txbNumofBoxes.Text = ItrateArray().ToString();
        }

        /// <summary>
        /// a method to delete a box  if validation is correct
        /// </summary>
        /// <param name="tile"> the tile that need to removed </param>
        public bool DeleteTile(Tile tile)
        {
            bool result = false;
            //if the red door is next to red box and green box is next to green box then delete the box
            if ((tile.Type == GREEN_DOOR && selectedBox.Type == GREEN_BOX) ||
                    (tile.Type == RED_DOOR && selectedBox.Type == RED_BOX))
            {
                tiles[selectedBox.Row, selectedBox.Column] = null;
                selectedBox.Dispose();
                selectedBox = null;
                focusedBox = true;

                result = true;

                // Itrate through the array and check if any box left, if not end the game and reset 
                txbNumofBoxes.Text = ItrateArray().ToString();
                if (ItrateArray() == 0)
                {
                    MessageBox.Show("Congratulations! \n\nGame Ended", "QGame", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    ResetGame();
                }
            }
            return result;
        }

        /// <summary>
        /// a method to move selected box
        /// </summary>
        public bool MoveBox()
        {
            selectedBox.Left = INIT_LEFT + WIDTH * selectedBox.Column;
            selectedBox.Top = INIT_TOP + HEIGHT * selectedBox.Row;
            return true;
        }

        /// <summary>
        /// a method to itrate through array and check for the number boxes and return it
        /// </summary>
        /// <returns>Number of Boxes left</returns>
        public int ItrateArray()
        {
            int numberofBox = 0;
            foreach (var tile in tiles)
            {
                if (tile != null)
                    if (tile.Type == RED_BOX || tile.Type == GREEN_BOX)
                    {
                        numberofBox++;
                    }
            }
            return numberofBox;
        }

        /// <summary>
        /// this method has the logic to paly the game
        /// </summary>
        /// <param name="row">row for specefic move</param>
        /// <param name="col">coloumn for specefic move</param>
        /// <returns>True or false depend on if there is still space to move</returns>
        private bool PlayTheGame(int row, int col)
        {
            try
            {
                //get the neighbour tile information depend's on the which button is been clicked
                Tile tile = getTile(row, col);
                //if it's not null then check if the validation is true then delete the box
                if (tile != null)
                {
                    DeleteTile(tile);
                    return false;
                }
                else
                {
                    //if that tile is null move
                    //put the box inside that tile
                    tiles[row, col] = tiles[selectedBox.Row, selectedBox.Column];
                    //make the tile null
                    tiles[selectedBox.Row, selectedBox.Column] = null;
                    // change the column of the selectedbox to that tile
                    selectedBox.Column = col;
                    selectedBox.Row = row;

                    MoveBox();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("oops.Something went wrong! please try again");
            }
            return true;
        }

        //Boxes click event handler
        private void TileBox_Click(object sender, EventArgs e)
        {
            try
            {
                //if no box has been selected before:
                if (focusedBox)
                {
                    selectedBox = (Tile)sender;
                    //to get focous
                    selectedBox.BorderStyle = BorderStyle.Fixed3D;
                    focusedBox = false;
                }
                else
                {
                    //to delete focous
                    selectedBox.BorderStyle = BorderStyle.None;
                    selectedBox = (Tile)sender;
                    selectedBox.BorderStyle = BorderStyle.Fixed3D;
                }
            }
            catch (Exception)
            {
                MessageBox.Show("oops. Something went wrong! please try again");
            }
        }

        /// <summary>
        /// a method to get the related image for tile
        /// </summary>
        /// <param name="type">Type of the tile</param>
        /// <returns>selected image type</returns>
        public Image PickImage(int type)
        {
            Image image = null;
            switch (type)
            {
                case WALL:
                    image = walls;
                    break;
                case RED_DOOR:
                    image = redDoor;
                    break;
                case GREEN_DOOR:
                    image = greenDoor;
                    break;
                case RED_BOX:
                    image = redBox;
                    break;
                case GREEN_BOX:
                    image = greenBox;
                    break;
                default:
                    break;
            }
            return image;
        }

        private void loadGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = dlgOpen.ShowDialog();
            switch (dialogResult)
            {
                case DialogResult.None:
                    break;
                case DialogResult.OK:
                    string fileName = dlgOpen.FileName;
                    LoadGame(fileName);
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

       //evenhandler for all the buttons
        private void btnController_Click(object sender, EventArgs e)
        {
            Button clickedButton = (Button)sender;
            
            bool result = true;
            //if no boxes are selected, show a message 
            if (selectedBox == null)
                MessageBox.Show("Click to start");
            else
            {
                //count each click and update the NUMofMoves text box
                txbNumofMoves.Text = (++numberOfMoves).ToString();
                switch (clickedButton.Name)
                {

                    case "btnUp":
                        while (result)
                            result = PlayTheGame(selectedBox.Row - 1, selectedBox.Column);
                        break;

                    case "btnDown":
                        while (result)
                            result = PlayTheGame(selectedBox.Row + 1, selectedBox.Column);
                        break;

                    case "btnRight":
                        while (result)
                            result = PlayTheGame(selectedBox.Row, selectedBox.Column + 1);
                        break;

                    case "btnLeft":
                        while (result)
                            result = PlayTheGame(selectedBox.Row, selectedBox.Column - 1);
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
    }
}
