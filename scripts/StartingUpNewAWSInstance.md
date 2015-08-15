#Open the FTP config for editing:
sudo vi /etc/vsftpd.conf

#Change pasv_address to the Public IP of the AWS Instance:
pasv_address=54.157.133.214
#Change password:
sudo passwd ubuntu
#Restart server for changes to take effect:
sudo service vsftpd restart



#Attach your EBS Volume after starting your instance. 
#Mount the Existing volume
sudo mount /dev/xvdf /data
#Add permissions:
sudo chmod 777 /data

#Shuting down:
sudo umount -d /dev/xvdf

