using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using zh3_gyak.Models;

namespace zh3_gyak
{
    public partial class UserControl1 : UserControl
    {
        StudiesContext context = new StudiesContext();
        public UserControl1()
        {
            InitializeComponent();
        }

        private void UserControl1_Load(object sender, EventArgs e)
        {
            var er = from x in context.Instructors
                     select x;

            listBox1.DataSource = er.ToList();
            listBox1.DisplayMember = "Name";

            var oktatóSzám = context.Instructors.Count();
            var legmagasabbStatus = (from x in context.Instructors
                                     select x.StatusFk).Max();

            var legrangosabb = (from x in context.Instructors
                                      where x.StatusFk == legmagasabbStatus
                                      select x.Name);

            MessageBox.Show($"{string.Join(',', legrangosabb)}");

            //csv-be iras feladat lehet

            // uj terem felvétele adatbazisba feladat lehet
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem == null) return;

            var instructor = (Instructor)listBox1.SelectedItem;

            var órák = from x in context.Lessons
                       where x.InstructorFK == instructor.InstructorSk///////////////
                       select new
                       {
                           x.CourseFkNavigation.Name,
                           x.CourseFkNavigation.Code,
                           Nap = x.DayFkNavigation.Name,
                           Sáv = x.TimeFkNavigation.Name,
                           Terem = x.RoomFkNavigation.Name,
                       };

            dataGridView1.DataSource = órák.toList();/////////
        }
    }
}
