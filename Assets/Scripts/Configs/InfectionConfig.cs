using UnityEngine;

namespace BallGame.Configs
{
    [CreateAssetMenu(fileName = "Infection", menuName = "Create Infection", order = 0)]
    public class InfectionConfig : ScriptableObject
    {
        [SerializeField]
        private Material _infectedMaterial;
        public Material InfectedMaterial => _infectedMaterial;
        
        [SerializeField]
        private float _infectionSpeed = 1f;
        public float InfectionSpeed => _infectionSpeed;
        
        [SerializeField]
        private Vector3 _infectedScale = new Vector3(1.2f, 1.2f, 1.2f);
        public Vector3 InfectedScale => _infectedScale;
       
        [SerializeField]
        private ParticleSystem _infectionEffect;
        public ParticleSystem InfectionEffect => _infectionEffect;
    }
}