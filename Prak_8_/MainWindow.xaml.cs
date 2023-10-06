using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Json;

namespace Prak_8_
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        static List<p> zam = Jsonka.Des<List<p>>("заметки.json") ?? new List<p>();
        public MainWindow()
        {
            InitializeComponent();
            foreach (p d in zam)
            {
                if (datka.SelectedDate.Value.ToLongDateString() == d.dt)
                {
                    lb.Items.Insert(lb.Items.Count, d.naz);
                }
            }
        }


        void DatePicker_CalendarClosed(object sender, RoutedEventArgs e)
        {
            txt1.Text = null;
            txt2.Text = null;
            if (zam.Count != 0)
            {
                lb.Items.Clear();
                foreach (p d in zam)
                {
                    if (datka.SelectedDate.Value.ToLongDateString() == d.dt)
                    {
                        lb.Items.Insert(lb.Items.Count, d.naz);
                    }
                }
            }
            lb.SelectedIndex = -1;
        }

        private void Del(object sender, RoutedEventArgs e)
        {
            if (lb.SelectedIndex != -1)
            {
                lb.Items.RemoveAt(lb.SelectedIndex);
                foreach (p a in zam)
                    if (datka.SelectedDate.Value.ToLongDateString() == a.dt && txt1.Text == a.naz || txt2.Text == a.op)
                    {
                        txt1.Text = null;
                        txt2.Text = null;
                        zam.Remove(a);
                        break;
                    }
                Jsonka.Ser("заметки.json", zam);
                lb.SelectedIndex = -1;
            }
        }
        private void Add(object sender, RoutedEventArgs e)
        {
            bool r = false;
            p a = new p();
            foreach (p g in zam)
            {
                if (g.naz == txt1.Text)
                {
                    r = true;
                    break;
                }
            }
            if (r != true)
            {
                if (txt1.Text != null && txt2.Text != null && datka.SelectedDate != null)
                {
                    a.dt = datka.SelectedDate.Value.ToLongDateString();
                    a.naz = txt1.Text;
                    a.op = txt2.Text;
                    zam.Add(a);
                    lb.Items.Insert(lb.Items.Count, txt1.Text);
                    Jsonka.Ser("заметки.json", zam);
                }
            }
            else
                MessageBox.Show("Заметка с таким названием уже существует!");
        }
        private void Save(object sender, RoutedEventArgs e)
        {
            if (lb.SelectedIndex != -1)
            {
                bool r = false;
                if (txt1.Text != null && txt2.Text != null && datka.SelectedDate != null)
                {
                    foreach (p g in zam)
                    {
                        if (g.naz == txt1.Text)
                        {
                            r = true;
                            break;
                        }
                    }
                    if (r != true)
                    {
                        foreach (p a in zam)
                            if (a.dt == datka.SelectedDate.Value.ToLongDateString() && a.naz == lb.SelectedItem.ToString())
                            {
                                int u = lb.SelectedIndex;
                                a.dt = datka.SelectedDate.Value.ToLongDateString();
                                a.naz = txt1.Text;
                                a.op = txt2.Text;
                                lb.Items.RemoveAt(u);
                                lb.Items.Insert(u, txt1.Text);
                                Jsonka.Ser("заметки.json", zam);
                                MessageBox.Show("Успешно сохранено!");
                                break;
                            }
                    }
                    else
                    {
                        MessageBox.Show("Заметка с таким названием уже существует");
                    }
                }
            }
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if (lb.SelectedIndex != -1)
                foreach (p d in zam)
                    if (datka.SelectedDate.Value.ToLongDateString() == d.dt && lb.SelectedItem.ToString() == d.naz)
                    {
                        txt1.Text = lb.SelectedItem.ToString();
                        txt2.Text = d.op;
                    }
        }

        private void cls(object sender, RoutedEventArgs e)
        {
            lb.SelectedIndex = -1;
            txt1.Text = null;
            txt2.Text = null;
        }
        class p
        {
            public string naz;
            public string op;
            public string dt;
        }
    }
    }
