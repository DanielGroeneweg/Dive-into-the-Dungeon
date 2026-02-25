using UnityEngine;
[CreateAssetMenu(menuName = "Spells/Effects/Create Wall")]
public class CreateWallEffect : SpellEffect
{
    public SpellWall wallPrefab;
    public override void Execute(SpellStats stats, SpellContext context)
    {
        SpellWall wall = Instantiate(wallPrefab, context.spellPosition, context.spellRotation);

        Vector3 size = wall.transform.localScale;
        size.x *= stats.areaSize;
        size.y *= stats.areaSize;
        wall.transform.localScale = size;

        wall.StartCountdown(stats.duration);
    }
}
