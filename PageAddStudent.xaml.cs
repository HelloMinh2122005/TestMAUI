using System;
using ClassesStudentsMAUI.Models;

namespace ClassesStudentsMAUI
{
    public partial class PageAddStudent : ContentPage
    {
        // Sự kiện để gửi dữ liệu học sinh trở lại danh sách
        public event EventHandler<Student>? StudentAdded;

        private readonly Student _student;

        // Constructor khi thêm mới học sinh
        public PageAddStudent()
        {
            InitializeComponent();
            _student = new Student();
        }

        // Constructor khi chỉnh sửa thông tin học sinh
        public PageAddStudent(Student student)
        {
            InitializeComponent();
            _student = student;

            // Hiển thị thông tin hiện tại của học sinh lên Entry và DatePicker
            StudentIDEntry.Text = _student.StudentID;
            StudentNameEntry.Text = _student.StudentName;
            StudentDOBPicker.Date = _student.StudentDOB;
        }

        // Xử lý sự kiện khi nhấn nút Lưu
        private void OnSaveClicked(object sender, EventArgs e)
        {
            string studentId = StudentIDEntry.Text;
            string studentName = StudentNameEntry.Text;
            DateTime studentDOB = StudentDOBPicker.Date;

            // Kiểm tra dữ liệu hợp lệ
            if (!string.IsNullOrEmpty(studentId) && !string.IsNullOrEmpty(studentName))
            {
                // Cập nhật đối tượng Student
                _student.StudentID = studentId;
                _student.StudentName = studentName;
                _student.StudentDOB = studentDOB;

                // Kích hoạt sự kiện StudentAdded và gửi dữ liệu về PageStudentsList
                StudentAdded?.Invoke(this, _student);

                // Đóng trang sau khi lưu
                Navigation.PopModalAsync();
            }
            else
            {
                // Hiển thị cảnh báo nếu trường nhập liệu trống
                DisplayAlert("Error", "Please enter the student's ID and name.", "OK");
            }
        }

        // Xử lý sự kiện khi nhấn nút Hủy
        private void OnCancelClicked(object sender, EventArgs e)
        {
            // Đóng trang nếu người dùng nhấn Hủy
            Navigation.PopModalAsync();
        }
    }
}
