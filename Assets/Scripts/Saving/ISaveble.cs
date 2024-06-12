namespace RPG.Saving 
{
    public interface ISaveble 
    {
        object CaptureState();

        void RestoreState(object state);
    }
}