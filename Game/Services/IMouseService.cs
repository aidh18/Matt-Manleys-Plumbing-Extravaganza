using System.Numerics;


namespace Matt_Manleys_Plumbing_Extravaganza.Game.Services
{
    public interface IMouseService
    {
        Vector2 GetCoordinates();
        bool IsButtonDown(MouseButton button);
        bool IsButtonPressed(MouseButton button);
        bool IsButtonReleased(MouseButton button);
        bool IsButtonUp(MouseButton button);
    }
}