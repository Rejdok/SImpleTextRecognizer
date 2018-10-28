using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SimpleTextRecognizer
{
    public partial class MultiPagePannel : UserControl
    {
        public MultiPagePannel()
        {
            InitializeComponent();
        }
        public void AddControl(Control control)
        {
            control.Dock = DockStyle.Fill;
            controls.Add(control);
        }
        public void RemoveControl(Control control)
        {
            controls.Remove(control);
        }
        public void SetActive(Control control)
        {
            if (panel1.Controls.Count != 0) { 
                    panel1.Controls.Remove(panel1.Controls[0]);
            }
            panel1.Controls.Add(control);
        }
        List<Control> controls = new List<Control>();
    }
}
