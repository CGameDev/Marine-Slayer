namespace MarineSlayer.Save
{
    public interface ISaveBackend
    {
        bool Exists { get; }
        string Read();
        void Write(string json);
    }
}
