using System;
using System.Threading.Tasks;

public interface ISceneServices
{
    public Task LoadScene(string sceneName);
    public Task LoadScene(SceneReference scene);

    public void SetCurrentTransition(SceneTransition transition);
}