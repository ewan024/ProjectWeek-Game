extends CharacterBody2D


var health := 20 

func take_damage(amount):
	health -= amount
	if health <= 0:
		queue_free()
