# SetVisible(obj, bool)

Sets if [object](#object) is visible and has collision/physics.

```
void setVisible(this obj object, bool visible)
```

## Parameters

#### `object`
Type: [obj](/MdDocs/Types/Obj.md)

#### `visible`
Type: [bool](/MdDocs/Types/Bool.md)

## Remarks

When [object](#object) is set to invisible, all constraints associated with it will be deleted.

## Examples

``` fcs
// Here's how an object can be invisible, while also having physics:
object obj
obj.setVisible(true)
on LateUpdate
{
    obj.setVisible(false)
}
```

