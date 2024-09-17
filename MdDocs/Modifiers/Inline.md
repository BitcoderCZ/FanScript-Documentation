# Inline

- When applied to variable - instead of storing a value stores a reference to some code, which is ran every time the variable is accesed.
- When applied to function - inlines the function for every call (by default functions which are called only once are inlined automatically) - replaces calls to this function by the code of this function.

Can be applied to the following variable types:
- [bool](/MdDocs/Types/Bool.md)
- [float](/MdDocs/Types/Float.md)
- [vec3](/MdDocs/Types/Vec3.md)
- [rot](/MdDocs/Types/Rot.md)
- [obj](/MdDocs/Types/Obj.md)
- [constr](/MdDocs/Types/Constr.md)

```
inline
```

## Targets

The modifier can be applied to:
 - Variable
 - Function

## Conflicting modifiers

These modifiers cannot be used with this one:
 - const
 - saved

