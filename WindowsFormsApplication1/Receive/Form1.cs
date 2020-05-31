using Object;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Receive
{
    public partial class Form1 : Form
    {
        MessageQueue queue = null;
        public Form1()
        {
            InitializeComponent();
            init();
        }
        void init()
        {
            string path = @".\private$\phongkehoach";
            queue = new MessageQueue(path);
            queue.BeginReceive();
            queue.ReceiveCompleted += Queue_ReceiveCompleted;
        }
        private void Queue_ReceiveCompleted(object sender, ReceiveCompletedEventArgs e)
        {
            var msg = e.Message;
            int type = msg.BodyType;
            XmlMessageFormatter fmt = new XmlMessageFormatter(
            new Type[] { typeof(string), typeof(benhNhan) }
            );
            msg.Formatter = fmt;
            var result = msg.Body;
            var t = result.GetType();
            if (t.Equals(typeof(benhNhan)))
            {
                SetText((benhNhan)result);
            }
            //else
            //    SetText("" + result);
            queue.BeginReceive();//loop back
        }
        private void SetText(benhNhan bn)
        {
            listBox1.DisplayMember = "hoten";
            listBox1.ValueMember = "cmnd";
            listBox1.Items.Add(bn);
        }


        private void button1_Click(object sender, EventArgs e)
        {
            benhNhan bn = (benhNhan)listBox1.SelectedItem;
            if (bn != null)
            {
                textBox1.Text = bn.msbn;
                textBox2.Text = bn.cmnd;
                textBox3.Text = bn.hoten;
                textBox4.Text = bn.diachi;
            }
        }
    }
}
