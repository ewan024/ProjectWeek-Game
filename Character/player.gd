extends CharacterBody2D


@export var SPEED : float = 200.0
@export var JUMP_VELOCITY : float = -300.0
@export var DOUBLE_JUMP_VELOCITY : float = -100

var gravity = ProjectSettings.get_setting("physics/2d/default_gravity")
var has_double_jumped : bool = false


func _physics_process(delta: float) -> void:
	# Add the gravity.
	if not is_on_floor():
		velocity += get_gravity() * delta

	# Handle jump.
	if Input.is_action_just_pressed("jump"):
		if is_on_floor():
			velocity.y = JUMP_VELOCITY
		elif not has_double_jumped:
			

	# Get the input direction and handle the movement/deceleration.
	# As good practice, you should replace UI actions with custom gameplay actions.
	var direction = Input.get_axis("left", "right")
	if direction:
		velocity.x = direction * SPEED
	else:
		velocity.x = move_toward(velocity.x, 0, SPEED)

	move_and_slide()
