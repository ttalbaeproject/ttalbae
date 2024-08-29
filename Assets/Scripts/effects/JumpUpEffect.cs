using UnityEngine;

public class JumpUpEffect : Effect
{
    public override string Id => "jumpUp";
    public Color effectColor = Color.green;

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
        DragSystem.Instance.maxDistance = DragSystem.Instance.defMaxDist * 1.3f;
        Player.Main.render.color = effectColor;
    }
}