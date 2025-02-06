extends CharacterBody2D



var speed = 100
var direction = Vector2.LEFT
var health := 20 

func _process(delta):
	move_and_slide(direction * speed)


func take_damage(amount):
	health -= amount
	if health <= 0:
		queue_free(	)
