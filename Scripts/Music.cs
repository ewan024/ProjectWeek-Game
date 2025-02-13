namespace HaloHell.Scripts;

using Godot;

public partial class Music : Control
{
	private AudioStreamPlayer2D _audioPlayer;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_audioPlayer = GetNode<AudioStreamPlayer2D>("BGMusic");
		_audioPlayer.Play();
	}
}
