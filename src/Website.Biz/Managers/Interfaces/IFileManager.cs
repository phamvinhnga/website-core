using Website.Entity.Model;
using Website.Shared.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Website.Biz.Managers.Interfaces
{
    public interface IFileManager
    {
        string BuildFileContent(string input, CoreEnum.Folder folder);
        FileModel Upload(FileModel file, CoreEnum.Folder folder);
    }
}
