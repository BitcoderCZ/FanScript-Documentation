

# LinearSpring(constr, vec3, vec3)

Makes the constraint springy, [linearLimits(constr,vec3,vec3)](/MdDocs/Functions/Physics/LinearLimits.constr.vec3.vec3.md) must be called before for linear spring to work.

```
void linearSpring(this constr constraint, vec3 stiffness, vec3 damping)
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

 - [addConstraint(obj,obj,vec3,constr)](/MdDocs/Functions/Physics/AddConstraint.obj.obj.vec3.constr.md)


