using System.IO;
using System.Web;
using ServiceStack.Server;
using ServiceStack.Support.WebHost;
using ServiceStack.Text;
using ServiceStack.Utils;
using ServiceStack.WebHost.HttpListener;

namespace ServiceStack
{
	public static class FileExtensions
	{
		public static void SaveTo(this IFile file, string filePath)
		{
			using (var sw = new StreamWriter(filePath, false))
			{
				file.InputStream.WriteTo(sw.BaseStream);
			}
		}

		public static void WriteTo(this IFile file, Stream stream)
		{
			file.InputStream.WriteTo(stream);
		}

		public static string MapServerPath(this string relativePath)
		{
			var isAspNetHost = HttpListenerBase.Instance == null || HttpContext.Current != null;
			var appHost = EndpointHost.AppHost;
			if (appHost != null)
			{
				isAspNetHost = !(appHost is HttpListenerBase);
			}

			return isAspNetHost
			       ? relativePath.MapHostAbsolutePath()
			       : relativePath.MapAbsolutePath();
		}

		public static bool IsRelativePath(this string relativeOrAbsolutePath)
		{
			return !relativeOrAbsolutePath.Contains(":")
				&& !relativeOrAbsolutePath.StartsWith("/") 
				&& !relativeOrAbsolutePath.StartsWith("\\");
		}

        public static byte[] ReadFully(this FileInfo file)
        {
            using (var fs = file.OpenRead())
            {
                return fs.ReadFully();
            }
        }
	}
}