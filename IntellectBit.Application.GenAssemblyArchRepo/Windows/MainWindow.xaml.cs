using System.Windows;
using IntellectBit.App.GenAssemblyArchRepo.Presenter;

namespace IntellectBit.App.GenAssemblyArchRepo.Windows
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MainWindowPresenter _presenter = new MainWindowPresenter();
        public MainWindow()
        {
            InitializeComponent();

            _presenter.View = this;
            this.DataContext = _presenter.DataModel;
        }

    }
}
