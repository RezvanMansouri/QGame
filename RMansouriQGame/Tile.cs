using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RMansouriQGame 
{
    /// <summary>
    /// A Tile class that inherited for a pictureBox
    /// </summary>
    public class Tile : PictureBox
    {
        //Adding some other properties to the class
        public int Row { get; set; }
        public int Column { get; set; }
        public int Type { get; set; }
    }
}
