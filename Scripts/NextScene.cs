using Godot;

public partial class NextScene : Area2D
{
	private Player _player;
	private Area2D _nextScene;
	public override void _Ready()
	{
		_player = GetNode<CharacterBody2D>("../Player") as Player;
	}

	public override void _Process(double delta)
	{
		CheckPlayer();
	}

	private void CheckPlayer()
	{
		var bodies = GetOverlappingBodies();
		GD.Print("bodies: " + bodies.Count + "BodyName: " + bodies);
		
		foreach (Node2D body in bodies)
		{
			GD.Print(body.Name);
			if (body is Player && Input.IsActionJustPressed("up"))
			{
				GetTree().ChangeSceneToFile("res://Scenes/MainGame2.tscn");
			}
		}
	}
}
