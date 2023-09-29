namespace MSuhininTestovoe.B2B
{
    public interface IActor
    {
        int Entity { get; }
        void Handle();
        void AddEntity(int entity);
    }
}