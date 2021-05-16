Those files are the scripts of the Database.
To create database:
1. open a new DB using MYSQL.
2. Upload all of the PHP files to a folder in the files explorer of the Hosting.
3. Find the link that directs to the files. Should look like(https://mywebsite.com/{CustomDirectory}/)
4. Adjust each file and add the password of the DB to those files.
5. You can change the EncryptionKey by generating new one and editing on both PHP Files & Unity scripts that communicate with the DB.
6. Add your URL to your Unity Project, link should look like that: (https://mywebsite.com/{directory}/{PHPFILE (Example: getData.php)}?).