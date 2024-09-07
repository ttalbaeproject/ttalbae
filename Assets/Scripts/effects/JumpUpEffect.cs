using UnityEngine;

public class JumpUpEffect : Effect
{
    public override string Id => "jumpUp";

    public override Color effectColor => Color.green;

    public override string Name => "점프력 증가";

    public JumpUpEffect(float dur) : base(dur)
    {
    }

    public override void OnEnd()
    {
        DragSystem.Instance.maxDistance = DragSystem.Instance.defMaxDist;
        Player.Main.render.color = Player.Main.spriteCol;
    }

    public override void OnStart()
    {
        foreach (Effect eff in Player.Main.effects) {
            if (eff.Id == "jumpLow") {
                eff.End();
            }
        }

        DragSystem.Instance.maxDistance = DragSystem.Instance.defMaxDist * 1.3f;
        Player.Main.render.color = effectColor;
        
        Player.Main.EffectIndicate(Player.Main.good);
    }
}