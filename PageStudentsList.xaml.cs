using ClassesStudentsMAUI.Models;
using Microsoft.Maui.Controls;

namespace ClassesStudentsMAUI
{
    public partial class PageStudentsList : ContentPage
    {
        private readonly Class _currentClass;

        public PageStudentsList(Class currentClass)
        {
            InitializeComponent();
            _currentClass = currentClass;
            LoadStudents();
        }

        // Phương thức tải danh sách học sinh từ database
        private void LoadStudents()
        {
            using (var dbContext = new AppDbContext())
            {
                var studentList = dbContext.Students
                    .Where(s => s.ClassID == _currentClass.ClassID)
                    .ToList();
                StudentListView.ItemsSource = studentList;
            }
        }

        // Xử lý sự kiện khi nhấn nút Thêm học sinh
        private async void AddStudent_Clicked(object sender, EventArgs e)
        {
            var addStudentPage = new PageAddStudent();
            addStudentPage.StudentAdded += async (s, studentData) =>
            {
                var (studentId, name, dob) = studentData;

                var newStudent = new Student
                {
                    StudentID = studentId,
                    StudentName = name,
                    StudentDOB = dob,
                    ClassID = _currentClass.ClassID
                };

                using (var dbContext = new AppDbContext())
                {
                    dbContext.Students.Add(newStudent);
                    await dbContext.SaveChangesAsync();
                }

                LoadStudents();
            };

            await Navigation.PushModalAsync(addStudentPage);
        }

        // Xử lý sự kiện khi nhấn nút Sửa học sinh
        private async void EditStudent_Clicked(object sender, EventArgs e)
        {
            var button = sender as Button;
            var selectedStudent = button?.CommandParameter as Student;

            if (selectedStudent != null)
            {
                var editStudentPage = new PageAddStudent(selectedStudent);
                editStudentPage.StudentAdded += async (s, studentData) =>
                {
                    var (studentId, name, dob) = studentData;

                    selectedStudent.StudentID = studentId;
                    selectedStudent.StudentName = name;
                    selectedStudent.StudentDOB = dob;

                    using (var dbContext = new AppDbContext())
                    {
                        dbContext.Students.Update(selectedStudent);
                        await dbContext.SaveChangesAsync();
                    }

                    LoadStudents();
                };

                await Navigation.PushModalAsync(editStudentPage);
            }
        }

        // Xử lý sự kiện khi nhấn nút Xóa học sinh
        private async void DeleteStudent_Clicked(object sender, EventArgs e)
        {
            var button = sender as Button;
            var selectedStudent = button?.CommandParameter as Student;

            if (selectedStudent != null)
            {
                bool confirm = await DisplayAlert("Xác nhận", "Bạn có chắc muốn xóa học sinh này không?", "Yes", "No");

                if (confirm)
                {
                    using (var dbContext = new AppDbContext())
                    {
                        dbContext.Students.Remove(selectedStudent);
                        await dbContext.SaveChangesAsync();
                    }

                    LoadStudents();
                }
            }
        }
    }
}
