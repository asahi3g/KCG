using UnityEngine;

public class Player : BaseMonoBehaviour
{
    [SerializeField] private PlayerInput _input;
    [SerializeField] private PlayerCamera _camera;

    public PlayerInput GetInput() => _input;
    public PlayerCamera GetCamera() => _camera;

}
