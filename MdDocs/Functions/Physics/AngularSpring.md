# AngularSpring(constr, vec3, vec3)

Makes the constraint springy, [angularLimits(constr,vec3,vec3)](/MdDocs/Functions/Physics/AngularLimits.md) must be called before for angular spring to work.

```
void angularSpring(this constr constraint, vec3 stiffness, vec3 damping)
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

 - [addConstraint(obj,obj,vec3,constr)](/MdDocs/Functions/Physics/AddConstraint.md)

