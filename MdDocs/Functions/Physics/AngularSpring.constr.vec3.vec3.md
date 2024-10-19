

# AngularSpring(constr, vec3, vec3)

Makes the constraint springy, [angularLimits(constr,vec3,vec3)](/MdDocs/Functions/AngularLimits.constr.vec3.vec3.md) must be called before for angular spring to work.

```
void angularSpring(this constr constraint, vec3 stiffness, vec3 damping)
```

## Parameters

#### `constraint`
Type: [constr](/MdDocs/Types/Constr.md)

#### `stiffness`
Type: [vec3](/MdDocs/Types/Vec3.md)

How stiff the sping will be.

#### `damping`
Type: [vec3](/MdDocs/Types/Vec3.md)

How much damping (drag) to apply.

## Related

 - [addConstraint(obj,obj,vec3,constr)](/MdDocs/Functions/AddConstraint.obj.obj.vec3.constr.md)


