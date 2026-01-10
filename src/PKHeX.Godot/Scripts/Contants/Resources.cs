namespace PKHeX.Godot.Scripts.Contants;

public static class Resources
{
    public static class Cursors
    {
        public const string Arrow = "res://Assets/Cursor/pointer_b.svg";
        public const string PointingHand = "res://Assets/Cursor/hand_small_point.svg";
        public const string Beam = "res://Assets/Cursor/bracket_b_vertical.svg";
        public const string Cross = "res://Assets/Cursor/cross_small.svg";
        public const string Wait = "res://Assets/Cursor/.svg";
    }

    public static class Sprites
    {
        public static string HeldItem(int id) => $"res://Assets/Sprites/Items/item_{id:D4}.png";

        public static class Overlays
        {
            public const string Shiny = "res://Assets/Sprites/Overlays/shiny.png";
            public const string ShinySquare = "res://Assets/Sprites/Overlays/shiny-square.png";
            public const string Alpha = "res://Assets/Sprites/Overlays/alpha.webp";
        }
    }
}
