# Idea
1. Widget: counts online users, ability to get active users count
2. Monitoring: life dashboard to see extended info about users
3. Site: landing with demo widget


## Widget

1. Send info about user with beacon request

## Monitor

Realtime dashboard with "lives".
Table: `id, end of life`

Tech: SignalR https://docs.microsoft.com/en-us/aspnet/core/tutorials/signalr

1. Add connections to groups
2. Create redis-sub for a group
3. Remove connections from groups on disconnect 
4. Remove sub if group is empty


### Other
https://colorhunt.co/palette/f7f7f7eeeeee393e46929aab