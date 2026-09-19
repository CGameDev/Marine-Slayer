using System.IO;

namespace MarineSlayer.Save
{
    public sealed class DevelopmentFileBackend : ISaveBackend
    {
        private readonly string path;
        public DevelopmentFileBackend(string path) { this.path = path; }
        public bool Exists { get { return File.Exists(path); } }
        public string Read() { return File.ReadAllText(path); }
        public void Write(string json) { File.WriteAllText(path, json); }
    }
}
