using UnityEngine;

namespace Characters
{
    public interface ICharacterFactory
    {
        CharacterFacade CreatePlayer(Vector3 position, Quaternion rotation);
        CharacterFacade CreateNpc(Vector3 position, Quaternion rotation);
    }
}