

# AddConstraint(obj, obj, vec3, constr)

Creates a [constraint](#constraint) (an invisible connection (rod) between 2 objects) between [part](#part) and [base](#base).

```
void addConstraint(this obj base, obj part, vec3 pivot, out constr constraint)
```

## Parameters

#### `base`
Type: [obj](/MdDocs/Types/Obj.md)

The object that will be glued on.

#### `part`
Type: [obj](/MdDocs/Types/Obj.md)

The object that will be glued on the [base](#base).

#### `pivot`
Type: [vec3](/MdDocs/Types/Vec3.md)

The other end of the constraint rod.

#### `constraint`
Modifiers: [out](/MdDocs/Modifiers/Out.md)

Type: [constr](/MdDocs/Types/Constr.md)

The created constraint.

## Related

 - [linearLimits(constr,vec3,vec3)](/MdDocs/Functions/LinearLimits.constr.vec3.vec3.md)
 - [angularLimits(constr,vec3,vec3)](/MdDocs/Functions/AngularLimits.constr.vec3.vec3.md)
 - [linearSpring(constr,vec3,vec3)](/MdDocs/Functions/LinearSpring.constr.vec3.vec3.md)
 - [angularSpring(constr,vec3,vec3)](/MdDocs/Functions/AngularSpring.constr.vec3.vec3.md)
 - [linearMotor(constr,vec3,vec3)](/MdDocs/Functions/LinearMotor.constr.vec3.vec3.md)
 - [angularMotor(constr,vec3,vec3)](/MdDocs/Functions/AngularMotor.constr.vec3.vec3.md)


