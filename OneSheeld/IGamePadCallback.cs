namespace OneSheeldClasses
{
    public interface IGamePadCallback
    {
        void OnButtonChange(bool up, bool down, bool left, bool right, bool orange, bool red, bool green, bool blue);
    }
}
