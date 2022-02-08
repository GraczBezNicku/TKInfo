# TKInfo
TKInfo is a plugin for SCP:SL that informs players about team damage. It also has a webhook feature, making it easier for staff members to act quickly upon teamkillers.

# Installation
1. You will need to install EXILED (https://github.com/Exiled-Team/EXILED) and download the .dll file from "Releases"
2. Navigate to your Plugins folder and put the dll there.
3. Go to your configs and configure both permissions and options.

# Isn't it just Killlogs?
No. TKInfo was first pointed at players, so it would be easier for them to report team killers. The newest update adding webhooks and revamping .report makes it similair to killogs, but keep in mind that TKInfo was released before Killogs.

# Permissions
The plugin has only one persmission and it's tkinfo.modalert . It's responsible for notyfing server moderators about teamkills, cuffkills and reports.

# Default Config Options
```
 t_k_info:
  is_enabled: true
  log_via_discord_webhook: false
  webhook_u_r_l: ''
  staff_role_i_d: ''
  webhook_avatar_u_r_l: https://cdn.discordapp.com/attachments/434037173281488899/940610688760545290/mrozonyhyperthink.jpg
  webhook_kill_text: ':red_square: [TEAM-KILL] :red_square: {Time} {target} ({targetID}) playing as {targetRole} was killed by their teammate {attacker} ({attackerID}) playing as {attackerRole}. <@&{roleID}>'
  webhook_cuff_kill_text: ':orange_square: [CUFF-KILL] :orange_square: {Time} {target} ({targetID}) playing as {targetRole} was killed by {attacker} ({attackerID}) playing as {attackerRole} whilst cuffed. <@&{roleID}>'
  notify_attacker: true
  attacker_message: <color=green>[TKINFO]</color> You have attacked your teammate <color=lime>{target}</color>!
  target_message: <color=green>[TKINFO]</color> You have been attacked by your teammate <color=maroon>{attacker} ({attackerID})</color>
  broadcast_duration: 3
  log_cuffed_kills: false
  alert_only_when_dead: true
  alert_message_kill: <color=green>[TKINFO]</color> <color=maroon>{killer} ({killerID})</color> has killed their teammate <color=lime>{target} ({targetID})</color>!
  alert_message_cuffed_killl: <color=green>[TKINFO]</color> <color=maroon>{killer} ({killerID})</color> has killed a cuffed player <color=lime>{target} ({targetID})</color>!
  alert_duration: 3
  can_players_report: true
  player_not_found_response: Player doesn't exit or has left the server.
  successful_report_response: Player has been reported to online staff (if any).
  report_wrong_usage_response: 'Wrong usage: .report Nickname'
  reporting_is_disabled_response: Reporting has been disabled on this server.
  alert_reported_player: <color=green>[TKINFO]</color> <color=green>{reporter} ({reporterID})</color> has reported <color=maroon>{reported} ({reportedID})</color>
  alert_report_duration: 6

```
