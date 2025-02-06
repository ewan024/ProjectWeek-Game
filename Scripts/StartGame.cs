namespace HaloHell.Scripts;

using Godot;	

public partial class StartGame : VBoxContainer
{
	private Button _playButton;
	private Button _settingsButton;
	private Button _quitButton;
	private Control _settingsMenu;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_playButton = GetNode<Button>("Play");
		_playButton.Pressed += _OnPlayPressed;
		
		_settingsButton = GetNode<Button>("Settings");
		_settingsButton.Pressed += _OnSettingsPressed;

		_settingsMenu = GetNode<Control>("../SettingsMenu");
		_settingsMenu.Visible = false;
		
		_quitButton = GetNode<Button>("Quit");
		_quitButton.Pressed += _OnQuitPressed;
	}

	private void _OnPlayPressed()
	{
		GetTree().ChangeSceneToFile("res://Scenes/MainScene.tscn");
	}

	private void _OnSettingsPressed()
	{
		Visible = false;
		_settingsMenu.Visible = true;
	}
	
	private void _OnQuitPressed()
	{
		GetTree().Quit();
	}
}
