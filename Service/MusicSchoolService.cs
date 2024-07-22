using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static Music_School.configuration.MusicSchoolConfiguration;

namespace Music_School.Service
{
    internal static class MusicSchoolService
    {
        public static void CreateXmlIfNotExist()
        {
            // בודק אם המסמך קיים ואם לא אז יוצר אותו
            if (!File.Exists(MusicSchoolPath))
            {
                // יצירת מסמך אקסאמאל חדש
                XDocument document = new();

                // יצירת אלמנט
                XElement musicSchool = new("music-school");

                // הוספת האלמנט למסמך
                document.Add(musicSchool);

                // שמירת המסמך בנתיב הזה
                document.Save(MusicSchoolPath);
            }
        }

        // פונקציה שמקבלת שם כיתה חדשה ומוסיפה אותה
        public static void insertClassRoom(string classroom)
        {
            // יוצר משתנה שטוען את המסמך החדש 
            XDocument document = XDocument.Load(MusicSchoolPath);

            // יוצר משתנה שיוצר צאצא ל - מיוזיק סקול 
            XElement? musicSchool = document.Descendants("music-school").FirstOrDefault();

            // בודק אם ה - מיוזיק סקול קיים
            if (musicSchool == null)
            {
                // אם לא קיים הוא יוצא מהפונקציה
                return;
            }

            // יצירת אלמנט חדש של כיתה עם שם הכיתה כמאפיין
            XElement newClassRoom = new(
                "class-Room",
                new XAttribute("name", classroom)
                );

            // מוסיף את הכיתה לתוך ה - מיוזיק סקול
            musicSchool.Add(newClassRoom);

            // שומר את המסמך
            document.Save(MusicSchoolPath);
        }

        public static void AddTeacher(string classRoomName, string teacherName)
        {
            XDocument document = XDocument.Load(MusicSchoolPath);

            XElement? musicSchool = document.Descendants("music-school").FirstOrDefault();

            if (musicSchool == null)
            {
                return; 
            }

            XElement? classRoom = musicSchool.Descendants("class-Room")
                .FirstOrDefault(room => room.Attribute("name")?.Value == classRoomName);

            if (classRoom == null)
            {
                return; 
            }

            XElement Teacher = new(
                "teacher",
                new XAttribute("name", teacherName)
                );

            classRoom.Add(Teacher);
            document.Save(MusicSchoolPath);
        }

        public static void AddStudent(string classroomName, string studentName, string instrument)
        {
            try
            {
                XDocument document = XDocument.Load(MusicSchoolPath);

                XElement? musicSchool = document.Descendants("music-school").FirstOrDefault();

                if (musicSchool == null)
                {
                    return;
                }

                XElement? classRoom = musicSchool.Descendants("class-room")
                    .FirstOrDefault(room => room.Attribute("name")?.Value == classroomName);

                if (classRoom == null)
                {
                    return;
                }

                XElement student = new(
                    "student",
                    new XAttribute("name", studentName),
                    new XElement("instrument", instrument)
                    );

                classRoom.Add(student);
                document.Save(MusicSchoolPath);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
            }
        }
    }
}
