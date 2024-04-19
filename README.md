
# Degree JobTracker
>This is a Web application for tracking people’s educational degrees and job histories. The system allows its users to filter jobs using a wide variety of metrics and allows administrators to do the same with all the data.

### Setting up a database connection for the application
To use this application you must have a Microsoft SQL Server database set up. To initially configure the database run the `DatabaseSetup.sql` script on the server. After configuring the database, edit the line of the `appsettings.json` file that has "DegreeJobTrackerContext" on it with the connection string of the database you want to use for the application. *Please Note:* `DatabseSetup.sql` *contains initial seed data for the database. To prevent this use `EmptyDatabaseSetup.Sql` instead.* **Warning running this script again after using the program for some time will delete all entries in the database!**

### Running the application
It is recommended to change the username and password for the admin view login immediately. The default credentials are "admin" for the username and "password" for the password. This is not secure at all. The credentials can be changed from within the application by logging in as the admin and clicking "Account". From there you can change the credentials via the links on the page.

### Editing/Deleting the Admin View Login Credentials
If a user forgets their credentials you can fix this by following the steps below:
1. Decide on what you want the username and password to be. (eg. Username: admin, Password: password).
2. Run the `Password Hasher` application and enter the password you wish to use.
3. Run the following sql on the database, replacing ***YourUsernameHere*** and ***YourPasswordHashHere*** with your chosen username and the password hash:
    ```
    INSERT INTO user_credential 
        (username, password)
        VALUES
        ('YourUsernameHere', 'YourPasswordHashHere')
    ;
    ```
4. (Optional) Delete the previous password by running the following sql, replacing  ***YourOldUsernameHere*** with the credentials you want to replace:
    ```
    DELETE FROM user_credential
    WHERE username = 'YourOldUsernameHere'
    ;
    ```