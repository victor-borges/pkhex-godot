using System;
using Godot;

namespace PKHeX.Godot.Scripts;

public partial class Cursor: Node
{
    private static readonly Vector2 BaseResolution = new(1280, 720);

    public override void _Ready()
    {
        // GetViewport().SizeChanged += OnSizeChanged;
        SetCursors(2f);
    }

    private void OnSizeChanged()
    {
        var size = GetTree().GetRoot().Size;
        var scale = MathF.Min(size.X / BaseResolution.X, size.Y / BaseResolution.Y);
        SetCursors(scale);
    }

    private static void SetCursors(float scaleFactor)
    {
        SetMouseCursor(Assets.Cursors.Arrow, Input.CursorShape.Arrow, new Vector2(8, 6), scaleFactor);
        SetMouseCursor(Assets.Cursors.Beam, Input.CursorShape.Ibeam, new Vector2(16, 16), scaleFactor * 0.8f);
        SetMouseCursor(Assets.Cursors.PointingHand, Input.CursorShape.PointingHand, new Vector2(8, 6), scaleFactor);
        SetMouseCursor(Assets.Cursors.Cross, Input.CursorShape.Cross, new Vector2(16, 16), scaleFactor);
        SetMouseCursor(Assets.Cursors.Drag, Input.CursorShape.Drag, new Vector2(16, 16), scaleFactor);
        SetMouseCursor(Assets.Cursors.CanDrop, Input.CursorShape.CanDrop, new Vector2(16, 16), scaleFactor);
        SetMouseCursor(Assets.Cursors.Forbidden, Input.CursorShape.Forbidden, new Vector2(16, 16), scaleFactor);
        SetMouseCursor(Assets.Cursors.ResizeVertical, Input.CursorShape.Vsize, new Vector2(16, 16), scaleFactor);
        SetMouseCursor(Assets.Cursors.ResizeHorizontal, Input.CursorShape.Hsize, new Vector2(16, 16), scaleFactor);
        SetMouseCursor(Assets.Cursors.ResizeMainDiagonal, Input.CursorShape.Fdiagsize, new Vector2(16, 16), scaleFactor);
        SetMouseCursor(Assets.Cursors.ResizeSecondaryDiagonal, Input.CursorShape.Bdiagsize, new Vector2(16, 16), scaleFactor);
        SetMouseCursor(Assets.Cursors.Move, Input.CursorShape.Move, new Vector2(16, 16), scaleFactor);
        SetMouseCursor(Assets.Cursors.Help, Input.CursorShape.Help, new Vector2(5, 1), scaleFactor);
        SetMouseCursor(Assets.Cursors.SplitVertical, Input.CursorShape.Vsplit, new Vector2(16, 16), scaleFactor);
        SetMouseCursor(Assets.Cursors.SplitHorizontal, Input.CursorShape.Hsplit, new Vector2(16, 16), scaleFactor);
    }

    private static void SetMouseCursor(string cursorPath, Input.CursorShape cursorShape, Vector2 hotSpot, float scaleFactor)
    {
        var texture = GD.Load<DpiTexture>(cursorPath);
        texture.BaseScale = scaleFactor;

        Input.SetCustomMouseCursor(texture, cursorShape, hotSpot * scaleFactor);
    }
}
