using UnityEngine;

public abstract class Effect {
    public abstract string Id {get;}
    public abstract Color effectColor {get;}
    public abstract string Name {get;}
    float duration, time;
    public bool isIn, ended;
    public Effect(float dur) {
        duration = dur;

        isIn = true;
        time = 0;

        OnStart();
    }

    public abstract void OnStart();
    public abstract void OnEnd();

    public void OnUpdate() {
        if (isIn) {
            time += Time.deltaTime;

            if (time > duration) {
                isIn = false;
                ended = true;

                OnEnd();
            }
        }
    }
}