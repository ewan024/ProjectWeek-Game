using Godot;

public partial class FullScreen : CheckBox
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Toggled += _OnFullScreenToggled;
	}

	private void _OnFullScreenToggled(bool toggled)
	{
		DisplayServer.WindowSetMode(toggled ? DisplayServer.WindowMode.Fullscreen : DisplayServer.WindowMode.Windowed);
	}
}
