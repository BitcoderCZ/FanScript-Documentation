# Collision(object, object, float, vec3)

Executes when [object2](#object2) collides with [object1](#object1).

```
on collision(object object1, out object object2, out float impulse, out vec3 normal) { }
```

## Parameters

#### `object1`
Type: object

The object to detect collisions for.

#### `object2`
Type: object

The object which [object1](#object1) collided with.

#### `impulse`
Type: float

Impact force of the collision.

#### `normal`
Type: vec3

Direction of impact from [object2](#object2) to [object1](#object1).

## Remarks

- If [object1](#object1) is colliding with multiple objects the most forceful collision will be reported.
- If you're overriding physics to move objects using Set Position then no collisions will occur.

