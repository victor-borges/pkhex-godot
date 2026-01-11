namespace PKHeX.Godot.Scripts.Contants;

public static class Resources
{
    public static class Cursors
    {
        public const string Arrow = "res://Assets/Cursor/pointer_b.svg";
        public const string PointingHand = "res://Assets/Cursor/hand_small_point.svg";
        public const string Beam = "res://Assets/Cursor/bracket_b_vertical.svg";
        public const string Cross = "res://Assets/Cursor/cross_small.svg";
        public const string Drag = "res://Assets/Cursor/hand_small_open.svg";
        public const string CanDrop = "res://Assets/Cursor/hand_small_closed.svg";
        public const string Forbidden = "res://Assets/Cursor/disabled.svg";
        public const string ResizeVertical = "res://Assets/Cursor/resize_b_vertical.svg";
        public const string ResizeHorizontal = "res://Assets/Cursor/resize_b_horizontal.svg";
        public const string ResizeMainDiagonal = "res://Assets/Cursor/resize_b_diagonal_mirror.svg";
        public const string ResizeSecondayDiagonal = "res://Assets/Cursor/resize_b_diagonal.svg";
        public const string Move = "res://Assets/Cursor/resize_b_cross.svg";
        public const string Help = "res://Assets/Cursor/mark_question_pointer_b.svg";
        public const string SplitVertical = "res://Assets/Cursor/resize_vertical.svg";
        public const string SplitHorizontal = "res://Assets/Cursor/resize_horizontal.svg";
    }

    public static class Sprites
    {
        public const string Egg = "res://Assets/Sprites/Pokemon/0.png";

        public static string HeldItem(int id) => $"res://Assets/Sprites/Items/item_{id:D4}.png";

        public static class Overlays
        {
            public const string Shiny = "res://Assets/Sprites/Overlays/shiny.png";
            public const string ShinySquare = "res://Assets/Sprites/Overlays/shiny-square.png";
            public const string Alpha = "res://Assets/Sprites/Overlays/alpha.webp";
        }
    }
}
