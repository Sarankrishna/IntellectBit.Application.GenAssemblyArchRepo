using System;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows;
using IntellectBit.App.GenAssemblyArchRepo.Classes;
using IntellectBit.App.GenAssemblyArchRepo.DataModel;
using IntellectBit.App.GenAssemblyArchRepo.Windows;
using System.Windows.Forms;
using System.IO;
using System.Reflection;
using System.Text;

namespace IntellectBit.App.GenAssemblyArchRepo.Presenter
{
    public class MainWindowPresenter
    {
        private MainWindowDataModel _dataModel;
        private MainWindow _view;

        public MainWindow View
        {
            get { return _view; }
            set { _view = value; }
        }
        public MainWindowDataModel DataModel
        {
            get { return _dataModel; }
            set { _dataModel = value; }
        }

        public MainWindowPresenter()
        {
            _dataModel = new MainWindowDataModel();
            _dataModel.FolderPath = Directory.GetCurrentDirectory();
            _dataModel.ProcessFileCommand = new DelegateCommand(ProcessFileCommandHandler, ProcessFileCommandCanExcute);
            _dataModel.ExportToCSVCommand = new DelegateCommand(ExportToCSVCommandHandler, ExportToCSVCommandCanExcute);
            _dataModel.SelectFolderPathCommand = new DelegateCommand(SelectFolderPathCommandHandler, SelectFolderPathCommandCanExcute);
            _dataModel.CloseCommand = new DelegateCommand(CloseCommandExecuted, CloseCommandCanExcute);

        }

        private bool SelectFolderPathCommandCanExcute(object obj)
        {
            return true;
        }

        private void SelectFolderPathCommandHandler(object obj)
        {
            FolderBrowserDialog folderDialog = new FolderBrowserDialog();
            if (string.IsNullOrEmpty(_dataModel.FolderPath))
            {
                folderDialog.SelectedPath = Directory.GetCurrentDirectory();
            }
            else
            {
                folderDialog.SelectedPath = _dataModel.FolderPath;
            }
            

            DialogResult result = folderDialog.ShowDialog();
            if (result.ToString() == "OK")
                _dataModel.FolderPath = folderDialog.SelectedPath;
        }

        private bool ExportToCSVCommandCanExcute(object obj)
        {
            return true;
        }

        private void ExportToCSVCommandHandler(object obj)
        {
            
           if(_dataModel != null && _dataModel.WindowFiles !=  null && _dataModel.WindowFiles.Any())
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendLine("FilePath,Architecure");
                foreach (var item in _dataModel.WindowFiles)
                {
                    sb.Append(item.FilePath);
                    sb.Append(",");
                    sb.AppendLine(item.Architecture.ToString());
                }
                var filePath = Path.GetTempPath() + "AssemblyArchReport.csv";
                File.WriteAllText(filePath, sb.ToString());
                System.Diagnostics.Process.Start(filePath);
            }
        }

        private void ProcessFileCommandHandler(object obj)
        {
            _dataModel.WindowFiles.Clear();
            var files = Directory.EnumerateFiles(_dataModel.FolderPath, "*.*", SearchOption.AllDirectories)
            .Where(s => s.EndsWith(".exe") || s.EndsWith(".dll"));
            foreach (var file in files)
            {
                var windowFile = new BusinessEntities.WindowFile { FilePath = file };
                windowFile.Architecture = GetAssemblyArchitecture(file);
                _dataModel.WindowFiles.Add(windowFile);
            }
        }

        private bool ProcessFileCommandCanExcute(object obj)
        {
            return true;
        }
   

        private bool CloseCommandCanExcute(object obj)
        {
            return true;
        }

        private void CloseCommandExecuted(object obj)
        {
            if (View != null)
            {
                View.Close();
            }
        }


        //private  void FolderScan(string directory, MainWindowDataModel dataModel)
        //{
        //    foreach (string f in Directory.GetFiles(directory,))
        //    {
        //        var file = new BusinessEntities.WindowFile { FilePath = f };
        //        file.Architecture = GetAssemblyArchitecture(f);
        //        dataModel.WindowFiles.Add(file);
        //    }
            
        //    foreach (string d in Directory.GetDirectories(directory))
        //    {
        //        FolderScan(d, dataModel);
        //    }
        //}


        private ProcessorArchitecture? GetAssemblyArchitecture(string filePath)
        {
            try
            {
                var assemblyName = AssemblyName.GetAssemblyName(filePath);


                return assemblyName.ProcessorArchitecture;
            }
            catch (Exception)
            {

                return null;
            }

        }


    }
}
