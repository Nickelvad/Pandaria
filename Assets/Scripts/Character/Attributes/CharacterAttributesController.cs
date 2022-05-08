using System.Collections.Generic;
using UnityEngine;
using Pandaria.Items;

namespace Pandaria.Characters.Attributes
{
    public class CharacterAttributesController : Singleton<CharacterAttributesController>
    {
        public GameObject attributesContainer;
        [HideInInspector] public Health health;
        [HideInInspector] public Stamina stamina;
        [HideInInspector] public Mana mana;
        [HideInInspector] public Defence defence;
        [HideInInspector] public CritDefenceRating critDefenceRating;
        [HideInInspector] public CritRating critRating;
        [HideInInspector] public AttackSpeed attackSpeed;
        [HideInInspector] public MinAttack minAttack;
        [HideInInspector] public MaxAttack maxAttack;
        private List<IAttribute> attributes;

        public void Awake()
        {
            health = attributesContainer.GetComponent<Health>();
            stamina = attributesContainer.GetComponent<Stamina>();
            mana = attributesContainer.GetComponent<Mana>();
            defence = attributesContainer.GetComponent<Defence>();
            critDefenceRating = attributesContainer.GetComponent<CritDefenceRating>();
            critRating = attributesContainer.GetComponent<CritRating>();
            attackSpeed = attributesContainer.GetComponent<AttackSpeed>();
            minAttack = attributesContainer.GetComponent<MinAttack>();
            maxAttack = attributesContainer.GetComponent<MaxAttack>();
            attributes = new List<IAttribute>
            {
                health, stamina, mana, defence, critDefenceRating, critRating, attackSpeed, minAttack, maxAttack
            };
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
            foreach (IAttribute attribute in attributes)
            {
                attribute.Recalculate();
            }
        }
    }

}
