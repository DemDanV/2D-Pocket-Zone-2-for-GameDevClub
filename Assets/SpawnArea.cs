using System;
using Unity.VisualScripting;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

public abstract class SpawnArea : MonoBehaviour
{
    public virtual T Spawn<T>(T prefab, Vector3 position) where T : Object
    {
        if (prefab is null)
        {
            throw new ArgumentNullException(nameof(prefab));
        }

        T t = Instantiate(prefab);
        GameObject go = t.GameObject();
        go.transform.position = position;
        return t;
    }
}

public abstract class RectangleSpawnArea : SpawnArea
{
    protected virtual T Spawn<T>(T prefab, Vector3 center, Vector2 size) where T : Object
    {
        Vector2 offset = GetOffsetOnRectangle(size);
        Vector3 pos = center + new Vector3(offset.x, offset.y);
        return Spawn(prefab, pos);
    }

    protected virtual T Spawn<T>(T prefab, Vector3 center, Vector2 minSize, Vector2 maxSize) where T : Object
    {
        Vector2 offset = GetOffsetInRectangle(minSize, maxSize);
        Vector3 pos = center + new Vector3(offset.x, offset.y);
        return Spawn(prefab, pos);
    }

    protected virtual T Spawn<T>(T prefab, Vector3 center, Vector2 size, Vector3 direction) where T : Object
    {
        Vector2 offset = GetOffsetOnRectangleSpread(size, direction, 180);
        Vector3 pos = center + new Vector3(offset.x, offset.y);
        return Spawn(prefab, pos);
    }

    protected Vector2 GetOffsetOnRectangle(Vector2 size)
    {
        float halfWidth = size.x * 0.5f;
        float halfHeight = size.y * 0.5f;
        float offsetX = Random.Range(-halfWidth, halfWidth);
        float offsetY = Random.Range(-halfHeight, halfHeight);

        return new Vector2(offsetX, offsetY);
    }

    protected Vector2 GetOffsetInRectangle(Vector2 minSize, Vector2 maxSize)
    {
        float offsetX = Random.Range(minSize.x * 0.5f, maxSize.x * 0.5f);
        float offsetY = Random.Range(minSize.y * 0.5f, maxSize.y * 0.5f);

        if (Random.value < 0.5f)
            offsetX *= -1;

        if (Random.value < 0.5f)
            offsetY *= -1;

        return new Vector2(offsetX, offsetY);
    }

    protected Vector2 GetOffsetOnRectangleSpread(Vector2 size, Vector2 direction, float maxSpreadAngleDegrees)
    {
        float randomAngleRadians = Random.Range(0.0f, maxSpreadAngleDegrees * Mathf.Deg2Rad);

        Vector2 randomOffset = Quaternion.Euler(0, 0, randomAngleRadians * Mathf.Rad2Deg) * direction;
        float offsetX = Random.Range(-size.x * 0.5f, size.x * 0.5f);
        float offsetY = Random.Range(-size.y * 0.5f, size.y * 0.5f);

        return new Vector2(offsetX, offsetY) + randomOffset;
    }
}

public abstract class CircleSpawnArea : SpawnArea
{
    protected virtual T Spawn<T>(T prefab, Vector3 center, float radius) where T : Object
    {
        Vector2 offset = GetOffsetOnCircle(radius);
        Vector3 pos = center + new Vector3(offset.x, offset.y);
        return Spawn(prefab, pos);
    }

    protected virtual T Spawn<T>(T prefab, Vector3 center, float minRadius, float maxRadius) where T : Object
    {
        Vector2 offset = GetOffsetInCircle(minRadius, maxRadius);
        Vector3 pos = center + new Vector3(offset.x, offset.y);
        return Spawn(prefab, pos);
    }

    protected virtual T Spawn<T>(T prefab, Vector3 center, float radius, Vector3 direction) where T : Object
    {
        Vector2 offset = GetOffsetOnCircleSpread(radius, direction, 180);
        Vector3 pos = center + new Vector3(offset.x, offset.y);
        return Spawn(prefab, pos);
    }

    protected Vector2 GetOffsetOnCircle(float radius)
    {
        Vector2 rand2D = Random.insideUnitCircle * radius;
        Vector3 fromPos = transform.position + new Vector3(rand2D.x, rand2D.y, 0);

        return fromPos;
    }

    protected Vector2 GetOffsetInCircle(float minRadius, float maxRadius)
    {
        float randDeg = Random.Range(0, 360);
        float angleRadians = Mathf.Deg2Rad * randDeg;

        float x = transform.position.x + minRadius * Mathf.Cos(angleRadians);
        float y = transform.position.y + maxRadius * Mathf.Sin(angleRadians);

        return new Vector3(x, y, 0);
    }

    protected Vector2 GetOffsetOnCircleSpread(float radius, Vector3 direction, float maxSpreadAngleDegrees)
    {
        float randomAngleRadians = Random.Range(0.0f, maxSpreadAngleDegrees * Mathf.Deg2Rad);

        Vector3 randomOffset = Quaternion.Euler(0, 0, randomAngleRadians * Mathf.Rad2Deg) * direction;

        return -randomOffset.normalized * radius;
    }
}