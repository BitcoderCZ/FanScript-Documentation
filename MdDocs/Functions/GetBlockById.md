# GetBlockById(float)

Returns a block with the specified id.

```
obj getBlockById(float ID)
```

## Parameters

#### `ID`
Type: [float](/MdDocs/Types/Float.md)

Id of the object, must be constant.

## Returns

[obj](/MdDocs/Types/Obj.md)

The [obj](/MdDocs/Types/Obj.md) with the [ID](#ID).

## Remarks

- The id of a block can be get by placing the block at (0, 0, 0) and running loggetBlock(0, 0, 0) in the EditorScript.
- EditorScript cannot connect wires to blocks, so if you are using it as the builder, you will have to connect the object wire to the block manually.

