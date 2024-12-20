

# GetSize(obj, vec3, vec3)

Gets the size of [object](#object).

```
void getSize(this obj object, out vec3 min, out vec3 max)
```

## Parameters

#### `object`
Type: [obj](/MdDocs/Types/Obj.md)

#### `min`
Modifiers: [out](/MdDocs/Modifiers/Out.md)

Type: [vec3](/MdDocs/Types/Vec3.md)

Distance from the center of [object](#object) to the negative edge.

#### `max`
Modifiers: [out](/MdDocs/Modifiers/Out.md)

Type: [vec3](/MdDocs/Types/Vec3.md)

Distance from the center of [object](#object) to the positive edge.

## Remarks

 - Size is measured in blocks, not in voxels.

## Examples

``` fcs
// to get the total size of the object, you can do this:
object obj
obj.getSize(out inline vec3 min, out inline vec3 max)
vec3 totalSize = max - min
```


