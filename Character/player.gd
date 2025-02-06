extends CharacterBody2D


@export var NORMALSPEED : float = 200.0
@export var JUMP_VELOCITY : float = -250.0
@export var attack_cooldown := 0.5
@export var attack_damage := 10

@onready var animated_sprite : AnimatedSprite2D = $AnimatedSprite2D
@onready var attack_hitbox = $AttackHitbox
@onready var dash = $Dash

var gravity = ProjectSettings.get_setting("physics/2d/default_gravity")
var animation_locked : bool = false
var direction : Vector2 = Vector2.ZERO
var can_attack := true

const dashspeed = 800
const dashlength = .1


func _physics_process(delta):
	# Add the gravity.
	if not is_on_floor():
		velocity.y += gravity * delta
		
	# Handle jump.
	if Input.is_action_just_pressed("jump") and is_on_floor():
		velocity.y = JUMP_VELOCITY
		
	if Input.is_action_just_pressed("up") and is_on_floor():
		velocity.y = JUMP_VELOCITY

	if Input.is_action_just_pressed("dash"):
		dash.start_dash(dashlength)
	var SPEED = dashspeed if dash.is_dashing() else NORMALSPEED

	direction = Input.get_vector("left", "right", "up", "down")
	if direction:
		velocity.x = direction.x * SPEED
	else:
		velocity.x = move_toward(velocity.x, 0, SPEED)

	move_and_slide()
	update_animation()
	update_facing_directions()

func update_animation():
	if not animation_locked:
		if direction.x != 0:
			animated_sprite.play("run")
		else:
			animated_sprite.play("idle")
			
func update_facing_directions():
	if direction.x > 0:
		animated_sprite.flip_h = true
	elif direction.x < 0:
		animated_sprite.flip_h = false
		
func _input(event):
	if event.is_action_pressed("attack") and can_attack:
		attack()
		
func attack():
	can_attack = false
	animated_sprite.play("attack")
	attack_hitbox.monitoring = true
	await animated_sprite.animation_finished
	attack_hitbox.monitoring = false
	await get_tree().create_timer(attack_cooldown).timeout
	can_attack = true
	
func _on_Area2D_body_entered(body):
	if body.is_in_group("enemies"):
		body.take_damage(10)
