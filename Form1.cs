using static Music_School.Service.MusicSchoolService;

namespace Music_School
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            CreateXmlIfNotExist();
            insertClassRoom("guitar jazz");
            AddTeacher("guitar jazz", "yossi levi");
            AddStudent("guitar jazz", "evya", "guitar");
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
