```
#1.) Attach your EBS Volume after starting your instance. 
#2.) Mount the Existing volume
sudo mount /dev/xvdf /data

#gets rid of Failed to initialize libdc1394
sudo ln /dev/null /dev/raw1394


#3.) Start the DIGITS server:
./digits/digits-devserver


#4.) If FTP Needed:   Open the FTP config for editing:
sudo vi /etc/vsftpd.conf

#Change pasv_address to the Public IP of the AWS Instance:
pasv_address=54.157.133.214
#Change password:
sudo passwd ubuntu
#Restart server for changes to take effect:
sudo service vsftpd restart



#Shuting down:
sudo umount -d /dev/xvdf
```
