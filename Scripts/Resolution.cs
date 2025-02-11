namespace HaloHell.Scripts;

using Godot;

public partial class Resolution : OptionButton
{
	private Button _applyButton;
	
	// List of predefined resolutions (width, height)
	private readonly (int, int)[] _resolutions = 
	{
		(1280, 720),
		(1920, 1080),
		(2560, 1600),
		(2560, 1440),
		(3840, 2160)
	};
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		DisplayServer.WindowSetSize(new Vector2I(1920, 1080));
		GD.Print("Default resolution set to: 1920x1080");
		
		// Initialize node
		_applyButton = GetNode<Button>("../Confirm");

		// Add resolutions to dropdown menu
		for (int i = 0; i < _resolutions.Length; i++)
		{
			AddItem($"{_resolutions[i].Item1} x {_resolutions[i].Item2}");
		}

		Selected = 1;

		// Connect button press
		_applyButton.Pressed += _OnApplyPressed;
	}

	// Apply the resolution selected from the dropdown
	private void _OnApplyPressed()
	{
		int selectedIndex = Selected;
		
		if (selectedIndex >= 0 && selectedIndex < _resolutions.Length)
		{
			// Set the resolution based on dropdown choice
			int width = _resolutions[selectedIndex].Item1;
			int height = _resolutions[selectedIndex].Item2;

			// Set the new resolution
			DisplayServer.WindowSetSize(new Vector2I(width, height));
			GD.Print($"Resolution set to: {width} x {height}");
		}
	}
}
