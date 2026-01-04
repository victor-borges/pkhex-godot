using Godot;

namespace PKHeX.Godot.Scripts;

public partial class SignalBus : Node
{
    [Signal] public delegate void FileLoadedEventHandler(string path);
    [Signal] public delegate void BoxChangedEventHandler(int boxIndex);
    [Signal] public delegate void BoxPokemonSelectedEventHandler(int slotIndex);
    [Signal] public delegate void CurrentPokemonChangedEventHandler();
}
