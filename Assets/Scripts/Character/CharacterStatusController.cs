using UnityEngine;
using Pandaria.Items;
using Pandaria.Characters.Attributes;

namespace Pandaria.Characters
{
    public class CharacterStatusController : Singleton<CharacterStatusController>
    {
        public GameObject attributesContainer;
        [HideInInspector] public Health health;
        [HideInInspector] public Stamina stamina;
        [HideInInspector] public Mana mana;
        [HideInInspector] public Defence defence;
        [HideInInspector] public CritDefenceRating critDefenceRating;
        [HideInInspector] public CritRating critRating;
        [HideInInspector] public AttackSpeed attackSpeed;
        [ReadOnly] public float attackMin = 0f;
        [ReadOnly] public float attackMax = 0f;

        public void Start()
        {
            Debug.Log("Starting");
            health = attributesContainer.GetComponent<Health>();
            stamina = attributesContainer.GetComponent<Stamina>();
            mana = attributesContainer.GetComponent<Mana>();
            defence = attributesContainer.GetComponent<Defence>();
            critDefenceRating = attributesContainer.GetComponent<CritDefenceRating>();
            critRating = attributesContainer.GetComponent<CritRating>();
            attackSpeed = attributesContainer.GetComponent<AttackSpeed>();
            EventBus.Instance.EquipmentChanged += RecalculateAttributes;
        }

        public bool CanDash()
        {
            return stamina.CanDash();
        }

        public void ApplyDash()
        {
            stamina.ApplyDash();
        }

        public void ApplyDamage(float damage)
        {
            health.ApplyDamage(damage);
        }
    
        void RecalculateAttributes(object sender, EquipableItem equipmentItem)
        {
            
        }
    }

}
