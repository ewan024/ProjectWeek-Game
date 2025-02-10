namespace HaloHell.Scripts;

using Godot;

public partial class SettingsMenu : Control
{
	private AudioStreamPlayer2D _audioPlayer;
	private Button _backButton;
	private HSlider _volumeSlider;
	private Control _menu;

	private Button _button;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_backButton = GetNode<Button>("Back");
		_volumeSlider = GetNode<HSlider>("Volume");
		_menu = GetNode<Control>("../Menu");
		
		float currentVolume = AudioServer.GetBusVolumeDb(AudioServer.GetBusIndex("Master"));
		_volumeSlider.Value = DbToLinear(currentVolume);

		_backButton.Pressed += _OnBackPressed;
		_volumeSlider.ValueChanged += _OnVolumeChanged;
		
		_button = GetNode<Button>("Button");
		_audioPlayer = GetNode<AudioStreamPlayer2D>("AudioStreamPlayer2D");
		_button.Pressed += _OnButtonPressed;
	}
	

	public void _OnBackPressed()
	{
		Visible = false;
		_menu.Visible = true;
	}
	
	public void _OnButtonPressed()
	{
		_audioPlayer.Play();
	}
	
	private void _OnVolumeChanged(double value)
	{
		// Convert linear value (0 to 1) to decibels and set volume
		float db = LinearToDb((float)value);
		AudioServer.SetBusVolumeDb(AudioServer.GetBusIndex("Master"), db);
	}
	
	private float LinearToDb(float linear)
	{
		return linear > 0f ? 20f * Mathf.Log(linear) / Mathf.Log(10f) : -80f; // Prevent log(0) errors
	}
	
	private float DbToLinear(float db)
	{
		return Mathf.Pow(10f, db / 20f);
	}
}
