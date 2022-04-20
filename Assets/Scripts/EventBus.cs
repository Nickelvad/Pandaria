
using System;
using System.Collections.Generic;
using Pandaria.Character.Actions;

namespace Pandaria
{
    public class EventBus : Singleton<EventBus>
    {
        public event EventHandler<List<CharacterAction>> ActionsChanged;

        public void CallActionsChanged(object sender, List<CharacterAction> actions)
        {
            ActionsChanged?.Invoke(sender, actions);
        }
    }
}