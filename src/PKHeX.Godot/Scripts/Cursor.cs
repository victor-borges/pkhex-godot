using System;
using Godot;
using PKHeX.Godot.Scripts.Contants;

namespace PKHeX.Godot.Scripts;

public partial class Cursor: Node
{
    private readonly Vector2 _baseResolution = new(1280, 720);

    public override void _Ready()
    {
        GetViewport().SizeChanged += OnSizeChanged;
        SetCursors(1f);
    }

    private void OnSizeChanged()
    {
        var size = GetTree().GetRoot().Size;
        var scale = MathF.Min(size.X / _baseResolution.X, size.Y / _baseResolution.Y);
        SetCursors(scale);
    }

    private static void SetCursors(float scaleFactor)
    {
        SetMouseCursor(Resources.Cursors.Arrow, Input.CursorShape.Arrow, new Vector2(8, 6), scaleFactor);
        SetMouseCursor(Resources.Cursors.Beam, Input.CursorShape.Ibeam, new Vector2(16, 16), scaleFactor);
        SetMouseCursor(Resources.Cursors.PointingHand, Input.CursorShape.PointingHand, new Vector2(8, 6), scaleFactor);
        SetMouseCursor(Resources.Cursors.Cross, Input.CursorShape.Cross, new Vector2(16, 16), scaleFactor);
        SetMouseCursor(Resources.Cursors.Drag, Input.CursorShape.Drag, new Vector2(16, 16), scaleFactor);
        SetMouseCursor(Resources.Cursors.CanDrop, Input.CursorShape.CanDrop, new Vector2(16, 16), scaleFactor);
        SetMouseCursor(Resources.Cursors.Forbidden, Input.CursorShape.Forbidden, new Vector2(16, 16), scaleFactor);
        SetMouseCursor(Resources.Cursors.ResizeVertical, Input.CursorShape.Vsize, new Vector2(16, 16), scaleFactor);
        SetMouseCursor(Resources.Cursors.ResizeHorizontal, Input.CursorShape.Hsize, new Vector2(16, 16), scaleFactor);
        SetMouseCursor(Resources.Cursors.ResizeMainDiagonal, Input.CursorShape.Fdiagsize, new Vector2(16, 16), scaleFactor);
        SetMouseCursor(Resources.Cursors.ResizeSecondayDiagonal, Input.CursorShape.Bdiagsize, new Vector2(16, 16), scaleFactor);
        SetMouseCursor(Resources.Cursors.Move, Input.CursorShape.Move, new Vector2(16, 16), scaleFactor);
        SetMouseCursor(Resources.Cursors.Help, Input.CursorShape.Help, new Vector2(5, 1), scaleFactor);
        SetMouseCursor(Resources.Cursors.SplitVertical, Input.CursorShape.Vsplit, new Vector2(16, 16), scaleFactor);
        SetMouseCursor(Resources.Cursors.SplitHorizontal, Input.CursorShape.Hsplit, new Vector2(16, 16), scaleFactor);

        static void SetMouseCursor(string cursorPath, Input.CursorShape cursorShape, Vector2 hotSpot, float scaleFactor)
        {
            var texture = GD.Load<DpiTexture>(cursorPath);
            texture.BaseScale = scaleFactor * 1.5f;

            Input.SetCustomMouseCursor(texture, cursorShape, hotSpot * scaleFactor * 1.5f);
        }
    }
}
