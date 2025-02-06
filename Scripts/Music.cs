using Godot;
using System;

public partial class Music : Control
{
	private AudioStreamPlayer2D _audioPlayer;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_audioPlayer = GetNode<AudioStreamPlayer2D>("AudioStreamPlayer2D");
		_audioPlayer.Play();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
