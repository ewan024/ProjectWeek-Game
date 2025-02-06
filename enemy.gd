extends CharacterBody2D

class_name KnightEnemy

const speed = 10
var is_knight_chase: bool

var health = 80
var health_max = 80
var health_min = 0

var dead: bool = false
var taking_damage: bool = false
var damage_to_deal = 20
