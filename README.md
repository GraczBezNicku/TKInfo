# TKInfo
TKInfo is a plugin for SCP:SL that informs players about team damage, thus making it easier to navigate and punish teamkillers.

# Installation
1. You will need to install EXILED (https://github.com/Exiled-Team/EXILED) and download the .dll file from "Releases"
2. Navigate to your Plugins folder and put the dll there.
3. Go to your configs and configure both permissions and options.

# Displaying Damage
For now I'm experimenting with manually calculating damage so that display_damage would work again. However give me a couple of days, since I'm doing other things now. (27/12/2021)

# Permissions
The plugins has only one persmission and it's tkinfo.modalert . It's responsible for notyfing server moderators about recent team damage.

# Default Config Options
```
  t_k_info:
  is_enabled: true
  # Displays damage dealt to the target
  display_damage: true
  # Lets players report team dammagers / killers. If teamKillerNotifier is on true, reports can be used as an early warning.
  can_report: true
  # Will notify the entire server on OnRoundStart about a player who has done {maxNotifierDamage} or has recivied {maxNotifierReports}.
  team_killer_notifier: true
  # If canReport is enabled, on every OnWaitingOnPlayers a notification will appear, reminding people that they can report!
  report_reminder: true
  # Length of reportReminder
  report_reminder_length: 20
  # If teamKillerNotifier is set to true, this is the limit needed to broadcast about a player. (It will loop and display the highest report number)
  max_notifier_reports: 10
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
  # Length of the most reported players alerts
  on_round_start_alert_length: 12
  # Message for reportReminder
  report_reminder_message: <color=green>[TKInfo]</color> You can report players who team damage in console (~ key) using <color=lime>.report <steamid64></color>
  # Displays a warning about the most reported player (needs to be reported {maxNotifierReports} times.) (No damage shown)
  on_round_start_not_no_d_m_g: <color=green>[TKInfo]</color> <color=maroon>{player}</color> is the most reported player online, standing at <color=brown>{reports}</color> reports for team damage, be careful around them!
  # Displays a warning about the most reported player (needs to be reported {maxNotifierReports} times.) (shows damage)
  on_round_start_not: <color=green>[TKInfo]</color> <color=maroon>{player}</color> is the most reported player online, standing at <color=brown>{reports}</color> reports for team damage and has dealt over <color=red>{dmg}</color> HP to their teammates. Be careful around them!
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
