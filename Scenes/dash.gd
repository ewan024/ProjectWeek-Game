extends Node2D

@onready var timer: Timer = %dashtimer  # Use % for scene-unique names in Godot 4

func start_dash(dur: float) -> void:
	timer.wait_time = dur
	timer.start()
	
func is_dashing() -> bool:
	return not timer.is_stopped()
