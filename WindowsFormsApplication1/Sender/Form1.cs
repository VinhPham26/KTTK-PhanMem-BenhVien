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

namespace Sender
{
    public partial class Form1 : Form
    {
        MessageQueue queue = null;
        public Form1()
        {
            InitializeComponent();
            init();
        }

        public void init()
        {
            string path = @".\private$\phongkehoach";
            //string path = @"hbmnl\private$\phongkehoach";
            if (MessageQueue.Exists(path))
            {
                queue = new MessageQueue(path, QueueAccessMode.Send);
            }
            else
                queue = MessageQueue.Create(path, true);
        }
        private void button1_Click(object sender, EventArgs e)
        {
            benhNhan bn = new benhNhan();
            bn.msbn = textBox1.Text;
            bn.cmnd = textBox2.Text;
            bn.hoten = textBox3.Text;
            bn.diachi = textBox4.Text;
            MessageQueueTransaction transaction = new MessageQueueTransaction();
            transaction.Begin();
            queue.Send(bn, transaction);
            transaction.Commit();
        }
    }
}
