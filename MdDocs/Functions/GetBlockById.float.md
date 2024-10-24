

# GetBlockById(float)

Returns a block with the specified id.

```
obj getBlockById(const float BLOCK)
```

## Parameters

#### `BLOCK`
Modifiers: [const](/MdDocs/Modifiers/Constant.md)

Type: [float](/MdDocs/Types/Float.md)

Id of the block, one of [BLOCK](/MdDocs/Constants/BLOCK.md).

## Returns

[obj](/MdDocs/Types/Obj.md)

The [obj](/MdDocs/Types/Obj.md) specified by [BLOCK](#BLOCK).

## Remarks

 - The id of a block can be get by placing the block at (0, 0, 0) and running log(getBlock(0, 0, 0)) in the EditorScript.
 - EditorScript cannot connect wires to blocks, so if you are using it as the builder, you will have to connect the object wire to the block manually.

## Examples

``` fcs
obj dirt = getBlockById(BLOCK_DIRT)
```


