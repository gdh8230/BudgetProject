using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Core.BaseForm
{
    public partial class BaseLogin : MetroFramework.Forms.MetroForm
    {
        public BaseLogin()
        {
            InitializeComponent();
            env = new _Environment();
        }
        _Environment env;
    }
}
