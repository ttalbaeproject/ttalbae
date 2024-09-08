using UnityEngine;

public abstract class Effect {
    public abstract string Id {get;}
    public abstract Color effectColor {get;}
    public abstract string Name {get;}
    public float duration, time;
    public bool isIn, ended;
    public EffectIcon icon;

    public Effect(float dur) {
        duration = dur;

        isIn = true;
        time = 0;

        icon = GameObject.Instantiate(UIManager.Instance.effectIcon, UIManager.Instance.effects);

        icon.img.color = effectColor;
        icon.effName.text = Name;
    }

    public abstract void OnStart();
    public abstract void OnEnd();

    public void OnUpdate() {
        if (isIn) {
            time += Time.deltaTime;

            if (icon != null) {
                icon.time_col.fillAmount = time / duration;
            }

            if (time > duration) {
                End();
            }
        }
    }

    public void End() {
        isIn = false;
        ended = true;

        if (icon != null) {
            GameObject.Destroy(icon.gameObject);
            icon = null;
        }

        OnEnd();
    }
}