using System.IO;
using System.Text;

namespace LogLibrary
{
    public class FileContentWriter : BaseContentWriter
    {
        private string _file_name;

        public FileContentWriter(string name)
        {
            _file_name = name;
        }

        public override bool WriteToMedia(string logcontent)
        {
            using (FileStream sourceStream = File.Open(_file_name, FileMode.Append))
            {
                byte[] buffer = Encoding.UTF8.GetBytes(logcontent + "\r\n");

                sourceStream.Write(buffer, 0, buffer.Length);
            }

            return true;
        }
    }
}
