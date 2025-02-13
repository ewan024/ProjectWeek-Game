using Godot;

public partial class Player : CharacterBody2D
{
	// Movement variables
	public float Speed = 200f;
	public float JumpForce = -300f;
	public float Gravity = 800f;
	public int Health = 100;
	public int Damage = 50;

	// Attack variables
	private bool _canAttack = true;

	// Nodes
	private AnimationPlayer anim;
	private Timer attackTimer;
	private AnimatedSprite2D sprite;
	private Area2D _attackArea;

	public override void _Ready()
	{
		// Get references to nodes
		attackTimer = GetNode<Timer>("Timer");
		sprite = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
		attackTimer.Timeout += OnAttackCooldown;
		_attackArea = GetNode<Area2D>("Aura");
	}

	public override void _PhysicsProcess(double delta)
	{
		Vector2 velocity = Velocity;

		// Apply gravity
		if (!IsOnFloor())
			velocity.Y += Gravity * (float)delta;

		CheckIfDead();

		Attack();

		// Movement
		float direction = Input.GetAxis("left", "right");
		if (direction != 0 && _canAttack)
		{
			velocity.X = direction * Speed;
			sprite.FlipH = direction < 0;
			sprite.Play("walk");
		}
		else if (_canAttack)
		{
			velocity.X = 0;
			sprite.Play("idle");
		}
		else
		{
			velocity.X = 0;
		}

		// Jumping
		if (Input.IsActionJustPressed("jump") && IsOnFloor())
			velocity.Y = JumpForce;

		// Apply movement
		Velocity = velocity;
		MoveAndSlide();
	}

	public void CheckIfDead()
	{
		if (Health <= 0)
		{
			GetTree().CreateTimer(0.5f).Timeout += QueueFree;
			GetTree().ChangeSceneToFile("res://Scenes/DeathScreen.tscn");
		}
		else
		{
			sprite.Play("hurt");
		}
	}

	private void OnAttackCooldown()
	{
		_canAttack = true;
	}

	private void Attack()
	{
		var bodies = _attackArea.GetOverlappingBodies();

		if (Input.IsActionJustPressed("attack") && _canAttack)
		{
			sprite.Play("attack");
			foreach (Node2D body in bodies)
			{
				if (body is Enemy enemy)
				{
					enemy.TakeDamage(Damage);
				}
			}

			_canAttack = false;
			attackTimer.Start(0.5f);
		}
	}
	
}
