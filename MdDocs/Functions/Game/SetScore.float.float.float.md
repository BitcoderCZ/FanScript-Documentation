

# SetScore(float, float, float)

Sets the [score](#score) and/or [coins](#coins).

```
void setScore(float score, float coins, const float RANKING)
```

## Parameters

#### `score`
Type: [float](/MdDocs/Types/Float.md)

The new score, if [RANKING](#RANKING) is [RANKING_TIME_FASTEST](/MdDocs/Constants/RANKING.md#RANKING_TIME_FASTEST) or [RANKING_TIME_LONGEST](/MdDocs/Constants/RANKING.md#RANKING_TIME_LONGEST) time is specified in frames (60 - 1s).

#### `coins`
Type: [float](/MdDocs/Types/Float.md)

The new amount of coins.

#### `RANKING`
Modifiers: [const](/MdDocs/Modifiers/Constant.md)

Type: [float](/MdDocs/Types/Float.md)

How players are ranked, one of [RANKING](/MdDocs/Constants/RANKING.md).


