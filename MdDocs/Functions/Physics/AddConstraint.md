# AddConstraint(obj, obj, vec3, constr)

Creates a [constraint](#constraint) (an invisible connection between 2 objects) between [part](#part) and [base](#base).

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
Type: [constr](/MdDocs/Types/Constr.md)

The created constraint.

## Related

 - [linearLimits(constr,vec3,vec3)](/MdDocs/Functions/Physics/LinearLimits.md)
 - [angularLimits(constr,vec3,vec3)](/MdDocs/Functions/Physics/AngularLimits.md)
 - [linearSpring(constr,vec3,vec3)](/MdDocs/Functions/Physics/LinearSpring.md)
 - [angularSpring(constr,vec3,vec3)](/MdDocs/Functions/Physics/AngularSpring.md)
 - [linearMotor(constr,vec3,vec3)](/MdDocs/Functions/Physics/LinearMotor.md)
 - [angularMotor(constr,vec3,vec3)](/MdDocs/Functions/Physics/AngularMotor.md)

