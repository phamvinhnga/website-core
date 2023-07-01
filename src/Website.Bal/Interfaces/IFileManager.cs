using Website.Entity.Model;
using Website.Shared.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Website.Bal.Interfaces
{
    public interface IFileManager
    {
        string BuildFileContent(string input, CoreEnum.Folder folder);
        FileModel Upload(FileModel file, CoreEnum.Folder folder);
    }
}
