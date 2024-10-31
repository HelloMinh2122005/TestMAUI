namespace ClassesStudentsMAUI;

public partial class PageAddClass : ContentPage
{
    public event EventHandler<(string ClassID, string ClassName)>? ClassAdded;

    public PageAddClass()
	{
		InitializeComponent();
	}

    // Xử lý sự kiện khi nhấn nút Save
    private void OnSaveClicked(object sender, EventArgs e)
    {
        string ClassID = ClassIDEntry.Text;
        string className = ClassNameEntry.Text;

        // Kiểm tra dữ liệu có hợp lệ không
        if (!string.IsNullOrEmpty(ClassID) && !string.IsNullOrEmpty(className))
        {
            // Kích hoạt sự kiện ClassAdded và gửi dữ liệu về MainPage
            ClassAdded?.Invoke(this, (ClassID, className));

            // Đóng trang sau khi lưu
            Navigation.PopModalAsync();
        }
        else
        {
            // Hiển thị cảnh báo nếu trường nhập liệu trống
            DisplayAlert("Lỗi", "Vui lòng nhập đầy đủ dữ liệu", "OK");
        }
    }

    // Xử lý sự kiện khi nhấn nút Cancel
    private void OnCancelClicked(object sender, EventArgs e)
    {
        // Đóng trang nếu người dùng nhấn Cancel
        Navigation.PopModalAsync();
    }
}