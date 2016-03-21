using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LOTK.Controller;

namespace LOTK.View
{
    public partial class Form1 : Form
    {
        private viewController controller;
        private int position;
        public Form1(viewController controller, int pos)
        {
            this.position = pos;
            this.controller = controller;
            InitializeComponent();
        }

        public void updateForm()
        {
            Required_Data data = controller.getData(position);

        }
    }
}
