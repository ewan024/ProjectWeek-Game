using Godot;

public partial class Enemy : CharacterBody2D
{
	public int Health = 100;
	public float Speed = 50f;
	public float Gravity = 800f;
	public float AttackRange = 40f;
	public int Damage = 25;

	private Player _player;
	private AnimatedSprite2D _anim;
	private Timer _damageTimer;

	public override void _Ready()
	{
		_anim = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
		_player = GetNode<CharacterBody2D>("../Player") as Player;
		_damageTimer = GetNode<Timer>("DamageTimer");
	}

	public override void _PhysicsProcess(double delta)
	{
		Vector2 velocity = Velocity;
		
		if (!IsOnFloor()) 
			velocity.Y += Gravity * (float)delta;
		
		if (_player == null) return;

		float distanceToPlayer = GlobalPosition.DistanceTo(_player.GlobalPosition);

		if (distanceToPlayer < AttackRange)
		{
			Attack();
		}
		else 
		{
			Vector2 direction = (_player.GlobalPosition - GlobalPosition).Normalized();
			velocity.X = direction.X * Speed;
			MoveTowardsPlayer();
		}
		
		Velocity = velocity;
		MoveAndSlide();
	}

	private void MoveTowardsPlayer()
	{
		_anim.Play("walk");
	}

	public void Attack()
	{
		_anim.Play("attack");

		if (_damageTimer.IsStopped())
		{
			_player.Health -= Damage;
			_damageTimer.Start();
		}
	}

	public void TakeDamage(int damage)
	{
		Health -= damage;

		if (Health <= 0)
		{
			_anim.Play("die");
			GetTree().CreateTimer(0.5f).Timeout += QueueFree;
		}
		else
		{
			_anim.Play("hurt");
		}
	}
}
