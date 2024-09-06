using UnityEngine;

public class JumpDownEffect : Effect
{
    public override string Id => "jumpLow";

    public override Color effectColor => Color.gray;

    public override string Name => "점프력 감소s";

    public JumpDownEffect(float dur) : base(dur)
    {
    }

    public override void OnEnd()
    {
        DragSystem.Instance.maxDistance = DragSystem.Instance.defMaxDist;
        Player.Main.render.color = Player.Main.spriteCol;
    }

    public override void OnStart()
    {
        DragSystem.Instance.maxDistance = DragSystem.Instance.defMaxDist * 0.6f;
        Player.Main.render.color = effectColor;
    }
}