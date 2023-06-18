using System.IO;

namespace Cogwheel.Utils;

internal static class FileEx
{
    public static bool TryDelete(string filePath)
    {
        try
        {
            File.Delete(filePath);
            return true;
        }
        catch
        {
            return false;
        }
    }
}