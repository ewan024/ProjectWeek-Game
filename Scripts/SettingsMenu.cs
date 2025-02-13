namespace HaloHell.Scripts;

using Godot;

public partial class SettingsMenu : Control
{
	private Button _backButton;
	private HSlider _masterSlider;
	private HSlider _musicSlider;
	private VBoxContainer _menu;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_backButton = GetNode<Button>("Back");
		_masterSlider = GetNode<HSlider>("MasterVolume");
		_musicSlider = GetNode<HSlider>("MusicVolume");
		_menu = GetNode<VBoxContainer>("../Menu");
		
		float masterVolume = AudioServer.GetBusVolumeDb(AudioServer.GetBusIndex("Master"));
		_masterSlider.Value = DbToLinear(masterVolume);
		
		float musicVolume = AudioServer.GetBusVolumeDb(AudioServer.GetBusIndex("Music"));
		_masterSlider.Value = DbToLinear(musicVolume);
		
		_backButton.Pressed += _OnBackPressed;
		_masterSlider.ValueChanged += OnMasterChanged;
		_musicSlider.ValueChanged += OnMusicChanged;
	}
	

	private void _OnBackPressed()
	{
		Visible = false;
		_menu.Visible = true;
	}
	
	private void OnMasterChanged(double value)
	{
		float db = LinearToDb((float)value);
		AudioServer.SetBusVolumeDb(AudioServer.GetBusIndex("Master"), db);
	}

	private void OnMusicChanged(double value)
	{
		float db = LinearToDb((float)value);
		AudioServer.SetBusVolumeDb(AudioServer.GetBusIndex("Music"), db);
	}
	
	private static float LinearToDb(float linear)
	{
		return linear > 0f ? 20f * Mathf.Log(linear) / Mathf.Log(10f) : -80f; // Prevent log(0) errors
	}
	
	private static float DbToLinear(float db)
	{
		return Mathf.Pow(10f, db / 20f);
	}
}
