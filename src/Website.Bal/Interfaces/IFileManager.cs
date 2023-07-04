using Website.Entity.Models;
using Website.Shared.Common;

namespace Website.Bal.Interfaces
{
    public interface IFileManager
    {
        string BuildFileContent(string input, CoreEnum.Folder folder);
        FileModel Upload(FileModel file, CoreEnum.Folder folder);
    }
}
