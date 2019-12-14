# JWT-Token
JWT Token with refresh token using Redis (Linux only)

# Configuration
Need to configure connection to Redis and Token parameters in appsettings.json

# End Points

## Get
```
/publicinfo
```
Public information without signing in.

````
/privateinfo
need token bear
````
You must be logged in to access the information.

## Post
````
/login
````
Need json body:
{
  "Access" : "User Manager",
  "Password" : "123"
}
