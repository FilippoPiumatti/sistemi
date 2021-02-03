using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Forms;

namespace _02_Automobiline_Thread
{
    public partial class Form1 : Form
    {
        Random rnd = new Random();
        public Form1()
        {
            InitializeComponent();
        }
        #region consegna
        /// 5 picture box, ognuna è una macchinina,
        /// a ogni macchinina associo un thread
        /// 1 unico metodo per automobile 
        /// OBBIETTIVO: Arrivare alla riga rossa posta a 900px orizzontali
        /// ogni machinina a una velocità diversa in px/sec generato casualmente tra 5px/sec e 30px/sec
        /// stampare in una textbox l'ordine di arrivo
#endregion

        private struct str
        {
            public int velocita;
            public Thread auto;
            public bool ar;
            public PictureBox pb;
            public int pos;
        }
        List<str> lst = new List<str>();
        private void Form1_Load(object sender, EventArgs e)
        { 
            for (int i = 0; i < 5; i++)
            {
                str st = new str();
                st.auto = new Thread(threadAuto);
                st.velocita = rnd.Next(5, 30);
                st.ar = false;
                st.pos = i + 1;
                switch (i)
                {
                    case 0:
                        st.pb = pictureBox1;
                        break;
                    case 1:
                        st.pb = pictureBox2;
                        break;
                    case 2:
                        st.pb = pictureBox3;
                        break;
                    case 3:
                        st.pb = pictureBox4;
                        break;
                    case 4:
                        st.pb = pictureBox5;
                        break;
                }
                lst.Add(st);
            }
        }

        private void threadAuto(object obj)
        {
            str s = (str)obj;
            while (!s.ar)
            {
                if (s.pb.Location.X > 900)
                {
                    BeginInvoke((MethodInvoker)delegate ()
                    {
                        s.ar = true;
                        textBox1.Text += "macchina " + s.pos;
                    });
                }
                else {
                    BeginInvoke((MethodInvoker)delegate ()
                    {
                        s.pb.Left += s.velocita;
                    });
                    Thread.Sleep(100);
                }
                
            }
            
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < 5; i++)
            {
                lst[i].auto.Start(lst[i]);
            }
        }

        private void btnAbort_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < 5; i++)
            {
                lst[i].auto.Abort();
            }
        }
    }
}
