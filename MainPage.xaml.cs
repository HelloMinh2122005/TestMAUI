using ClassesStudentsMAUI.Data;
using ClassesStudentsMAUI.Models;

namespace ClassesStudentsMAUI
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            LoadClasses();
        }

        private void LoadClasses()
        {
            using (var dbContext = new AppDbContext())
            {
                var classList = dbContext.Classes.ToList();
                ClassListView.ItemsSource = classList;
            }
        }

        public async void AddClass_Clicked(object sender, EventArgs e)
        {
            var PageAddClass = new PageAddClass();

            // Xử lý sự kiện khi lớp mới được thêm từ PageAddClass
            PageAddClass.ClassAdded += async (s, classData) =>
            {
                var (ClassID, className) = classData;

                // Tạo đối tượng Class mới
                var newClass = new Class
                {
                    ClassID = ClassID,
                    ClassName = className
                };

                // Lưu vào database
                using (var dbContext = new AppDbContext())
                {
                    dbContext.Classes.Add(newClass);
                    await dbContext.SaveChangesAsync();
                }

                // Cập nhật danh sách lớp hiển thị
                LoadClasses();
            };

            // Mở trang thêm lớp
            await Navigation.PushModalAsync(PageAddClass);
        }


        public async void EditClass_Clicked(object sender, EventArgs e)
        {
            var button = sender as Button;
            var selectedClass = button?.CommandParameter as Class;

            if (selectedClass != null)
            {
                // Mở trang chỉnh sửa với dữ liệu lớp đã chọn
                var editClassPage = new PageAddClass
                {
                    BindingContext = selectedClass
                };

                editClassPage.ClassAdded += async (s, classData) =>
                {
                    var (ClassID, className) = classData;

                    // Cập nhật thông tin lớp
                    selectedClass.ClassID = ClassID;
                    selectedClass.ClassName = className;

                    using (var dbContext = new AppDbContext())
                    {
                        dbContext.Classes.Update(selectedClass);
                        await dbContext.SaveChangesAsync();
                    }

                    // Cập nhật danh sách lớp hiển thị
                    LoadClasses();
                };

                await Navigation.PushModalAsync(editClassPage);
            }
        }


        public async void DeleteClass_Clicked(object sender, EventArgs e)
        {
            var button = sender as Button;
            var selectedClass = button?.CommandParameter as Class;

            if (selectedClass != null)
            {
                bool confirm = await DisplayAlert("Xác nhận", "Bạn có chắc muốn xóa lớp này không?", "Yes", "No");

                if (confirm)
                {
                    using (var dbContext = new AppDbContext())
                    {
                        dbContext.Classes.Remove(selectedClass);
                        await dbContext.SaveChangesAsync();
                    }

                    // Cập nhật danh sách lớp hiển thị
                    LoadClasses();
                }
            }
        }

        public async void OnClassSelected(object sender, SelectionChangedEventArgs e)
        {
            var selectedClass = e.CurrentSelection.FirstOrDefault() as Class;
            if (selectedClass != null)
            {
                // Điều hướng đến trang danh sách học sinh của lớp
                var PageStudentsList = new PageStudentsList(selectedClass);
                await Navigation.PushAsync(PageStudentsList);
            }
        }
    }
}