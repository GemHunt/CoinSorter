FTP Install Notes
There are all sorts of examples of using vsftpd on AWS, but they all a bit different. This is what works for me, just to get things started. This is for a temporary instance that only I would be using. 


Security group changes: 
Make sure 20-21 and 1024-1048 is open to your IP
sudo apt-get update
sudo apt-get install vsftpd

sudo vi /etc/vsftpd.conf
Add the following lines to the bottom of the vsftpd.conf file:
#Change pasv_address to the Public IP of the AWS Instance:
pasv_address=50.16.43.125
pasv_enable=YES
pasv_min_port=1024
pasv_max_port=1048
port_enable=YES
pasv_addr_resolve=YES
write_enable=YES

#Change password:
sudo passwd ubuntu

#Restart server for changes to take effect:
sudo service vsftpd restart
