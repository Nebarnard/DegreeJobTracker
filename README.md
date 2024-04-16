# Degree JobTracker
>This is a Web application for tracking people’s educational degrees and job histories. The system allows its users to filter jobs using a wide variety of metrics and allows administrators to do the same with all the data.

### Changing the Admin View Login Credentials
There is no way to change the user credentials within the program itself. To do so you must follow the following steps:
1. Decide on what you want your username and password to be. (eg. Username: admin, Password: password).
2. Run the `Password Hasher` application and enter the password you wish to use.
3. Run the following sql on the database, replacing ***YourUsernameHere*** and ***YourPasswordHashHere*** with your chosen username and the password hash:
    ```
    INSERT INTO user_credential 
        (username, password)
        VALUES
        ('YourUserNameHere', 'YourPasswordHashHere')
    ;
    ```
4. (Optional) Delete the previous password by running the following sql, replacing  ***YourOldUsernameHere*** with the credentials you want to replace:
    ```
    DELETE FROM user_credential
    WHERE username = 'YourOldUsernameHere'
    ;
    ```