using UnityEngine;

public class FlipEffect : Effect
{
    public override string Id => "flip";

    public override Color effectColor => Color.magenta;

    public override string Name => "좌우 반전";

    public FlipEffect(float dur) : base(dur)
    {
    }

    public override void OnEnd()
    {
        Player.Main.isFlipped = false;
        
        Player.Main.render.color = Player.Main.spriteCol;
    }

    public override void OnStart()
    {
        Player.Main.isFlipped = true;
        Player.Main.render.color = effectColor;
        
        Player.Main.EffectIndicate(Player.Main.bad);
    }
}