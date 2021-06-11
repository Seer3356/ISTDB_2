# ISTDB

### API Call Dictionary

#### Sign Up

##### Type:

> HttpPost

##### URL Extension:

> /signup?username=<insertusernamehere>&password=<insertpasswordhere>

##### Use:

Creates new user login for ISTDB system.

#### Login

##### Type:

> HttpPost

##### URL Extension:

> /login?username=<insertusernamehere>&password=<insertpasswordhere>

##### Returns:

Unique ID used to fetching timetables & personal data.

##### Use:

Logs user into ISTDB system and hands out unique ID.

#### Delete User

##### Type:

> HttpDelete

##### URL Extension:

> /deleteuser?username=<insertusertobedeletedhere>

##### Returns:

Void

##### Use:

Deletes user from ISTDB system. (administration purposes only)

