namespace PKHeX.Godot.Scripts;

public partial class BoxTab : Control
{
    private Application _application = null!;
    private MarginContainer _marginContainer = null!;
    private const string BoxSceneName = "Box";

    public override void _Ready()
    {
        _application = GetNode<Application>(Application.NodePath);
        _application.FileLoaded += OnFileLoaded;

        _marginContainer = GetNode<MarginContainer>("MarginContainer");
    }

    private void OnFileLoaded()
    {
        if (_application.Game is null)
            return;

        var generation = _application.Game.Generation;
        var scene = generation <= 2
            ? ResourceLoader.Load<PackedScene>(Scenes.Boxes.Box20)
            : ResourceLoader.Load<PackedScene>(Scenes.Boxes.Box);

        var currentNode = GetNodeOrNull<Node>($"MarginContainer/{BoxSceneName}");
        currentNode?.QueueFree();

        var box = scene.Instantiate<Control>();
        box.Name = BoxSceneName;

        _marginContainer.AddChild(box);
    }
}
