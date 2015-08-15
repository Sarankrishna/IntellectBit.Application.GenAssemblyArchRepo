using IntellectBit.App.GenAssemblyArchRepo.DataModel;
using System.Reflection;

namespace IntellectBit.App.GenAssemblyArchRepo.BusinessEntities
{
    public class WindowFile : DataModelBase
    {
        public string FilePath { get; set; }

        public ProcessorArchitecture? Architecture { get;set; }
    }
}
