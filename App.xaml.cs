namespace ClassesStudentsMAUI
{
    public partial class App : Application
    {
        public static AppDbContext DbContext { get; private set; }
        public App()
        {
            InitializeComponent();

            DbContext = new AppDbContext();

            // Tạo database nếu chưa tồn tại
            //DbContext.Database.EnsureCreated();

            MainPage = new MainPage();
        }
    }
}