using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using IntellectBit.App.GenAssemblyArchRepo.BusinessEntities;

namespace IntellectBit.App.GenAssemblyArchRepo.DataModel
{
    public class MainWindowDataModel : DataModelBase
    {
        private string _folderPath;
        public string FolderPath
        {
            get { return _folderPath; }
            set
            {
                _folderPath = value;
                OnPropertyChanged("FolderPath");
            }
        }

        private ObservableCollection<WindowFile> _windowFiles = new ObservableCollection<WindowFile>();
                public ObservableCollection<WindowFile> WindowFiles
        {
            get { return _windowFiles; }
            set
            {
                _windowFiles = value;
                OnPropertyChanged("WindowFiles");
            }
        }

        private ICommand _selectFolderPathCommand;

        public ICommand SelectFolderPathCommand
        {
            get { return _selectFolderPathCommand; }
            set
            {
                _selectFolderPathCommand = value;
                OnPropertyChanged("SelectFolderPathCommand");
            }
        }

        private ICommand _exportToCSVCommand;

        public ICommand ExportToCSVCommand
        {
            get { return _exportToCSVCommand; }
            set
            {
                _exportToCSVCommand = value;
                OnPropertyChanged("ExportToCSVCommand");
            }
        }

        private ICommand _processFileCommand;
        public ICommand ProcessFileCommand
        {
            get { return _processFileCommand; }
            set
            {
                _processFileCommand = value;
                OnPropertyChanged("ProcessFileCommand");
            }
        }


        private ICommand _closeCommand;
        public ICommand CloseCommand
        {
            get { return _closeCommand; }
            set
            {
                _closeCommand = value;
                OnPropertyChanged("CloseCommand");
            }
        }
    }
}
