using System.IO;

namespace Bodia_benchmark_xamarin
{
    public static class Globals
    {
        public readonly static string UsersDatabaseName = "Users.db3";
        public readonly static string FilesForCompressFolderName = "FilesForCompress";
        public readonly static string UncompressedFilesFolderName = "UncompressedFiles";
        public readonly static string CompressedFilesZipName = "CompressedFiles.zip";
        public readonly static string ApplicationFilesFolder = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
        public readonly static string PathToFilesForCompressFolder = Path.Combine(ApplicationFilesFolder, FilesForCompressFolderName);
        public readonly static string PathToUncompressFilesFolder = Path.Combine(ApplicationFilesFolder, UncompressedFilesFolderName);
        public readonly static string PathToCompressedZipFile = Path.Combine(ApplicationFilesFolder, CompressedFilesZipName);
        
    }
}