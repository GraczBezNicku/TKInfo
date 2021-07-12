# TKInfo
TKInfo is a plugin for SCP:SL that informs players about team damage, thus making it easier to navigate and punish teamkillers.

# Recent Info

12/07/2021: I'm tired for now since I've spent all my day making this so I'll make this page look better later.

# Installation
1. You will need to install EXILED (https://github.com/Exiled-Team/EXILED) and download the .dll file from "Releases"
2. Navigate to your Plugins folder and put the dll there.
3. Go to your configs and configure both permissions and options.

# Permissions
The plugins has only one persmission and it's tkinfo.modalert . It's responsible for notyfing server moderators about recent team damage.

# Default Config Options
```
  is_enabled: true
  # Displays damage dealt to the target
  display_damage: true
  # If someone gets team damaged beyond limit it will alert people with tkinfo.modalert permission
  mod_alert: true
  # people with tkinfo.modalert permission will only see the notification when they're dead if this is set to true.
  mod_alert_only_when_dead: true
  # Upon shooting a teammate, the attacker also gets the notification.
  notify_attacker: true
  # Damage limit, which upon reaching will alert people with tkinfo.modalert permission. (modAlert must be set to true!)
  alert_damage: 70
  # Length of the player alerts
  player_alert_length: 6
  # Length of the mod alerts
  mod_alert_length: 12
  # Displays a warning that the team damage hit was higher than alertDamage and displayDamage is set to true.
  mod_alert_msg: <color=green>[TK-Info]</color> <color=lime>{player}</color> was attacked by their teammate <color=maroon>{player2}</color> for <color=red>{dmg}</color> HP!
  # Displays a warning that the team damage hit was higher than alertDamage and displayDamage is set to false.
  mod_alert_msg_no_d_m_g: <color=green>[TK-Info]</color> <color=lime>{player}</color> was attacked by their teammate <color=maroon>{player2}</color>!
  # Displays when attacked by your teammate and displayDamage is set to true,
  attack_msg: <color=green>[TK-Info]</color> You were attacked by your teammate {player} for <color=red>{dmg}</color> HP!
  # Displays when you attack your teammate and displayDamage is set to true
  attacker_notification: <color=green>[TK-Info]</color> You attacked your teammate {player} for <color=red>{dmg}</color> HP!
  # Displays when attacked by your teammate and displayDamage is set to false
  attack_msg_no_d_m_g: <color=green>[TK-Info]</color> You were attacked by your teammate {player}!
  # Displays when you attack your teammate and displayDamage is set to false
  attacker_notification_no_d_m_g: <color=green>[TK-Info]</color> You attacked your teammate {player}!
```
