namespace BLITTY;

public abstract class Scene
{
    public abstract void Load();

    public abstract void Unload();

    public abstract void Update(float dt);

    public abstract void FixedUpdate(float dt);

    public abstract void Draw();

}
