namespace PKHeX.Godot.Scripts.CurrentPokemon;

public partial class CurrentPokemonEditor : Control
{
    private Application _application = null!;
    private Dictionary<byte, PackedScene> _pokemonEditorScenes = null!;
    private const string PokemonEditorSceneName = "PokemonEditor";

    public override void _Ready()
    {
        _application = GetNode<Application>(Application.NodePath);
        _application.FileLoaded += OnFileLoaded;

        _pokemonEditorScenes = new()
        {
            [1] = ResourceLoader.Load<PackedScene>(Scenes.PokemonEditor.PK1),
            [2] = ResourceLoader.Load<PackedScene>(Scenes.PokemonEditor.PK2),
            // [3] = ResourceLoader.Load<PackedScene>(Scenes.PokemonEditor.PK3),
            // [4] = ResourceLoader.Load<PackedScene>(Scenes.PokemonEditor.PK4),
            // [5] = ResourceLoader.Load<PackedScene>(Scenes.PokemonEditor.PK5),
            // [6] = ResourceLoader.Load<PackedScene>(Scenes.PokemonEditor.PK6),
            // [7] = ResourceLoader.Load<PackedScene>(Scenes.PokemonEditor.PK7),
            [8] = ResourceLoader.Load<PackedScene>(Scenes.PokemonEditor.PK8),
            [9] = ResourceLoader.Load<PackedScene>(Scenes.PokemonEditor.PK9),
        };
    }

    private void OnFileLoaded()
    {
        if (_application.Game is null)
            return;

        var generation = _application.Game.Generation;

        if (!_pokemonEditorScenes.TryGetValue(generation, out var scene))
            throw new NotImplementedException($"No support for generation {generation} games");

        var currentNode = GetNodeOrNull<Node>(PokemonEditorSceneName);
        currentNode?.QueueFree();

        var newEditor = scene.Instantiate<Control>();
        newEditor.Name = PokemonEditorSceneName;
        newEditor.SetAnchorsPreset(LayoutPreset.FullRect);

        AddChild(newEditor);
    }
}
