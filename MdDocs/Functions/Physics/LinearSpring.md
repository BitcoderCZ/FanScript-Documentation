# LinearSpring(constr, vec3, vec3)

Makes the constraint springy, [linearLimits(constr,vec3,vec3)](/MdDocs/Functions/Physics/LinearLimits.md) must be called before for linear spring to work.

```
void linearSpring(this constr constraint, vec3 stiffness, vec3 damping)
```

## Parameters

#### `constraint`
Type: constr

The constraint to use.

#### `stiffness`
Type: vec3

How stiff the sping will be.

#### `damping`
Type: vec3

How much damping (drag) to apply.

## Related

 - [addConstraint(object,object,vec3,constr)](/MdDocs/Functions/Physics/AddConstraint.md)

